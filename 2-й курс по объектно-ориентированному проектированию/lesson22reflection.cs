/**
 *
// Пример наследования вида: военнослужащий армии. Там одновременно присутствует иерархия званий, иерархия должностей, иерархия подразделений.
// В определённых условиях нужны каждые из них и выделить первостепенную иерархию не представляется возможным.
// Каждая иерархия реализуется с помощью полиморфизма и наследования, а сам объект военнослужащего содержит соответствующие поля для каждой из иерархий

public abstract class Rank {};
public class Soldier: Rank {};
public class Foreman: Soldier {};
public class Ensign: Foreman {};
public class Lieutenant: Ensign {};
public class Captain: Lieutenant {};

public abstract class Position {};
public class Member: Position {};
public class Branch: Member {};
public class Platoon: Branch {};
public class Company: Platoon {};
public class Colonel: Company {};

public class MilitaryMan 
{
    public Rank Rank { get;set; }
    public Position Position { get;set; }
}

Java

interface Material {
     // материал для шитья
    void sew();
}

class Wool implements Material {
    @Override
    public void sew() {}
}

class Cotton implements Material {
    @Override
    public void sew() {}
}

interface Clothes {
    // материалы, из которых изготовлена вещь
    Material[] getMaterials();

    // надеть одежду
    void putOn();
    int getSize();
    String getBrand();
}

class Blouses implements Clothes {
    private final Material[] materials;
    private final String brand;
    private final int size;

    public Blouses(Material[] materials, String brand, int size) {
        this.materials = materials;
        this.brand = brand;
        this.size = size;
    }

    @Override
    public Material[] getMaterials() {
        return new Material[0];
    }

    @Override
    public void putOn() {}

    @Override
    public int getSize() {
      return this.size;
    }

    @Override
    public String getBrand() {
        return this.brand;
    }
}

class Trousers implements Clothes {

    private final Material[] materials;
    private final String brand;
    private final int size;

    public Trousers(Material[] materials, String brand, int size) {
        this.materials = materials;
        this.brand = brand;
        this.size = size;
    }

    @Override
    public Material[] getMaterials() {
        return new Material[0];
    }

    @Override
    public void putOn() {}

    @Override
    public int getSize() {
        return this.size;
    }

    @Override
    public String getBrand() {
        return this.brand;
    }
}
Имеется класс "Одежда", от которого наследуются классы "Брюки" и "Блузки".
Одежда предполагает несколько связанных сущностей, определяющих состояние вещи.
В частности, одежда характеризуется формой и материалом, из которого она сделана. Оба этих признака часто используются вместе.
Признак формы логично выделить основным: клиента интересует различие прежде всего между брюками и блузками, а не между материалами, из которых они сделаны.
Поэтому признак материала выделен в отдельную иерархию -- он находится в отношении композиции с классом одежда (одежда содержит материал).

Python

class Race():
    race_name = ''
    motherland = ''

class Elf(Race):
    race_name = 'elf'
    motherland = 'Nilfadiil'

class Orc(Race):
    race_name = 'orc'
    motherland = 'Grocks mountain'

#

class GameClass():
    def battle_method(self, target):
        raise NotImplementedError

class Wizard(GameClass):
    def battle_method(self, target):
        print(f'Phew-phew. Magick missle flying to {target}')

class Barbarian(GameClass):
    def battle_method(self, target):
        print(f'GRAAAA. My axe want to crack {target}\'s head!')

#

class Hero():
    def __init__(self, race, game_class):
        self.race = race
        self.game_class = game_class
Имеется класс Hero (Герой), у которого есть своя раса (бонусы к характеристикам и т.п.).
У героя также имеется один из игровых классов (боеввые навыки и т.п.).
Сперва напрашивается создать класс Wizard как наследник класса Hero, и дополнить атрибутом расы.
Однако на курсе уже не раз отмечалось, что в подобной ситуации атрибуты будут плодить лишние условные цепочки.
Поэтому создадим иерархию классов для характеристики расы и применим льготное наследование.

Так же применим наследование реализации и создадим иерархию классов для другой важной характеристики -- игрового класса.

Итог -- отдельный класс Hero, экземпляры которого содержат в себе классы Race и GameClass как атрибуты. Часто в игровых системах стартовые раса и класс определяют существенные бонусы и ограничения, накладываемые на игровой процесс, так что мы имеем несколько критериев классификации, как минимум одна из которых может меняться в течение игры (игровой класс). Race и GameClass в принципе равнозначны, и лучше добавлять их обоих композицией.


 */