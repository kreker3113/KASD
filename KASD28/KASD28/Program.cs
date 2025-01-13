using System;
using System.Collections;
using System.Collections.Generic;

public interface MyCollection<T>
{
    void Add(T e);
    void AddAll(MyCollection<T> c);
    void Clear();
    bool Contains(object o);
    bool ContainsAll(MyCollection<T> c);
    bool IsEmpty();
    bool Remove(object o);
    void RemoveAll(MyCollection<T> c);
    void RetainAll(MyCollection<T> c);
    int Size();
    T[] ToArray();
    T[] ToArray(T[] a);
}

public interface MyList<T> : MyCollection<T>
{
    void Add(int index, T e);
    void AddAll(int index, MyCollection<T> c);
    T Get(int index);
    int IndexOf(object o);
    int LastIndexOf(object o);
    IEnumerator<T> ListIterator();
    IEnumerator<T> ListIterator(int index);
    T Remove(int index);
    void Set(int index, T e);
    MyList<T> SubList(int fromIndex, int toIndex);
}

public interface MyQueue<T> : MyCollection<T>
{
    T Element();
    bool Offer(T obj);
    T Peek();
    T Poll();
}

public interface MyDeque<T> : MyCollection<T>
{
    void AddFirst(T obj);
    void AddLast(T obj);
    T GetFirst();
    T GetLast();
    bool OfferFirst(T obj);
    bool OfferLast(T obj);
    T Pop();
    void Push(T obj);
    T PeekFirst();
    T PeekLast();
    T PollFirst();
    T PollLast();
    bool RemoveLast();
    bool RemoveFirst();
    bool RemoveLastOccurrence(object obj);
    bool RemoveFirstOccurrence(object obj);
}

public interface MySet<T> : MyCollection<T>
{
    T First();
    T Last();
    MySet<T> SubSet(T fromElement, T toElement);
    MySet<T> HeadSet(T toElement);
    MySet<T> TailSet(T fromElement);
}

public interface MySortedSet<T> : MySet<T>
{
    // Дополнительные методы для сортированных множеств
}

public interface MyNavigableSet<T> : MySortedSet<T>
{
    // Методы для навигационных множеств
}

public interface MyMap<K, V>
{
    void Clear();
    bool ContainsKey(object key);
    bool ContainsValue(object value);
    MyCollection<KeyValuePair<K, V>> EntrySet();
    V Get(object key);
    bool IsEmpty();
    MyCollection<K> KeySet();
    void Put(K key, V value);
    void PutAll(MyMap<K, V> m);
    bool Remove(object key);
    int Size();
    MyCollection<V> Values();
}

public interface MySortedMap<K, V> : MyMap<K, V>
{
    // Дополнительные методы для сортированных карт
}

public interface MyNavigableMap<K, V> : MySortedMap<K, V>
{
    // Методы для навигационных карт
}



public class MyArrayList<T> : MyList<T>
{
    private T[] elementData;
    private int size;

    public MyArrayList()
    {
        elementData = new T[10];
        size = 0;
    }

    public MyArrayList(T[] a)
    {
        size = a.Length;
        elementData = new T[size * 2];
        Array.Copy(a, elementData, size);
    }

    public void Add(T e)
    {
        if (size == elementData.Length) Array.Resize(ref elementData, size * 2);
        elementData[size++] = e;
    }

    public void AddAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(item);
        }
    }

    public void Clear() => size = 0;

    public bool Contains(object o) => IndexOf(o) >= 0;

    public bool ContainsAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            if (!Contains(item)) return false;
        }
        return true;
    }

    public bool IsEmpty() => size == 0;

    public bool Remove(object o)
    {
        int index = IndexOf(o);
        if (index >= 0)
        {
            Remove(index);
            return true;
        }
        return false;
    }

    public void RemoveAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Remove(item);
        }
    }

    public void RetainAll(MyCollection<T> c)
    {
        for (int i = size - 1; i >= 0; i--)
        {
            if (!c.Contains(elementData[i]))
            {
                Remove(i);
            }
        }
    }

    public int Size() => size;

    public T[] ToArray() => elementData.Take(size).ToArray();

    public T[] ToArray(T[] a)
    {
        if (a.Length < size) return ToArray();
        Array.Copy(elementData, a, size);
        return a;
    }

    public void Add(int index, T e)
    {
        if (index < 0 || index > size) throw new ArgumentOutOfRangeException();
        Add(e); // Call Add to increase size
        for (int i = size - 1; i > index; i--)
        {
            elementData[i] = elementData[i - 1];
        }
        elementData[index] = e;
    }

    public void AddAll(int index, MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(index++, item);
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();
        return elementData[index];
    }

    public int IndexOf(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (elementData[i].Equals(o)) return i;
        }
        return -1;
    }

    public int LastIndexOf(object o)
    {
        for (int i = size - 1; i >= 0; i--)
        {
            if (elementData[i].Equals(o)) return i;
        }
        return -1;
    }

    public IEnumerator<T> ListIterator()
    {
        for (int i = 0; i < size; i++)
        {
            yield return elementData[i];
        }
    }

    public IEnumerator<T> ListIterator(int index)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();
        for (int i = index; i < size; i++)
        {
            yield return elementData[i];
        }
    }

    public T Remove(int index)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();
        T removedElement = elementData[index];
        for (int i = index; i < size - 1; i++)
        {
            elementData[i] = elementData[i + 1];
        }
        size--;
        elementData[size] = default(T);
        return removedElement;
    }

    public void Set(int index, T e)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();
        elementData[index] = e;
    }

    public MyList<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
            throw new ArgumentOutOfRangeException();
        MyArrayList<T> subList = new MyArrayList<T>();
        for (int i = fromIndex; i < toIndex; i++)
        {
            subList.Add(elementData[i]);
        }
        return subList;
    }
}
public class MyPriorityQueue<T> : MyQueue<T>
{
    private List<T> elements = new List<T>();

