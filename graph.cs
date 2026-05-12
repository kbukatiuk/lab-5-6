using System;
using System.Collections.Generic;
using System.Diagnostics;
class Graph
{
    private Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
    // Додавання вершини
    public void AddVertex(int vertex)
    {
        if (!graph.ContainsKey(vertex))
            graph[vertex] = new List<int>();
    }

    // Додавання ребра
    public void AddEdge(int first, int second)
    {
        AddVertex(first);
        AddVertex(second);
        if (!graph[first].Contains(second))
            graph[first].Add(second);
        if (!graph[second].Contains(first))
            graph[second].Add(first);
    }
    // Виведення списку суміжності
    public void PrintGraph()
    {
        foreach (var item in graph)
        {
            Console.Write(item.Key + ": ");
            foreach (int vertex in item.Value)
                Console.Write(vertex + " ");

            Console.WriteLine();
        }
    }

    // Підрахунок кількості ребер графа
    public int CountEdges()
    {
        int count = 0;
        foreach (var item in graph)
        {
            count += item.Value.Count;
        }
        return count / 2;
    }
    // МІЙ ВАРІАНТ:
    // Обчислити степінь кожної вершини графа
    public void PrintDegrees()
    {
        foreach (var item in graph)
        {
            int vertex = item.Key;
            int degree = item.Value.Count;
            Console.WriteLine("Степінь вершини " + vertex + " = " + degree);
        }
    }
    // Обхід графа в ширину
    public void BFS(int start)
    {
        if (!graph.ContainsKey(start))
        {
            Console.WriteLine("Такої вершини немає");
            return;
        }
        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            Console.Write(current + " ");
            foreach (int next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }

        Console.WriteLine();
    }

    // Обхід графа в глибину
    public void DFS(int start)
    {
        if (!graph.ContainsKey(start))
        {
            Console.WriteLine("Такої вершини немає");
            return;
        }
        HashSet<int> visited = new HashSet<int>();
        DFS(start, visited);
        Console.WriteLine();
    }

    private void DFS(int current, HashSet<int> visited)
    {
        visited.Add(current);
        Console.Write(current + " ");
        foreach (int next in graph[current])
        {
            if (!visited.Contains(next))
                DFS(next, visited);
        }
    }

    // Перевірка, чи існує шлях між двома вершинами
    public bool HasPath(int start, int finish)
    {
        if (!graph.ContainsKey(start) || !graph.ContainsKey(finish))
            return false;

        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue(start);
        visited.Add(start);
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            if (current == finish)
                return true;

            foreach (int next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }

        return false;
    }

    // BFS без виведення для експерименту
    public void BFSWithoutPrint(int start)
    {
        if (!graph.ContainsKey(start))
            return;
        Queue<int> queue = new Queue<int>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            foreach (int next in graph[current])
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }
    }

    // DFS без виведення для експерименту
    public void DFSWithoutPrint(int start)
    {
        if (!graph.ContainsKey(start))
            return;
        HashSet<int> visited = new HashSet<int>();
        DFSWithoutPrint(start, visited);
    }

    private void DFSWithoutPrint(int current, HashSet<int> visited)
    {
        visited.Add(current);
        foreach (int next in graph[current])
        {
            if (!visited.Contains(next))
                DFSWithoutPrint(next, visited);
        }
    }
}

class Program
{
    static Random random = new Random();
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Graph graph = new Graph();

        // Створення графа
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(2, 5);
        graph.AddEdge(3, 6);
        graph.AddEdge(6, 7);

        // Ізольована вершина
        graph.AddVertex(8);
        Console.WriteLine("Завдання 2.2. Графи");
        Console.WriteLine();
        
        Console.WriteLine("Мій варіант: обчислити степінь кожної вершини графа");
        Console.WriteLine();
        
        Console.WriteLine("Список суміжності:");
        graph.PrintGraph();
        
        Console.WriteLine();
        Console.WriteLine("Кількість ребер у графі: " + graph.CountEdges());
        Console.WriteLine();
        
        Console.WriteLine("Виконання мого варіанту:");
        graph.PrintDegrees();
        Console.WriteLine();
        
        Console.WriteLine("Обхід графа в ширину BFS:");
        graph.BFS(1);
        
        Console.WriteLine("Обхід графа в глибину DFS:");
        graph.DFS(1);
        
        Console.WriteLine();
        Console.WriteLine("Перевірка шляху між вершинами 1 і 7:");

        if (graph.HasPath(1, 7))
            Console.WriteLine("Шлях існує");
        else
            Console.WriteLine("Шляху немає");
        Console.WriteLine();
        Console.WriteLine("Перевірка шляху між вершинами 1 і 8:");

        if (graph.HasPath(1, 8))
            Console.WriteLine("Шлях існує");
        else
            Console.WriteLine("Шляху немає");

        Console.WriteLine();
        Console.WriteLine("Експериментальна частина:");

        Experiment(10);
        Experiment(50);
        Experiment(100);
    }

    static void Experiment(int size)
    {
        double bfsTime = 0;
        double dfsTime = 0;

        for (int i = 0; i < 3; i++)
        {
            Graph graph = GenerateGraph(size);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            graph.BFSWithoutPrint(1);
            timer.Stop();

            bfsTime += timer.Elapsed.TotalMilliseconds;

            timer.Restart();
            graph.DFSWithoutPrint(1);
            timer.Stop();
            dfsTime += timer.Elapsed.TotalMilliseconds;
        }

        Console.WriteLine();
        Console.WriteLine("Кількість вершин: " + size);
        Console.WriteLine("Середній час BFS: " + bfsTime / 3 + " мс");
        Console.WriteLine("Середній час DFS: " + dfsTime / 3 + " мс");
    }

    static Graph GenerateGraph(int size)
    {
        Graph graph = new Graph();

        for (int i = 1; i <= size; i++)
            graph.AddVertex(i);

        for (int i = 1; i < size; i++)
            graph.AddEdge(i, i + 1);

        for (int i = 0; i < size; i++)
        {
            int first = random.Next(1, size + 1);
            int second = random.Next(1, size + 1);
            if (first != second)
                graph.AddEdge(first, second);
        }
        return graph;
    }
}
