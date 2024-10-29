using System;

public class MyVector<T>
{
    private T[] elementData;
    private int elementCount;
    private int capacityIncrement;

    // Конструктор с начальной ёмкостью и приращением
    public MyVector(int initialCapacity, int capacityIncrement)
    {
        elementData = new T[initialCapacity];
        this.capacityIncrement = capacityIncrement;
        elementCount = 0;
    }

    // Конструктор с начальной ёмкостью по умолчанию (0)
    public MyVector(int initialCapacity) : this(initialCapacity, 0) { }

    // Конструктор с начальной ёмкостью и ёмкостью по умолчанию (10, 0)
    public MyVector() : this(10, 0) { }

    // Конструктор из массива
    public MyVector(T[] a)
    {
        elementData = new T[a.Length];
        Array.Copy(a, elementData, a.Length);
        elementCount = a.Length;
    }

    // Метод добавления элемента
    public void Add(T e)
    {
        EnsureCapacity();
        elementData[elementCount++] = e;
    }

    // Метод добавления массива
    public void AddAll(T[] a)
    {
        foreach (var e in a)
        {
            Add(e);
        }
    }

    // Метод очистки вектора
    public void Clear()
    {
        elementCount = 0;
    }

    // Метод проверки наличия объекта
    public bool Contains(object o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (elementData[i]?.Equals(o) == true)
                return true;
        }
        return false;
    }

    // Метод проверки наличия всех объектов
    public bool ContainsAll(T[] a)
    {
        foreach (var e in a)
        {
            if (!Contains(e)) return false;
        }
        return true;
    }

    // Метод проверки на пустоту
    public bool IsEmpty() => elementCount == 0;

    // Метод удаления объекта
    public bool Remove(object o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (elementData[i]?.Equals(o) == true)
            {
                RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    // Метод удаления всех объектов
    public void RemoveAll(T[] a)
    {
        foreach (var e in a)
        {
            Remove(e);
        }
    }

    // Метод оставления только указанных объектов
    public void RetainAll(T[] a)
    {
        for (int i = elementCount - 1; i >= 0; i--)
        {
            if (!Array.Exists(a, elem => elem.Equals(elementData[i])))
            {
                RemoveAt(i);
            }
        }
    }

    // Метод получения размера вектора
    public int Size() => elementCount;

    // Метод преобразования в массив
    public T[] ToArray()
    {
        T[] result = new T[elementCount];
        Array.Copy(elementData, result, elementCount);
        return result;
    }

    // Метод преобразования в массив с параметром
    public T[] ToArray(ref T[] a)
    {
        if (a == null || a.Length < elementCount)
        {
            a = ToArray(); 
        }
        Array.Copy(elementData, a, elementCount); 
        return a;  
    }

    // Метод добавления по индексу
    public void Add(int index, T e)
    {
        if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException();
        EnsureCapacity();
        Array.Copy(elementData, index, elementData, index + 1, elementCount - index);
        elementData[index] = e;
        elementCount++;
    }

    // Метод добавления массива по индексу
    public void AddAll(int index, T[] a)
    {
        foreach (var e in a)
        {
            Add(index++, e);
        }
    }

    // Метод получения элемента по индексу
    public T Get(int index)
    {
        if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException();
        return elementData[index];
    }

    // Метод для получения индекса объекта
    public int IndexOf(object o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (elementData[i]?.Equals(o) == true) return i;
        }
        return -1;
    }
    // Метод получения последнего индекса объекта
    public int LastIndexOf(object o)
    {
        for (int i = elementCount - 1; i >= 0; i--)
        {
            if (elementData[i]?.Equals(o) == true) return i;
        }
        return -1;
    }

    // Метод удаления по индексу
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException();
        T removedElement = elementData[index];
        Array.Copy(elementData, index + 1, elementData, index, elementCount - index - 1);
        elementCount--;
        return removedElement;
    }

    // Метод замены элемента по индексу
    public T Set(int index, T e)
    {
        if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException();
        T oldElement = elementData[index];
        elementData[index] = e;
        return oldElement;
    }

    // Метод получения подсписка
    public MyVector<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > elementCount || fromIndex > toIndex)
            throw new ArgumentOutOfRangeException();

        MyVector<T> subList = new MyVector<T>();
        for (int i = fromIndex; i < toIndex; i++)
        {
            subList.Add(elementData[i]);
        }
        return subList;
    }

    // Метод для первого элемента
    public T FirstElement()
    {
        if (IsEmpty()) throw new InvalidOperationException();
        return elementData[0];
    }

    // Метод для последнего элемента
    public T LastElement()
    {
        if (IsEmpty()) throw new InvalidOperationException();
        return elementData[elementCount - 1];
    }

    // Метод для удаления элемента по индексу
    public void RemoveElementAt(int pos)
    {
        RemoveAt(pos);
    }

    // Метод для удаления диапазона
    public void RemoveRange(int begin, int end)
    {
        if (begin < 0 || end > elementCount || begin > end)
            throw new ArgumentOutOfRangeException();

        for (int i = end - 1; i >= begin; i--)
        {
            RemoveAt(i);
        }
    }

    // Увеличение ёмкости
    private void EnsureCapacity()
    {
        if (elementCount < elementData.Length) return;

        int newCapacity = capacityIncrement > 0 ? elementData.Length + capacityIncrement : elementData.Length * 2;
        Array.Resize(ref elementData, newCapacity);
    }
}
class Program
{
    static void Main()
    {
        // Создание вектора с начальной ёмкостью 5 и увеличением ёмкости 2
        MyVector<int> intVector = new MyVector<int>(5, 2);

        // Добавление элементов
        intVector.Add(1);
        intVector.Add(2);
        intVector.Add(3);

        // Проверка размера
        Console.WriteLine("Size: " + intVector.Size()); // Size: 3

        // Получение элемента по индексу
        Console.WriteLine("Element at index 1: " + intVector.Get(1)); // Element at index 1: 2

        // Удаление элемента
        intVector.Remove(2);
        Console.WriteLine("Contains 2: " + intVector.Contains(2)); // Contains 2: False

        // Чистка вектора
        intVector.Clear();
        Console.WriteLine("Is empty: " + intVector.IsEmpty()); // Is empty: True

        // Добавление массива элементов
        intVector.AddAll(new int[] { 4, 5, 6 });

        // Конвертация в массив
        int[] array = intVector.ToArray();
        Console.WriteLine("Array: " + string.Join(", ", array)); // Array: 4, 5, 6

        // Получение первых и последних элементов
        Console.WriteLine("First element: " + intVector.FirstElement()); // First element: 4
        Console.WriteLine("Last element: " + intVector.LastElement()); // Last element: 6

        // Получение индекса элемента
        Console.WriteLine("Index of 5: " + intVector.IndexOf(5)); // Index of 5: 1
    }
}