    public T Element()
    {
        if (IsEmpty()) throw new InvalidOperationException("Queue is empty");
        return Peek();
    }

    public bool Offer(T obj)
    {
        elements.Add(obj);
        elements.Sort(); // Assuming T implements IComparable
        return true;
    }

    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("Queue is empty");
        return elements[0];
    }

    public T Poll()
    {
        if (IsEmpty()) throw new InvalidOperationException("Queue is empty");
        T value = elements[0];
        elements.RemoveAt(0);
        return value;
    }

    public void Add(T e) => Offer(e);

    public void AddAll(MyCollection<T> c) => c.ToArray().ToList().ForEach(Add);

    public void Clear() => elements.Clear();

    public bool Contains(object o) => elements.Contains((T)o);

 

    public bool IsEmpty() => elements.Count == 0;

    public bool Remove(object o) => elements.Remove((T)o);

    public void RemoveAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Remove(item);
        }
    }

    public void RetainAll(MyCollection<T> c)
    {
        elements = elements.Where(e => c.Contains(e)).ToList();
    }

    public int Size() => elements.Count;

    public T[] ToArray() => elements.ToArray();

    public T[] ToArray(T[] a)
    {
        if (a.Length < Size())
            return ToArray();
        elements.CopyTo(a);
        return a;
    }

    public bool ContainsAll(MyCollection<T> c)
    {
        throw new NotImplementedException();
    }
}
public class MyArrayDeque<T> : MyDeque<T>
{
    private List<T> elements = new List<T>();

    public void AddFirst(T obj) => elements.Insert(0, obj);

    public void AddLast(T obj) => elements.Add(obj);

    public T GetFirst()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        return elements[0];
    }

    public T GetLast()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        return elements[elements.Count - 1];
    }

    public bool OfferFirst(T obj)
    {
        AddFirst(obj);
        return true;
    }

    public bool OfferLast(T obj)
    {
        AddLast(obj);
        return true;
    }

    public T Pop()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        T value = GetLast();
        elements.RemoveAt(elements.Count - 1);
        return value;
    }

    public void Push(T obj) => AddFirst(obj);

    public T PeekFirst()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        return GetFirst();
    }

    public T PeekLast()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        return GetLast();
    }

    public T PollFirst()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        T value = GetFirst();
        elements.RemoveAt(0);
        return value;
    }

    public T PollLast()
    {
        if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
        T value = GetLast();
        elements.RemoveAt(elements.Count - 1);
        return value;
    }


    public bool RemoveLastOccurrence(object obj) => elements.Remove((T)obj);

    public bool RemoveFirstOccurrence(object obj) => elements.Remove((T)obj);

    public void Add(T e) => AddLast(e);

    public void AddAll(MyCollection<T> c) => c.ToArray().ToList().ForEach(Add);

    public void Clear() => elements.Clear();

    public bool Contains(object o) => elements.Contains((T)o);



    public bool IsEmpty() => elements.Count == 0;

    public bool Remove(object o) => elements.Remove((T)o);

    public void RemoveAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Remove(item);
        }
    }

    public void RetainAll(MyCollection<T> c)
    {
        elements = elements.Where(e => c.Contains(e)).ToList();
    }

    public int Size() => elements.Count;

    public T[] ToArray() => elements.ToArray();

    public T[] ToArray(T[] a)
    {
        if (a.Length < Size())
            return ToArray();
        elements.CopyTo(a);
        return a;
    }

    public bool RemoveLast()
    {
        throw new NotImplementedException();
    }

    public bool RemoveFirst()
    {
        throw new NotImplementedException();
    }

    public bool ContainsAll(MyCollection<T> c)
    {
        throw new NotImplementedException();
    }
}
public class MyHashSet<T> : MySet<T>
{
    private HashSet<T> set = new HashSet<T>();

    public void Add(T e) => set.Add(e);

