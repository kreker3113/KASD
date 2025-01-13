using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

public interface MyIterator<T>
{
    bool HasNext();
    T Next();
    bool HasPrevious();
    T Previous();
    int NextIndex();
    int PreviousIndex();
    void Remove();
    void Set(T element);
    void Add(T element);
}
public class MyPriorityQueue<T>
{
    private T[] queue;
    private int size;

    public MyPriorityQueue(int capacity)
    {
        queue = new T[capacity];
        size = 0;
    }

    public void Enqueue(T item)
    {
        if (size >= queue.Length)
            throw new InvalidOperationException("Очередь переполнена.");
        queue[size++] = item; // Упрощенная логика добавления
    }

    public class MyItr : MyIterator<T>
    {
        private MyPriorityQueue<T> priorityQueue;
        private int cursor = 0;

        public MyItr(MyPriorityQueue<T> queue)
        {
            this.priorityQueue = queue;
        }

        public bool HasNext() => cursor < priorityQueue.size;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            return priorityQueue.queue[cursor++];
        }

        public bool HasPrevious() => cursor > 0;

        public T Previous()
        {
            if (!HasPrevious())
                throw new InvalidOperationException("Нет предыдущего элемента.");
            return priorityQueue.queue[--cursor];
        }

        public int NextIndex() => cursor;

        public int PreviousIndex() => cursor - 1;

        public void Remove()
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для удаления.");
            // Логика удаления элемента
            cursor--;
            priorityQueue.size--; // Упрощенная логика уменьшения размера
        }

        public void Set(T element)
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для замены.");
            priorityQueue.queue[cursor - 1] = element;
        }

        public void Add(T element)
        {
            // Логика добавления элемента
            priorityQueue.Enqueue(element);
            cursor++;
        }
    }

    public MyItr Iterator() => new MyItr(this);
}
public class MyArrayDeque<T>
{
    private T[] deque;
    private int size;

    public MyArrayDeque(int capacity)
    {
        deque = new T[capacity];
        size = 0;
    }

    public void AddFirst(T item)
    {
        if (size >= deque.Length)
            throw new InvalidOperationException("Дек переполнен.");
        // Логика добавления в начало
        for (int i = size; i > 0; i--)
        {
            deque[i] = deque[i - 1];
        }
        deque[0] = item;
        size++;
    }

    public void AddLast(T item)
    {
        if (size >= deque.Length)
            throw new InvalidOperationException("Дек переполнен.");
        deque[size++] = item;
    }

    public class MyItr : MyIterator<T>
    {
        private MyArrayDeque<T> deque;
        private int cursor = 0;

        public MyItr(MyArrayDeque<T> deque)
        {
            this.deque = deque;
        }

        public bool HasNext() => cursor < deque.size;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            return deque.deque[cursor++];
        }

        public bool HasPrevious() => cursor > 0;

        public T Previous()
        {
            if (!HasPrevious())
                throw new InvalidOperationException("Нет предыдущего элемента.");
            return deque.deque[--cursor];
        }

        public int NextIndex() => cursor;

        public int PreviousIndex() => cursor - 1;

        public void Remove()
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для удаления.");
            // Логика удаления элемента
            cursor--;
            deque.size--; // Упрощенная логика уменьшения размера
        }

        public void Set(T element)
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для замены.");
            deque.deque[cursor - 1] = element;
        }

        public void Add(T element)
        {
            // Логика добавления элемента
            deque.AddFirst(element);
            cursor++;
        }
    }

    public MyItr ListIterator() => new MyItr(this);
}
public class MyHashMap<K, V>
{
    private class Node
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node Next { get; set; }

        public Node(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }

