#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;

class Node
{
    public int Value;
    public Node Left;
    public Node Right;

    public Node(int value)
    {
        Value = value;
    }
}

class BST
{
    private Node root;

    // Метод додавання елемента
    public void Insert(int value)
    {
        root = Insert(root, value);
    }

    private Node Insert(Node node, int value)
    {
        // Якщо місце порожнє, створюємо новий вузол
        if (node == null)
            return new Node(value);

        // Якщо значення менше, рухаємось вліво
        if (value < node.Value)
            node.Left = Insert(node.Left, value);

        // Якщо значення більше, рухаємось вправо
        else if (value > node.Value)
            node.Right = Insert(node.Right, value);
         return node;
    }

    // Метод пошуку елемента
    public bool Search(int value)
    {
        return Search(root, value);
    }

    private bool Search(Node node, int value)
    {
        // Якщо вузол порожній, значення не знайдено
        if (node == null)
            return false;
        // Якщо значення знайдено
        if (node.Value == value)
            return true;
        // Якщо значення менше, шукаємо в лівому піддереві
        if (value < node.Value)
            return Search(node.Left, value);
        // Інакше шукаємо в правому піддереві
        return Search(node.Right, value);
    }
    // Підрахунок кількості вузлів
    public int Count()
    {
        return Count(root);
    }

    private int Count(Node node)
    {
        if (node == null)
            return 0;
        return 1 + Count(node.Left) + Count(node.Right);
    }

    // Обчислення висоти дерева
    public int Height()
    {
        return Height(root);
    }

    private int Height(Node node)
    {
        if (node == null)
            return 0;

        int left = Height(node.Left);
        int right = Height(node.Right);
          return Math.Max(left, right) + 1;
    }

    // Прямий обхід: корінь, ліво, право
    public void PreOrder()
    {
        PreOrder(root);
        Console.WriteLine();
    }

    private void PreOrder(Node node)
    {
        if (node == null)
            return;
        Console.Write(node.Value + " ");
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    // Симетричний обхід: ліво, корінь, право
    public void InOrder()
    {
        InOrder(root);
        Console.WriteLine();
    }

    private void InOrder(Node node)
    {
        if (node == null)
            return;

        InOrder(node.Left);
        Console.Write(node.Value + " ");
        InOrder(node.Right);
    }

    // Зворотний обхід: ліво, право, корінь
    public void PostOrder()
    {
        PostOrder(root);
        Console.WriteLine();
    }

    private void PostOrder(Node node)
    {
        if (node == null)
            return;

        PostOrder(node.Left);
        PostOrder(node.Right);
        Console.Write(node.Value + " ");
    }

    // Обхід дерева в ширину
    public void BFS()
    {
        if (root == null)
            return;

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            Console.Write(current.Value + " ");

            if (current.Left != null)
                queue.Enqueue(current.Left);
            if (current.Right != null)
                queue.Enqueue(current.Right);
        }

        Console.WriteLine();
    }

    // Варіант 6: вивести всі вузли на заданому рівні k
    public void PrintLevel(int k)
    {
        PrintLevel(root, 0, k);
        Console.WriteLine();
    }

    private void PrintLevel(Node node, int level, int k)
    {
        if (node == null)
            return;

        // Якщо поточний рівень дорівнює потрібному
        if (level == k)
        {
            Console.Write(node.Value + " ");
            return;
        }

        PrintLevel(node.Left, level + 1, k);
        PrintLevel(node.Right, level + 1, k);
    }
}

class Program
{
    static Random random = new Random();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
       BST tree = new BST();
       // Додаємо елементи в дерево
        tree.Insert(50);
        tree.Insert(30);
        tree.Insert(70);
        tree.Insert(20);
        tree.Insert(40);
        tree.Insert(60);
        tree.Insert(80);

        Console.WriteLine("Бінарне дерево пошуку");
        Console.WriteLine();

        Console.WriteLine("Прямий обхід:");
        tree.PreOrder();

        Console.WriteLine("Симетричний обхід:");
        tree.InOrder();

        Console.WriteLine("Зворотний обхід:");
        tree.PostOrder();

        Console.WriteLine("Обхід у ширину:");
        tree.BFS();

        Console.WriteLine("Кількість вузлів: " + tree.Count());
        Console.WriteLine("Висота дерева: " + tree.Height());
        Console.WriteLine("Пошук числа 40: " + tree.Search(40));
        Console.WriteLine("Пошук числа 100: " + tree.Search(100));
        Console.WriteLine();

        Console.Write("Введіть рівень k: ");
        int k = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Вузли на рівні " + k + ":");
        tree.PrintLevel(k);

        Console.WriteLine();
        Console.WriteLine("Експериментальна частина");
        Experiment(100);
        Experiment(1000);
        Experiment(5000);
    }

    // Експеримент для порівняння BST і SortedSet
    static void Experiment(int size)
    {
        double bstInsertTime = 0;
        double bstSearchTime = 0;

        double setInsertTime = 0;
        double setSearchTime = 0;

        for (int i = 0; i < 3; i++)
        {
            int[] data = GenerateData(size);

            BST tree = new BST();
            SortedSet<int> set = new SortedSet<int>();

            Stopwatch timer = new Stopwatch();

            // Час вставлення у власне дерево
            timer.Start();

            foreach (int number in data)
                tree.Insert(number);

            timer.Stop();
            bstInsertTime += timer.Elapsed.TotalMilliseconds;

            // Час пошуку у власному дереві
            timer.Restart();

            foreach (int number in data)
                tree.Search(number);

            timer.Stop();
            bstSearchTime += timer.Elapsed.TotalMilliseconds;

            // Час вставлення у SortedSet
            timer.Restart();

            foreach (int number in data)
                set.Add(number);

            timer.Stop();
            setInsertTime += timer.Elapsed.TotalMilliseconds;

            // Час пошуку у SortedSet
            timer.Restart();

            foreach (int number in data)
                set.Contains(number);

            timer.Stop();
            setSearchTime += timer.Elapsed.TotalMilliseconds;
        }

        Console.WriteLine();
        Console.WriteLine("Розмір даних: " + size);
        Console.WriteLine("BST вставлення: " + bstInsertTime / 3 + " мс");
        Console.WriteLine("BST пошук: " + bstSearchTime / 3 + " мс");
        Console.WriteLine("SortedSet вставлення: " + setInsertTime / 3 + " мс");
        Console.WriteLine("SortedSet пошук: " + setSearchTime / 3 + " мс");
    }

    // Генерація випадкових чисел без повторів
    static int[] GenerateData(int size)
    {
        HashSet<int> numbers = new HashSet<int>();

        while (numbers.Count < size)
        {
            int value = random.Next(1, size * 10);
            numbers.Add(value);
        }

        int[] result = new int[size];
        numbers.CopyTo(result);

        return result;
    }
}
