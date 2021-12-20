### 11. Функциональная инъекция зависимостей-2

Сейчас мы организуем инъекцию зависимостей, в которой обойдёмся всего одной функцией вместо пяти. Для этого добавим функцию, которая будет вычислять нужную нам функцию в зависимости от запрошенной команды. При этом и различные реализации создаются прозрачно, с поддержкой полиморфизма.

**cleaner_api.py**

```
import pure_robot

class RobotApi:

    def setup(self, fn, transfer):
        self.f_transfer = transfer
        self.fn = fn

    def make(self, command):
        if not hasattr(self, 'cleaner_state'):
            self.cleaner_state = 
                 pure_robot.RobotState(0.0, 0.0, 0, pure_robot.WATER)

        cmd = command.split(' ')
        fun = self.fn(cmd)
        if cmd[0]=='move':
             self.cleaner_state = fun(self.f_transfer,int(cmd[1]), 
                                      self.cleaner_state) 
        elif cmd[0]=='turn':
            self.cleaner_state = fun(self.f_transfer,int(cmd[1]), 
                                     self.cleaner_state)
        elif cmd[0]=='set':
            self.cleaner_state = fun(self.f_transfer,cmd[1], 
                                     self.cleaner_state) 
        elif cmd[0]=='start':
            self.cleaner_state = fun(self.f_transfer, 
                                     self.cleaner_state)
        elif cmd[0]=='stop':
            self.cleaner_state = fun(self.f_transfer, 
                                     self.cleaner_state)
        return self.cleaner_state

    def __call__(self, command):
        return self.make(command)


def transfer_to_cleaner(message):
    print (message)

def double_move(transfer,dist,state):
    return pure_robot.move(transfer,dist*2,state)

def robotFn(cmd):
    if cmd[0]=='move':
        return pure_robot.move
    elif cmd[0]=='turn':
        return pure_robot.turn  
    elif cmd[0]=='set':
        return pure_robot.set_state 
    elif cmd[0]=='start':
        return pure_robot.start
    elif cmd[0]=='stop':
        return pure_robot.stop 
    return None

def robotFn2(cmd):
    if cmd[0]=='move':
        return double_move
    return robotFn(cmd)

api = RobotApi()  
# api.setup(robotFn, transfer_to_cleaner)
api.setup(robotFn2, transfer_to_cleaner)
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

**Плюсы.**

Мы разделили API и его реализацию через параметризацию.

**Точка развязки зависимостей теперь находится непосредственно в точке её использования** , а не в конструкторе, как было в объектно-ориентированной версии, в результате чего потенциальный рост зависимостей будет происходить линейно. В объектном же подходе жизненный цикл конструктора слабо предсказуем.

Мы сохраняем интерфейс в формате единственной команды api(), которой не требуется какое-то дооснащение при смене реализаций.

Наша схема прозрачна и не требует специальных паттернов типа Inversion of Control, когда вызовами управляет специальный фреймворк.

**Минусы.**

В общем случае при большом количестве зависимых функций схема с функцией, "вычисляющей" эти зависимости, может быть громоздкой и не слишком подходящей для поддержки серверного интерфейса.
