### Стиль и архитектура

Мы рассмотрели несколько базовых стилей программирования, теперь рассмотрим элементарную архитектуру. В чём различие между стилем и архитектурой программирования?

**Стиль** -- это способ, которым мы составляем непосредственно исходный код, и организуем его в модули.

**Архитектура** -- это способ, которым мы из готовых модулей составляем результирующую систему, учитывая нужные технические характеристики.

Стиль -- это программирование в чистом виде, а архитектура связана прежде всего с организацией взаимодействия кода ("бизнес-логики"), баз данных, серверов приложений, увязывание всех подсистем в сетевой инфраструктуре, обеспечение масштабирования и т. д.

В общем случае стиль и архитектура -- это оси "прикладное - системное" или "computer science - инженерия".

Но прежде всего в понятие программной архитектуры включают  **схему взаимодействия множества клиентов с сервером** . Именно исходя из выбранной схемы и удаётся в итоге построить крупную, надёжно работающую и хорошо масштабирующуюся под высокие нагрузки систему. Или не удаётся -- если архитектура спроектирована неправильно.


### 6. Типовая хипстерская архитектура

Это "классический" ошибочный архитектурный паттерн, который однако реализован в огромном количестве ныне действующих проектов. Его массовость объясняется очевидностью, простотой, и непониманием принципиальных моментов highload-систем.

Архитектура подразумевает некий программный интерфейс (API), который предоставляется клиентам и скрывает реализацию где-то в недрах сервера. У нас есть модуль robot.py из предыдущего примера (назовём его pure_robot.py), который хранит функциональную реализацию Чистильщика.

Но клиенту мы предоставим совершенно оригинальный API, который инкапсулирует (полностью скроет) эту реализацию.

### pure_robot.py

```
import math
from collections import namedtuple

RobotState = namedtuple("RobotState", "x y angle state")

# режимы работы устройства очистки
WATER = 1 # полив водой
SOAP  = 2 # полив мыльной пеной
BRUSH = 3 # чистка щётками


# взаимодействие с роботом вынесено в отдельную функцию
def transfer_to_cleaner(message):
    print (message)

# перемещение
def move(transfer,dist,state):
    angle_rads = state.angle * (math.pi/180.0)   
    new_state = RobotState(
        state.x + dist * math.cos(angle_rads),
        state.y + dist * math.sin(angle_rads),
        state.angle,
        state.state)  
    transfer(('POS(',new_state.x,',',new_state.y,')'))
    return new_state

# поворот
def turn(transfer,turn_angle,state):
    new_state = RobotState(
        state.x,
        state.y,
        state.angle + turn_angle,
        state.state)
    transfer(('ANGLE',state.angle))
    return new_state

# установка режима работы
def set_state(transfer,new_internal_state,state):
    if new_internal_state=='water':
        self_state = WATER  
    elif new_internal_state=='soap':
        self_state = SOAP
    elif new_internal_state=='brush':
        self_state = BRUSH
    else:
        return state  
    new_state = RobotState(
        state.x,
        state.y,
        state.angle,
        self_state)
    transfer(('STATE',self_state))
    return new_state

# начало чистки
def start(transfer,state):
    transfer(('START WITH',state.state))
    return state

# конец чистки
def stop(transfer,state):
    transfer(('STOP',))
    return state


# интерпретация набора команд
def make(transfer,code,state):
    for command in code:
        cmd = command.split(' ')
        if cmd[0]=='move':
            state = move(transfer,int(cmd[1]),state) 
        elif cmd[0]=='turn':
            state = turn(transfer,int(cmd[1]),state)
        elif cmd[0]=='set':
            state = set_state(transfer,cmd[1],state) 
        elif cmd[0]=='start':
            state = start(transfer,state)
        elif cmd[0]=='stop':
            state = stop(transfer,state)
    return state
```

---

Реализуйте API к данному модулю так, как вы это понимаете. Каким конкретно техническим способом клиентам представляется доступ к этому API, мы не рассматриваем.
Ссылку на гитхаб введите в форму ниже:
