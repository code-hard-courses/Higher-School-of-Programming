### Процедурный стиль: возможное решение

```
import math

# входная программа управления роботом
code = (
    'move 100',
    'turn -90',
    'set soap',
    'start',
    'move 50',
    'stop'
)

# режимы работы устройства очистки
WATER = 1 # полив водой
SOAP  = 2 # полив мыльной пеной
BRUSH = 3 # чистка щётками

# текущий режим работы устройства очистки
state = WATER

# текущие позиция и угол (ориентация) робота
x = 0.0
y = 0.0
angle = 0
```

```
# взаимодействие с роботом вынесено в отдельную функцию
def transfer_to_cleaner(message):
    print (message)

# перемещение
def move(dist):
    global x,y,angle
    angle_rads = angle * (math.pi/180.0)
    x += dist * math.cos(angle_rads)
    y += dist * math.sin(angle_rads)
    transfer_to_cleaner(('POS(',x,',',y,')'))

# поворот
def turn(turn_angle):
    global angle
    angle += turn_angle
    transfer_to_cleaner(('ANGLE',angle))

# установка режима работы
def set_state(new_state):
    global state
    if new_state=='water':
        state = WATER  
    elif new_state=='soap':
        state = SOAP
    elif new_state=='brush':
        state = BRUSH
    transfer_to_cleaner(('STATE',state))

# начало чистки
def start():
    global state
    transfer_to_cleaner(('START WITH',state))

# остановка чистки
def stop():
    transfer_to_cleaner(('STOP',))

# главная программа
for command in code:
    cmd = command.split(' ')

    if cmd[0]=='move':
        move(int(cmd[1])) 

    elif cmd[0]=='turn':
        turn(int(cmd[1]))       

    elif cmd[0]=='set':
        set_state(cmd[1]) 

    elif cmd[0]=='start':
        start()

    elif cmd[0]=='stop':
        stop()
```

В нашем случае значительно нагляднее становится главный алгоритм, и вдобавок мы вынесли код потенциальной привязки нашей программы с модулем, управляющим роботом, в отдельную процедуру transfer_to_cleaner(). Сейчас в ней выполняется лишь демонстрационный вывод операции в консоль, но достаточно в неё добавить, например, код управления лего-роботом, и она сразу станет работоспособной, причём остальной код совершенно не изменится.

**Плюсы.**
Становится доступной вся мощь структурного подхода Дейкстры.

**Минусы.**
Активно используются глобальные мутабельные переменные. Сплошная изменчивость, перекрещивающиеся связи между переменными.