    /*private V t;
    private K r*/
    private Node[] table;
    private int size;
    private double loadFactor;
    public MyHashMap()
    {
        table = new Node[16];
        size = 16;
        loadFactor = 0.75;
    }
    public MyHashMap(int initialCapacity)
    {
        table = new Node[initialCapacity];
        size = initialCapacity;
        loadFactor = 0.75;
    }
    public MyHashMap(int initialCapacity, double loadFactorr)
    {
        table = new Node[initialCapacity];
        size = initialCapacity;
        loadFactor = loadFactorr;
    }
    private int GetHashCode(K key)
    {
        return Math.Abs(key.GetHashCode()) % size;
    }
    private int GetHashCode(V key)
    {
        return Math.Abs(key.GetHashCode()) % size;
    }
    public void Clear() { size = 0; }
    public bool ContainsKey(K key)
    {
        int index = GetHashCode(key);
        Node current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }
    public bool ContainsValue(V value)
    {
        int index = GetHashCode(value);
        Node current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(value))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public IEnumerable<KeyValuePair<K, V>> EntrySet()
    {
        for (int i = 0; i < size - 5; i++)
        {
            Node current = table[i];
            while (current != null)
            {
                yield return new KeyValuePair<K, V>(current.Key, current.Value);
                current = current.Next;
            }
        }
    }
    public V Get(K key)
    {
        int index = GetHashCode(key);
        Node current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                return current.Value;
            }
            current = current.Next;
        }

        throw new KeyNotFoundException("Ключ не найден.");
    }
    public bool IsEmpty() { return size == 0; }
    public K[] KeySet()
    {
        K[] t = new K[size];
        for (int i = 0; i < size; i++)
        {
            Node current = table[i];
            while (current != null)
            {
                t[i] = current.Key;
                current = current.Next;
            }
        }
        return t;
    }
    public void Put(K key, V value)
    {
        int index = GetHashCode(key);
        int k = -1;
        if (table[index] != null)
        {
            while (table != null)
            {
                if (k == index) k = index;

            }
            if (k == -1)
            {
                Node newNode = new Node(key, value);
                newNode.Next = table[index];
                table[index] = newNode;
            }
        }
        else
        {
            table[index] = new Node(key, value);
        }

        size++;
    }
    public void Remove(K key)
    {
        int index = GetHashCode(key);

        // Если в индексе нет значения, ничего не делаем
        if (table[index] == null)
        {
            return;
        }

        // Если удаляемый ключ - первый в списке
        if (table[index].Key.Equals(key))
        {
            table[index] = table[index].Next;
            size--;
            return;
        }

        // Ищем ключ в списке
        Node current = table[index];
        Node previous = null;
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                previous.Next = current.Next;
                size--;
                return;
            }

            previous = current;
            current = current.Next;
        }
    }
    public int Size() { return size; }
}
public class MyHashSet<T>
{
    private HashSet<T> set;

    public MyHashSet()
    {
        set = new HashSet<T>();
    }

    public void Add(T item)
    {
        set.Add(item);
    }

    public class MyItr : MyIterator<T>
    {
        private IEnumerator<T> enumerator;

        public MyItr(MyHashSet<T> hashSet)
        {
            enumerator = hashSet.set.GetEnumerator();
        }

        public bool HasNext() => enumerator.MoveNext();

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            return enumerator.Current;
        }

        public bool HasPrevious() => false; // Для HashSet нет предыдущего элемента

        public T Previous() => throw new InvalidOperationException("Нет предыдущего элемента.");

        public int NextIndex() => throw new NotSupportedException();

        public int PreviousIndex() => throw new NotSupportedException();

        public void Remove()
        {
            enumerator.Dispose();
        }

        public void Set(T element) => throw new NotSupportedException();

        public void Add(T element) => throw new NotSupportedException();
    }

    public MyItr Iterator() => new MyItr(this);
}
public class MyTreeSet<T> where T : IComparable<T>
{
    private SortedSet<T> set;

    public MyTreeSet()
    {
        set = new SortedSet<T>();
    }

    public void Add(T item)
    {
        set.Add(item);
    }

    public class MyItr : MyIterator<T>
    {
        private IEnumerator<T> enumerator;

        public MyItr(MyTreeSet<T> treeSet)
        {
            enumerator = treeSet.set.GetEnumerator();
        }

        public bool HasNext() => enumerator.MoveNext();

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            return enumerator.Current;
        }

        public bool HasPrevious() => false; // Для TreeSet нет предыдущего элемента

        public T Previous() => throw new InvalidOperationException("Нет предыдущего элемента.");

        public int NextIndex() => throw new NotSupportedException();

        public int PreviousIndex() => throw new NotSupportedException();

        public void Remove()
        {
            enumerator.Dispose();
        }

        public void Set(T element) => throw new NotSupportedException();

        public void Add(T element) => throw new NotSupportedException();
    }

    public MyItr Iterator() => new MyItr(this);
}
public class MyArrayList<T>
{
    private T[] array;
    private int size;

    public MyArrayList(int capacity)
    {
        array = new T[capacity];
        size = 0;
    }

    public void Add(T item)
    {
        if (size >= array.Length)
            throw new InvalidOperationException("Список переполнен.");
        array[size++] = item;
    }

    public class MyItr : MyIterator<T>
    {
        private MyArrayList<T> arrayList;
        private int cursor = 0;

        public MyItr(MyArrayList<T> list)
        {
            this.arrayList = list;
        }

        public bool HasNext() => cursor < arrayList.size;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            return arrayList.array[cursor++];
        }

        public bool HasPrevious() => cursor > 0;

        public T Previous()
        {
            if (!HasPrevious())
                throw new InvalidOperationException("Нет предыдущего элемента.");
            return arrayList.array[--cursor];
        }

        public int NextIndex() => cursor;

        public int PreviousIndex() => cursor - 1;

        public void Remove()
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для удаления.");
            cursor--;
            arrayList.size--; // Упрощенная логика уменьшения размера
        }

        public void Set(T element)
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для замены.");
            arrayList.array[cursor - 1] = element;
        }

        public void Add(T element)
        {
            arrayList.Add(element);
            cursor++;
        }
    }

    public MyItr ListIterator() => new MyItr(this);
}
public class MyVector<T>
{
    private T[] array;
    private int size;

