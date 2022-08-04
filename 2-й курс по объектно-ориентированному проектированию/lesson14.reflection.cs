/**
Сформируйте тип (класс) Vector<T> , над которым допустима операция сложения.
Cложите объекты типа Vector<Vector<Vector<T>>>

Python

class Any(General):

    ... 

    def __add__(self, other):
        """Summation"""
        raise NotImplementedError()

class Vector(Any):
    """
    >>> v1 = Vector(0, 1, 2)
    >>> v2 = Vector(7, 10, 15)
    >>> (v1 + v2).get_sequence_representation()
    (7, 11, 17)
    >>> v3 = Vector(3)
    >>> (v1 + v3) is Void
    True
    >>> v4 = Vector(6)
    >>> nested_v1 = Vector(Vector(v1), Vector(v3))
    >>> nested_v2 = Vector(Vector(v2), Vector(v4))
    >>> nested_v1.get_sequence_representation()
    (((0, 1, 2),), ((3,),))
    >>> nested_v2.get_sequence_representation()
    (((7, 10, 15),), ((6,),))
    >>> (nested_v1 + nested_v2).get_sequence_representation()
    (((7, 11, 17),), ((9,),))
    """

    def __init__(self, *args: t_Any, **kwargs):
        super().__init__(*args, **kwargs)
        self.sequence = args
        self._size = len(args)

    def __add__(self, other: 'Vector') -> Union['Vector', Void]:
        try:
            assert self._size == other._size
        except AssertionError:
            sum_vector = Void
        else:
            sum_vector = self._sum_vectors(other)
        return sum_vector

    def _sum_vectors(self, other: 'Vector') -> 'Vector':
        sequence_items = starmap(operator.add, zip(self.sequence, other.sequence))
        sum_vector = Vector(*sequence_items)
        return sum_vector

    def get_sequence_representation(self) -> tuple:
        """Get representation of all nested sequence (recursive)"""
        this_func_name = self.get_sequence_representation.__name__
        representation = tuple(
                getattr(item, this_func_name, lambda: item)()
                for item in self.sequence)
return representation
Java

public class Adder<T> extends Any {

    public T sum(T first, T second){

        T ans = null;

        if(first instanceof String){
            ans = sumString(first, second);
        }

        if(first instanceof Integer){
            ans = sumInteger(first, second);
        }

        if (first instanceof Double){
            ans = sumDouble(first, second);
        }

        return ans;
    }

    private T sumString(T first, T second){
        return (T)(first + (String)second);
    }

    private T sumInteger(T first, T second){
        Integer sum = (Integer)first + (Integer)second;
        T value = (T)sum;
        return value;
    }

    private T sumDouble(T first, T second){
        return (T)((Double)((Double)(first) + (Double)second));
    }
}

public class Vector<T> extends Adder {

    public static int ADD_NIL = 0;
    public static int ADD_OK = 1;
    public static int ADD_ERR = 2;

    private int length;
    private T[] arr;
    private int add_status;

    public Vector(T[] arr){
        this.arr = arr;
        length = arr.length;
        add_status = ADD_NIL;
    }

    public Vector(int length){
        arr = (T[])new Object[length];
        this.length = length;
        add_status = ADD_NIL;
    }

    public void add(Vector<? extends T> v){
        Vector<String> temp = new Vector<String>(1);
        if (v.getLength() == length){
            T[] arr2 = v.getArr();

            for (int i = 0; i < length; i++){
                if(arr2[i].getClass().isInstance(temp)){ 
                // проверяем типы. Если это Vector, то:
                    ((Vector<T>)arr[i]).add(((Vector<T>)arr2[i]));
                }
                else { // иначе - это Number или String
                    add_v((Vector<T>) v);
                    add_status = ADD_OK;
                    break;
                }
            }

            add_status = ADD_OK;
        }
        else{
            add_status = ADD_ERR;
        }
    }

    private void add_v(Vector<T> v){
        T[] arr2 = v.getArr();
        for(int i = 0; i < length; i++){
            arr[i] = (T) sum(arr[i], arr2[i]);
        }
    }

    public static Vector addVectors(Vector v1, Vector v2){
        Vector ans = (Vector)v1.deepCopy();

        ans.add(v2);

        return ans;
    }

    public int getLength(){
        return length;
    }

    public int get_add_status(){
        return add_status;
    }

    public T[] getArr(){
        return arr;
    }
}
*/

