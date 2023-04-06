using TrivialCollections.Library.SingleLinked.PolishNotation;
using static TrivialCollections.Library.SingleLinked.PolishNotation.PostfixCalculator;

var calculator = new PostfixCalculator();
void TryGetResult()
{
    try
    {
        Console.WriteLine($"Expression: {calculator.Expression ?? "null"}");
        Console.WriteLine($"ExpressionIsValid: {calculator.ExpressionIsValid}");
        Console.WriteLine($"Actual: {calculator.Result}\n");
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.WriteLine();
    }
}

TryGetResult();

calculator = new PostfixCalculator("");
TryGetResult();

calculator = new PostfixCalculator("!");
TryGetResult();

calculator = new PostfixCalculator("1");
TryGetResult();

Console.ReadKey();

var tests = new string[]
{
    "( 1 + ( 3 - 4 ) + 5 ) * 6",
    "( ( ( 2 - 3 ) * 4 ) + 1 ) / 3",
    "1 + 2 ^ ( 1 / 2 )",
    "1 + sqrt ( 2 )",
    "sin 7 ^ 2 + cos 7 ^ 2",
    "( sin 3 / cos 3 ) * ctg 3",
    "sin ( Pi )", /*3 + 0,1415*/ 
    "2 * sin 3 * cos 3",
    "( 3 + 2 ) * 2",
    "2 * ( 3 + 2 )",
    "-2 + ( ( 3 + 3 ) ! * 0,5 + 2 )",
    "5 %",
    "10 * ( ( 50 - 25 ) / 5 ) %",
    "1 + 2 * ( 10 * ( ( 50 - 25 ) / 5 ) % * 4 + 50 % + 2 ) !",
    "( 10 + 20 ) %",
    "100 * 3 + ( 10 + 20 ) %",
    "100 * ( 300 + ( 10 + 20 /    0,5 ) % + 10 )",
    "100 * ( 300 -    ( 10 +      20 / 0,5 ) % + 10 )",
    "2 * 3 %",
    "2 * 3 !",
    "2 * 3 ! %",
    "2 * 3 % !",
    "tg 0",
    "ctg 0",
    "gr ( Pi / 2 )",
    "sin ( 3 * Pi / 2 )",
    "sin 270",
};

var answers = new double[]
{
    30,
    -1,
    1+Math.Sqrt(2),
    1+Math.Sqrt(2),
    1,
    1,
    0,
    Math.Sin(6),
    10,
    10,
    360,
    0.05,
    0.5,
    241,
    0.3,
    390,
    46000,
    16000,
    0.06,
    12,
    0.12,
    2,
    Math.Tan(0),
    1/Math.Tan(0),
    90,
    -1,
    -1
};

for (var i = 0; i < tests.Length; i++)
{
    Console.WriteLine(tests[i]);
    calculator.Expression = tests[i];
    if (i > 25) 
        calculator.AngleMeasure = AngleMeasures.deg;
    Console.WriteLine(calculator.PostfixNotation);
    Console.WriteLine($"Expected: {answers[i]}, Actual: {calculator.Result}");
    Console.WriteLine();
    i++;
}

Console.ReadLine();