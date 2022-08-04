Java

Наследование реализации: класс "диаграмма классов" наследует реализацию класса "дерево", так как для хранения дерева иерархии классоа не требуется особая структура данных. Достаточно использовать стандартное дерево, возможно, расширив его небольшим числом специфичных операций.

class Tree<T> {

    public Tree(T rootData) { ... }
    public void add(T parent, T value) { ... }
    private Node<T> searchNode(Node<T> currentNode, T value) { ... }
   }
}

class ClassDiagram extends Tree<Class> {

    public ClassDiagram(Class rootData) {
        super(rootData);
    }
}
Льготное наследование: наследование класса FailedAmazonS3Response от класса HttpResponse. Ответ про сбой от хранилища Amazon S3 -- по сути, обычный ответ на http-запрос, который внутри себя содержит как стандарные сведения, так и дополнительную уточняющую информацию.

class HttpResponse {
    private int statusCode;
    private byte[] body;

    public HttpResponse(int statusCode, byte[] body) {
        this.statusCode = statusCode;
        this.body = body;
    }
}

class FailedAmazonS3Response extends HttpResponse {
    private String key;
    private String bucketName;
    private String region;

    public FailedAmazonS3Response(String key, String bucketName, 
            String region, int statusCode, byte[] body) {
        super(statusCode, body);
        this.key = key;
        this.bucketName = bucketName;
        this.region = region;
    }
}
Python

Наследование реализации: Имеется класс Creature, и потребовалось добавить расу, которая накапливает статусы состояния, и не расходует их по мере течения игрового времени. При этом такой режим можно включать и выключать.
Данный пример похож на наследование с расширением. Разница прежде всего в акцентах: в случае наследования с расширением мы вообще не задумываемся о реализации, а только расширяем набор операций. В случае наследования реализации по каким-то причинам решено было её учитывать, и в целом этого желательно всегда избегать, хотя на практике удаётся далеко не всегда.

class Creature():
    def __init__(self, hp):
        self.hp = hp
        self.resistances = {}
        self.statuses = []

    def end_turn(self):
        for status in self.statuses.copy():
            status.tick(self)
            if status.duration <= 0:
                self.statuses.remove(status)

class AnotherCreature(Creature):
    def __init__(self, hp):
        super().__init__(hp)
        self.suppress = True

    def end_turn(self):
        if not self.suppress:
            super().end_turn
Льготное наследование: Класс-концовка игры. Мы создаем класс-событие, выполняющее нужные действия при завершении игры, и наследуемся от него, задавая новое описание. Например, если в игре потребуется создать тупиковую ветку, приводящую к концу игры, мы просто создадим наследника корневого завершения с новым описанием.
class GameOver():
    same_text = ''

    def __init__(self):
        print(self.same_text)
        print('Game Over!')


class HappyEnding(GameOver):
    same_text = 'And they lived happily ever after'


class BadEnding(GameOver):
    same_text = 'And died on the same day. Today.'