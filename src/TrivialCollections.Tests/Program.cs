using TrivialCollections.Library;

var test1 = new Test { Data = new IntWrapper { Number = 1 } };
var test2 = new Test { Data = null };
var test3 = new Test { Data = new IntWrapper { Number = 3 } };

Console.WriteLine("Stack test:");

var stack = new TrivialStack<Test>();
stack.Push(test1);
stack.Push(test2);
stack.Push(test3);

Console.WriteLine("Peek: " + stack.Peek());
Console.WriteLine("Peek: " + stack.Peek());

Console.WriteLine("*****");

Console.WriteLine("Pop: " + stack.Pop());
Console.WriteLine("Pop: " + stack.Pop());
Console.WriteLine("Pop: " + stack.Pop());

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

Console.WriteLine("\nQueue test:");

var queue = new TrivialQueue<Test>();
queue.Enqueue(test1);
queue.Enqueue(test2);
queue.Enqueue(test3);

Console.WriteLine("Peek: " + queue.Peek());
Console.WriteLine("Peek: " + queue.Peek());

Console.WriteLine("*****");

Console.WriteLine("Dequeue: " + queue.Dequeue());
Console.WriteLine("Dequeue: " + queue.Dequeue());
Console.WriteLine("Dequeue: " + queue.Dequeue());

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

Console.ReadLine();

file class Test
{
    public IntWrapper? Data { get; init; }

    public override string? ToString() => Data is null ? "no data" : Data.Number.ToString();
}

internal class IntWrapper
{
    public int Number { get; init; }
}