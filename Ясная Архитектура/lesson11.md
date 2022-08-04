### 10. Функциональная инъекция зависимостей: передача каждой зависимости в отдельной функции

Снова задействуем наш базовый модуль pure_robot.py, в котором сосредоточены ключевые "низкоуровневые" функции управления роботом. Он был определён уже давно, и с тех пор не претерпел вообще никаких изменений, что крайне важно.

Это простейший случай управления зависимостями -- просто передаём нужную функцию в качестве параметра функции, которая в ней нуждается.

**cleaner_api.py**

```
import pure_robot

class RobotApi:

    def setup(self, f_move,f_turn,f_set_state,f_start,f_stop, f_transfer):
        self.f_move = f_move
        self.f_turn = f_turn
        self.f_set_state = f_set_state
        self.f_start = f_start
        self.f_stop = f_stop
        self.f_transfer = f_transfer

    def make(self, command):
        if not hasattr(self, 'cleaner_state'):
            self.cleaner_state = 
                pure_robot.RobotState(0.0, 0.0, 0, pure_robot.WATER)

        cmd = command.split(' ')
        if cmd[0]=='move':
             self.cleaner_state = self.f_move(self.f_transfer,int(cmd[1]), 
                 self.cleaner_state) 
        elif cmd[0]=='turn':
            self.cleaner_state = self.f_turn(self.f_transfer,int(cmd[1]), 
                 self.cleaner_state)
        elif cmd[0]=='set':
            self.cleaner_state = self.f_set_state(self.f_transfer,cmd[1], 
                 self.cleaner_state) 
        elif cmd[0]=='start':
            self.cleaner_state = self.f_start(self.f_transfer, 
               self.cleaner_state)
        elif cmd[0]=='stop':
            self.cleaner_state = self.f_stop(self.f_transfer, 
               self.cleaner_state)
        return self.cleaner_state

    def __call__(self, command):
        return self.make(command)


def transfer_to_cleaner(message):
    print (message)

def double_move(transfer,dist,state):
    return pure_robot.move(transfer,dist*2,state)

api = RobotApi()  
api.setup(pure_robot.move, pure_robot.turn, pure_robot.set_state, 
    pure_robot.start, pure_robot.stop, transfer_to_cleaner)
# api.setup(double_move, pure_robot.turn, pure_robot.set_state,
#    pure_robot.start, pure_robot.stop, transfer_to_cleaner)
```

**client.py**

```
from cleaner_api import api

api('move 100')
api('turn -90')
api('set soap')
api('start')
api('move 50')
s = api('stop')
```

Далее следуют комментарии к этому коду.


Для удобства хранения мутабельного состояния мы создали нашему API обёртку в виде класса.

Сам класс определён как callable с помощью специфичного для Python приёма -- когда с помощью внутренней функции __call__ класс можно вызывать как обычную функцию. А в функциональных языках такой способ обычно поддерживается естественным образом.

Наш интерфейс -- это единственная функция api(), которая на входе получает строку-команду. Внутри она настраивается методом setup, который получает список конкретных вызываемых функций, реализующих нужный интерфейс.

Метод make разбирает входную строку и вызывает соответствующую функцию, реализация которой настраивается независимо.

Смена реализации нашего интерфейса -- единственной функции api() -- выполняется так же прозрачно, одной строкой. Например, реализация, где перемещение происходит на вдвое большее расстояние, меняется одной строчкой.

Важно, что нашей api-функции не требуется конструктор. И ещё один серьёзный плюс -- возможность тестирования клиентской части путём подмены (техника mock) функции api на отладочную, что в нашем случае возможно одним оператором.

Далее рассмотрим второй вариант функциональной реализации.