    public void AddAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Add(item);
        }
    }

    public void Clear() => set.Clear();

    public bool Contains(object o) => set.Contains((T)o);

    public bool ContainsAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            if (!Contains(item)) return false;
        }
        return true;
    }

    public bool IsEmpty() => set.Count == 0;

    public bool Remove(object o) => set.Remove((T)o);

    public void RemoveAll(MyCollection<T> c)
    {
        foreach (var item in c.ToArray())
        {
            Remove(item);
        }
    }

    public void RetainAll(MyCollection<T> c)
    {
        set = new HashSet<T>(set.Where(e => c.Contains(e)));
    }

    public int Size() => set.Count;

    public T[] ToArray() => set.ToArray();

    public T[] ToArray(T[] a)
    {
        if (a.Length < Size())
            return ToArray();
        set.CopyTo(a);
        return a;
    }

    public T First()
    {
        if (IsEmpty()) throw new InvalidOperationException("Set is empty");
        return set.First();
    }

    public T Last()
    {
        if (IsEmpty()) throw new InvalidOperationException("Set is empty");
        return set.Last();
    }

    public MySet<T> SubSet(T fromElement, T toElement)
    {
        throw new NotImplementedException();
    }

    public MySet<T> HeadSet(T toElement)
    {
        throw new NotImplementedException();
    }

    public MySet<T> TailSet(T fromElement)
    {
        throw new NotImplementedException();
    }
}
public class MyHashMap<K, V> : MyMap<K, V>
{
    private Dictionary<K, V> map = new Dictionary<K, V>();

    public void Clear() => map.Clear();

    public bool ContainsKey(object key) => map.ContainsKey((K)key);

    public bool ContainsValue(object value) => map.ContainsValue((V)value);

    public MyCollection<KeyValuePair<K, V>> EntrySet()
    {
        throw new NotImplementedException();
    }

    public V Get(object key)
    {
        map.TryGetValue((K)key, out V value);
        return value;
    }

    public bool IsEmpty() => map.Count == 0;

    public MyCollection<K> KeySet()
    {
        throw new NotImplementedException();
    }

    public void Put(K key, V value) => map[key] = value;

    public void PutAll(MyMap<K, V> m)
    {
        throw new NotImplementedException();
    }

    public bool Remove(object key) => map.Remove((K)key);

    public int Size() => map.Count;

    public MyCollection<V> Values()
    {
        return new MyArrayList<V>(map.Values.ToArray());
    }
}
public class MyTreeMap<K, V> : MyNavigableMap<K, V>
{
    private SortedDictionary<K, V> map = new SortedDictionary<K, V>();

    public void Clear() => map.Clear();

    public bool ContainsKey(object key) => map.ContainsKey((K)key);

    public bool ContainsValue(object value) => map.ContainsValue((V)value);

    public MyCollection<KeyValuePair<K, V>> EntrySet()
    {
        throw new NotImplementedException();
    }

    public V Get(object key)
    {
        map.TryGetValue((K)key, out V value);
        return value;
    }

    public bool IsEmpty() => map.Count == 0;

    public MyCollection<K> KeySet()
    {
        throw new NotImplementedException();
    }

    public void Put(K key, V value) => map[key] = value;
 

    public bool Remove(object key) => map.Remove((K)key);

    public int Size() => map.Count;

    public MyCollection<V> Values()
    {
        return new MyArrayList<V>(map.Values.ToArray());
    }

    public MySortedMap<K, V> HeadMap(K end)
    {
        throw new NotImplementedException();
    }

    public MySortedMap<K, V> SubMap(K start, K end)
    {
        throw new NotImplementedException();
    }

    public MySortedMap<K, V> TailMap(K start)
    {
        throw new NotImplementedException();
    }

    public void PutAll(MyMap<K, V> m)
    {
        throw new NotImplementedException();
    }
}






public class CollectionException : Exception
{
    public CollectionException(string message) : base(message) { }
}

public class ElementNotFoundException : CollectionException
{
    public ElementNotFoundException(string message) : base(message) { }
}

public class InvalidOperationException : CollectionException
{
    public InvalidOperationException(string message) : base(message) { }
}


public class Program
{
    public static void Main()
    {
        // Пример использования MyArrayList
        MyArrayList<int> list = new MyArrayList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Console.WriteLine("Size of list: " + list.Size());
        Console.WriteLine("Contains 2: " + list.Contains(2));

        // Пример использования MyHashMap
        MyHashMap<string, int> map = new MyHashMap<string, int>();
        map.Put("one", 1);
        map.Put("two", 2);
        Console.WriteLine("Value for 'one': " + map.Get("one"));

        // Пример использования MyPriorityQueue
        MyPriorityQueue<int> priorityQueue = new MyPriorityQueue<int>();
        priorityQueue.Offer(5);
        priorityQueue.Offer(1);
        Console.WriteLine("Peek: " + priorityQueue.Peek());
        Console.WriteLine("Poll: " + priorityQueue.Poll());

        // Пример использования MyArrayDeque
        MyArrayDeque<int> deque = new MyArrayDeque<int>();
        deque.AddFirst(1);
        deque.AddLast(2);
        Console.WriteLine("First: " + deque.GetFirst());
        Console.WriteLine("Last: " + deque.GetLast());
    }
}
