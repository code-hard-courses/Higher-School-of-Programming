### 9. Dependency injection: возможное решение

Python не поддерживает концепцию интерфейсов как лингвистическую абстракцию, поэтому определим условный класс ICleaner, имитирующий интерфейс (абстрактные базовые классы тоже использовать не будем, дабы не усложнять идею деталями программной реализации). Описание этого класса задает абстрактное описание всех реализаций нашего робот-интерпретатора.

Условный "интерфейс" (robot.py):

```
# интерфейс Чистильщик
class ICleaner:

    # режимы работы устройства очистки
    WATER = 1 # полив водой
    SOAP  = 2 # полив мыльной пеной
    BRUSH = 3 # чистка щётками

    # конструктор 
    def __init__(self, transfer):
        pass  

    # перемещение
    def move(self,dist):
        pass

    # поворот
    def turn(self,turn_angle):
        pass

    # установка режима работы
    def set_state(self,new_state):
        pass

    # начало чистки
    def start(self):
        pass

    # конец чистки   
    def stop(self):
        pass

    # интерпретация набора команд
    def make(self,code):
        pass
```

Первая сменяемая реализация. Первую реализацию этого интерфейса оставим почти без изменений.

```
# класс Чистильщик
class Cleaner(ICleaner):


    # конструктор 
    def __init__(self, transfer):
        self.x = 0.0
        self.y = 0.0
        self.angle = 0
        self.state = self.WATER
        self.transfer = transfer 

    # перемещение
    def move(self,dist):
        angle_rads = self.angle * (math.pi/180.0)
        self.x += dist * math.cos(angle_rads)
        self.y += dist * math.sin(angle_rads)
        self.transfer(('POS(',self.x,',',self.y,')'))

    # поворот
    def turn(self,turn_angle):
        self.angle += turn_angle
        self.transfer(('ANGLE',self.angle))

    # установка режима работы
    def set_state(self,new_state):
        if new_state=='water':
            self.state = self.WATER  
        elif new_state=='soap':
            self.state = self.SOAP
        elif new_state=='brush':
            self.state = self.BRUSH
        self.transfer(('STATE',self.state))

    # начало чистки
    def start(self):
        self.transfer(('START WITH',self.state))

    # конец чистки   
    def stop(self):
        self.transfer(('STOP',))

    # интерпретация набора команд
    def make(self,code):
        for command in code:
            cmd = command.split(' ')
            if cmd[0]=='move':
                self.move(int(cmd[1])) 
            elif cmd[0]=='turn':
                self.turn(int(cmd[1]))   
            elif cmd[0]=='set':
                self.set_state(cmd[1]) 
            elif cmd[0]=='start':
                self.start()
            elif cmd[0]=='stop':
                self.stop()
```

Вторая сменяемая реализация, допустим, задает перемещение робота на вдвое меньшую дистанцию. Просто унаследуем её от первой реализации, и переопределим всего один метод.

```
# класс Медленный Чистильщик
class HalfCleaner(Cleaner):

    def move(self,dist):
        super(HalfCleaner, self).move(dist/2)
```

Серверный интерфейс cleaner_api.py:

```
import robot

# класс Чистильщик API
class CleanerApi:

    # взаимодействие с роботом вынесено в отдельную функцию
    def transfer_to_cleaner(self,message):
        print (message)

    # конструктор 
    def __init__(self):
        self.cleaner = robot.HalfCleaner(self.transfer_to_cleaner) 

    # интерпретация набора команд
    def activate_cleaner(self,code):
        self.cleaner.make(code)

    def get_x(self):
        return self.cleaner.x

    def get_y(self):
        return self.cleaner.y

    def get_angle(self):
        return self.cleaner.angle

    def get_state(self):
        return self.cleaner.state
```

В результате мы можем создать множество реализаций, работающих по единой схеме, а в CleanerApi достаточно будет изменить под нужную реализацию всего одну строчку, или сделать гибкую настройку. Например, конкретная реализация может запрашиваться клиентом в момент создания серверной сессии, и надо просто организовать выбор соответствующего класса-реализатора в конструкторе. Такая схема напоминает паттерн Декоратор.

Клиентская часть:

```
from cleaner_api import CleanerApi

# главная программа
cleaner_api = CleanerApi()
cleaner_api.activate_cleaner((
    'move 100',
    'turn -90',
    'set soap',
    'start',
    'move 50',
    'stop'
    ))
print (cleaner_api.get_x(),cleaner_api.get_y(),cleaner_api.get_angle(),
    cleaner_api.get_state())
```

**Плюсы.**

Мы не завязываемся теперь на конкретную реализацию, и можем свободно выбирать между различными вариантами реализаций "на лету".

**Минусы.**

Сохраняются все недостатки объектного подхода (в частности, зависимости от внутреннего состояния объекта).

Интерфейсы монолитны, абстрактны и не компонуемы друг с другом.

Если решено внести правки в класс-интерфейс, редактировать также придётся множество классов-реализаций.

*Далее рассмотрим, как подобный подход реализовать в стиле функционального программирования.*

### 10. Функциональная инъекция зависимостей

Недостаток объектного подхода при инъекции зависимостей, во-первых, в том, что интерфейсы получаются автономными, и мы не можем их компоновать, и во-вторых, нарушается важный паттерн проектирования Interface Segregation Principle, когда клиент должен получать/видеть только те интерфейсные методы, которые он непосредственно использует.

При функциональном подходе мы применяем исключительно функции. На уровне API взаимодействие происходит через вызовы, которым функции передаются как параметры, в результате чего удаётся разделить вызывающую сторону от "инъекции". Тут, в частности, возможны два подхода:

- каждая зависимость (функция робота) передаётся отдельно;
- передаётся только одна функция (полиморфически).

Первый случай. **Передача каждой зависимости в отдельной функции.**

Реализуйте функциональную инъекцию зависимостей, используя наш базовый модуль pure_robot.py, в котором сосредоточены ключевые "низкоуровневые" функции управления роботом, так, как вы это понимаете.
