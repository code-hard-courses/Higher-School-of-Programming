### 12. Конкатенативный стиль: возможное решение

**cleaner_api.py**

```
import pure_robot

class CatApi:

    def __init__(self):
        self.stack = [ pure_robot.RobotState(0.0, 0.0, 0, pure_robot.WATER) ]

    def exec(self, code):
        heap = code.split(' ')
        for itm in heap:
            self.compose(itm)               
        if len(self.stack) > 0:
            return self.stack[0]
        return None

    def compose(self, fn):
        if fn=='move':
            v = self.pop()
            state = self.pop()
            new_state = pure_robot.move(transfer,int(v), state) 
            self.push(new_state)
        elif fn=='turn':
            v = self.pop()
            state = self.pop()
            new_state = pure_robot.turn(transfer,int(v), state) 
            self.push(new_state)
        elif fn=='set':
            v = self.pop()
            state = self.pop()
            new_state = pure_robot.set_state(transfer,v, state) 
            self.push(new_state)
        elif fn=='start':
            state = self.pop()
            new_state = pure_robot.start(transfer, state) 
            self.push(new_state)
        elif fn=='stop':
            state = self.pop()
            new_state = pure_robot.stop(transfer, state) 
            self.push(new_state)
        else:
            self.push(fn)

    def pop(self):
        v = self.stack[-1]
        self.stack.pop()
        return v

    def push(self, v):
        self.stack.append(v)

def transfer(message):
    print (message)
```

**client.py**

```
from cat_api import CatApi

api = CatApi()
s = api.exec('100 move -90 turn soap set start 50 move stop')

print(s)
```


### 12. Конкатенативный стиль

Функциональную часть -- pure_robot.py, мы по-прежнему оставляем неизменной, что подтверждает правильность уже довольно давно выбранного подхода.

В учебном коде нету проверок на корректность входного потока, но в реальном проекте конечно их надо выполнять, контролировать наличие параметров в стеке и т. д.

Как устроена серверная часть -- класс CatApi? При инициализации в нём создаётся стек с одним элементом -- начальным состоянием. Команда exec() создаёт так называемую "кучу" (входной поток вычислений), который будет последовательно сканироваться, и далее выполняет его интерпретацию. Сама интерпретация сводится к прозаическому применению очередного элемента кучи к текущему содержимому стека (функция compose()). Для наших управляющих команд (move, turn, ...) функция compose извлекает (pop) из стека верхнее значение -- параметр команды (если он требуется) и текущее состояние, и вызывает для них соответствующую функцию вычисления нового состояния, которое помещается обратно на верх стека (push). Если же очередной элемент кучи -- обычное значение, оно просто помещается в стек. Вот и всё!

**Плюсы.**

Реализация очень проста, наглядна и эффективна.

Мутабелен только стек, но прямого доступа к нему не требуется: он используется по сути только для промежуточных вычислений.

**Минусы.**

Исходные команды надо записывать в обратной польской нотации. Впрочем, конвертор в неё из инфиксного стиля пишется без проблем.