    public MyVector(int capacity)
    {
        array = new T[capacity];
        size = 0;
    }

    public void Add(T item)
    {
        if (size >= array.Length)
            throw new InvalidOperationException("Вектор переполнен.");
        array[size++] = item;
    }

    public class MyItr : MyIterator<T>
    {
        private MyVector<T> vector;
        private int cursor = 0;

        public MyItr(MyVector<T> vector)
        {
            this.vector = vector;
        }

        public bool HasNext() => cursor < vector.size;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            return vector.array[cursor++];
        }

        public bool HasPrevious() => cursor > 0;

        public T Previous()
        {
            if (!HasPrevious())
                throw new InvalidOperationException("Нет предыдущего элемента.");
            return vector.array[--cursor];
        }

        public int NextIndex() => cursor;

        public int PreviousIndex() => cursor - 1;

        public void Remove()
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для удаления.");
            cursor--;
            vector.size--; // Упрощенная логика уменьшения размера
        }

        public void Set(T element)
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для замены.");
            vector.array[cursor - 1] = element;
        }

        public void Add(T element)
        {
            vector.Add(element);
            cursor++;
        }
    }

    public MyItr ListIterator() => new MyItr(this);
}
public class MyLinkedList<T>
{
    private class Node
    {
        public T Value;
        public Node Next;
        public Node Prev;

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node head;
    private Node tail;
    private int size;

    public MyLinkedList()
    {
        head = null;
        tail = null;
        size = 0;
    }

    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
        size++;
    }

    public class MyItr : MyIterator<T>
    {
        private MyLinkedList<T> list;
        private Node current;
        private int cursor = 0;

        public MyItr(MyLinkedList<T> list)
        {
            this.list = list;
            current = list.head;
        }

        public bool HasNext() => cursor < list.size;

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Нет следующего элемента.");
            T value = current.Value;
            current = current.Next;
            cursor++;
            return value;
        }

        public bool HasPrevious() => cursor > 0;

        public T Previous()
        {
            if (!HasPrevious())
                throw new InvalidOperationException("Нет предыдущего элемента.");
            current = current.Prev;
            cursor--;
            return current.Value;
        }

        public int NextIndex() => cursor;

        public int PreviousIndex() => cursor - 1;

        public void Remove()
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для удаления.");
            // Логика удаления элемента
            cursor--;
            list.size--; // Упрощенная логика уменьшения размера
        }

        public void Set(T element)
        {
            if (cursor == 0)
                throw new InvalidOperationException("Нет элемента для замены.");
            // Логика замены элемента
        }

        public void Add(T element)
        {
            list.Add(element);
            cursor++;
        }
    }

    public MyItr ListIterator() => new MyItr(this);
}

class Program
{
    static void Main()
    {
        TestMyPriorityQueue();
        TestMyArrayDeque();
        TestMyArrayList();
        TestMyVector();
        TestMyLinkedList();
    }

    static void TestMyPriorityQueue()
    {
        Console.WriteLine("Testing MyPriorityQueue:");
        var queue = new MyPriorityQueue<int>(5);
        queue.Enqueue(5);
        queue.Enqueue(3);
        queue.Enqueue(8);

        var iterator = queue.Iterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        Console.WriteLine();
    }

    static void TestMyArrayDeque()
    {
        Console.WriteLine("Testing MyArrayDeque:");
        var deque = new MyArrayDeque<int>(5);
        deque.AddFirst(1);
        deque.AddLast(2);
        deque.AddFirst(0);

        var iterator = deque.ListIterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        Console.WriteLine();
    }


    static void TestMyTreeSet()
    {
        Console.WriteLine("Testing MyTreeSet:");
        var treeSet = new MyTreeSet<int>();
        treeSet.Add(5);
        treeSet.Add(3);
        treeSet.Add(8);

        var iterator = treeSet.Iterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        Console.WriteLine();
    }

    static void TestMyArrayList()
    {
        Console.WriteLine("Testing MyArrayList:");
        var arrayList = new MyArrayList<string>(5);
        arrayList.Add("one");
        arrayList.Add("two");
        arrayList.Add("three");

        var iterator = arrayList.ListIterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        Console.WriteLine();
    }

    static void TestMyVector()
    {
        Console.WriteLine("Testing MyVector:");
        var vector = new MyVector<double>(5);
        vector.Add(1.1);
        vector.Add(2.2);
        vector.Add(3.3);

        var iterator = vector.ListIterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        Console.WriteLine();
    }

    static void TestMyLinkedList()
    {
        Console.WriteLine("Testing MyLinkedList:");
        var linkedList = new MyLinkedList<char>();
        linkedList.Add('A');
        linkedList.Add('B');
        linkedList.Add('C');

        var iterator = linkedList.ListIterator();
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
        Console.WriteLine();
    }
}
