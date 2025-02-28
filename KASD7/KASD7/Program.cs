﻿class MyVector<T>
{
    private int elementCount;
    private int capacityIncrement;
    T[]? elementData;

    public MyVector(int initialCapacity, int initialcapacityIncrement)
    {
        elementData = new T[initialCapacity];
        elementCount = initialCapacity;
        capacityIncrement = initialcapacityIncrement;
    }

    public MyVector(int initialCapacity)
    {
        elementData = new T[initialCapacity];
        capacityIncrement = 0;
    }

    public MyVector()
    {
        elementData = null;
        capacityIncrement = 0;
        elementCount = 10;
    }

    public MyVector(T[] a)
    {
        elementData = new T[(int)(a.Length)];
        for (int i = 0; i < a.Length; i++)
            elementData[i] = a[i];
        elementCount = a.Length;
    }

    public void Add(T e)
    {
        if (elementData == null)
        {
            elementData = new T[capacityIncrement > 0 ? capacityIncrement : 10];
        }

        if (elementCount == elementData.Length)
        {
            T[]? newArray = null;
            if (capacityIncrement != 0) newArray = new T[elementCount + capacityIncrement];
            else newArray = new T[elementCount * 2];
            for (int i = 0; i < elementCount; i++) newArray[i] = elementData[i];
            elementData = newArray;
        }
        elementData[elementCount] = e;
        elementCount++;
    }

    public void AddAll(T[] a)
    {
        foreach (T item in a)
            Add(item);
    }

    public int Size()
    {
        return elementCount;
    }

    public T Get(int index)
    {
        if (elementData == null)
            throw new InvalidOperationException("elementData is Empty");

        if (index < 0 || index >= elementCount)
            throw new ArgumentOutOfRangeException(nameof(index));
        return elementData[index];
    }

    public void Print()
    {
        if (elementData != null)
        {
            Console.Write("Vector:");
            for (int i = 0; i < elementCount; i++)
                if (elementData[i] != null)
                    Console.Write($" {elementData[i]}");
            Console.WriteLine();
        }
        else Console.WriteLine("Empty vector");
    }
}

class Program
{
    static bool CorrectPart(string part)
    {
        if (part.Length > 1 && part[0] == '0')
            return false;
        bool intper = int.TryParse(part, out int number);
        if (intper == true && number >= 0 && number <= 255)
            return true;
        return false;
    }

    static bool CorrectIp(string ip)
    {
        string[] parts = ip.Split('.');
        if (parts.Length != 4)
            return false;
        foreach (var part in parts)
        {
            if (!CorrectPart(part))
                return false;
        }
        return true;
    }

    static void Main(string[] args)
    {
        StreamReader f = new StreamReader("input.txt");
        MyVector<string> lin = new MyVector<string>();
        MyVector<string> IPS = new MyVector<string>();

        string? line;
        while ((line = f.ReadLine()) != null)
        {
            lin.Add(line);
        }
        f.Close();
        for (int i = 0; i < lin.Size(); i++)
        {
            string currentLine = lin.Get(i);

            if (!string.IsNullOrWhiteSpace(currentLine))
            {
                string[] potentialIP = currentLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var CandidateIP in potentialIP)
                {
                    if (CorrectIp(CandidateIP))
                    {
                        IPS.Add(CandidateIP);
                    }
                }
            }
        }
        StreamWriter f1 = new StreamWriter("output.txt", false);
        for (int i = 0; i < IPS.Size(); i++)
        {
            string ip = IPS.Get(i);

            if (!string.IsNullOrWhiteSpace(ip))
            {
                f1.WriteLine(ip);
            }
        }
      
        f1.Close();

        IPS.Print();
    }
}