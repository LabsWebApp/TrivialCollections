using TrivialCollections.Library;
using TrivialCollections.Library.Interfaces;

// Замер памяти на утечку
//var firstMemoryUsage = GC.GetTotalMemory(true);
//Console.WriteLine("Использовано памяти: " + firstMemoryUsage);

var test1 = new TestClass { Data = new IntWrapper { Number = 1 } };
var test2 = new TestClass { Data = null };
var test3 = new TestClass { Data = new IntWrapper { Number = 3 } };

Console.WriteLine("STACK test:");

IStack<TestClass> stack = new TrivialStack<TestClass>();
// цикл для проверки утечки памяти
//for (var i = 0; i < 1000; i++) 
{
    stack.Push(test1);
    Console.WriteLine("Добавили в стек: " + test1);
    stack.Push(test2);
    Console.WriteLine("Добавили в стек: " + test2);
    stack.Push(test3);
    Console.WriteLine("Добавили в стек: " + test3);

    Console.WriteLine("*****");

    Console.WriteLine("Peek: " + stack.Peek());
    Console.WriteLine("Peek: " + stack.Peek());

    Console.WriteLine("*****");

    while (!stack.IsEmpty) Console.WriteLine("Pop: " + stack.Pop());
}

Console.WriteLine("*****");

try
{
    Console.WriteLine("Pop: " + stack.Pop());
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e);
    Console.WriteLine("Так было задумано)))");
}

Console.WriteLine("\nQUEUE test:");

IQueue<TestClass> queue = new TrivialQueue<TestClass>();
// цикл для проверки утечки памяти
//for (var i = 0; i < 1000; i++) 
{
    queue.Enqueue(test1);
    queue.Enqueue(test2);
    queue.Enqueue(test3);

    Console.WriteLine("Peek: " + queue.Peek());
    Console.WriteLine("Peek: " + queue.Peek());

    Console.WriteLine("*****");

    while (!queue.IsEmpty) Console.WriteLine("Dequeue: " + queue.Dequeue());
}

Console.WriteLine("*****");

try
{
    Console.WriteLine("Dequeue: " + queue.Dequeue());
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e);
    Console.WriteLine("Так было задумано)))");
}

Console.WriteLine("*****");
Console.WriteLine("Использовано памяти: " + (GC.GetTotalMemory(true) - firstMemoryUsage));

Console.ReadLine();

file class TestClass
{
    public IntWrapper? Data { get; init; }

    public override string? ToString() => Data is null ? "no data" : Data.Number.ToString();
}

internal class IntWrapper
{
    public int Number { get; init; }
}