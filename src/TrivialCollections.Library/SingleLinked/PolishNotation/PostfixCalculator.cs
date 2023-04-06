using System.Numerics;
using System.Text;
using TrivialCollections.Library.Interfaces;

namespace TrivialCollections.Library.SingleLinked.PolishNotation;

/// <summary>
/// Расчётный модуль для калькулятора, основанный на обратной польской записи
/// </summary>
public class PostfixCalculator
{
    #region Consts
    public const double Pi = double.Pi, Tau = double.Tau, E = double.E;
    #endregion

    #region Constructors
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="T:TrivialCollections.Library.PolishNotation.PostfixCalculator" />
    /// c пустым выражением.
    /// </summary>
    public PostfixCalculator() { }
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="T:TrivialCollections.Library.PolishNotation.PostfixCalculator" />
    /// c заданным выражением.
    /// </summary>
    /// <param name="expression">Новое выражение.</param>
    public PostfixCalculator(string expression) => Expression = expression;
    #endregion

    #region Fields & Properties
    /// <summary>
    /// Унарные операции: sin x
    /// </summary>
    protected IList<string> UnaryOperators { get; init; } =     //string[] -?
        new[] { "sin", "cos", "tg", "ctg", "rad", "gr", "ln", "lg", "lb", "exp", "2^", "!", "sqrt", "%" };

    /// <summary>
    /// Бинарные операции: х + у
    /// </summary>
    protected IList<string> BinaryOperators { get; init; } =
        new[] { "+", "-", "*", "/", "^", "log", "-%", "+%" };

    /// <summary>
    /// Тернарные операции: T х у z 
    /// </summary>
    protected IList<string> TernaryOperators { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Четвертичные операции: Qut х у z t
    /// </summary>
    protected IList<string> QuaternaryOperators { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Пятерные операции: Q х у z t c
    /// </summary>
    protected IList<string> QuinaryOperators { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Постфиксные операции: x !
    /// </summary>
    protected IList<string> PostfixOperators { get; init; } = new[] { "!", "%" };

    /// <summary>
    /// Арифметическое выражение, к-ое следует рассчитать.
    /// </summary>
    public string? Expression { get; set; }

    /// <summary>
    /// Пытается создать обратную польскую запись:
    /// - в случае успеха, возвращает состояние валидности обратной польской записи;
    /// - в противном, возвращает false.
    /// </summary>
    public bool ExpressionIsValid
    {
        get
        {
            try
            {
                return RpnIsValid;
            }
            catch
            {
                return false;
            }
        }
    }

    #region Angle -> Radian

    protected virtual Func<double, double> AngleToRadians => AngleMeasure switch
    {
        AngleMeasures.rad => r => r,
        AngleMeasures.deg => g => g % 360D * 0.0174532925199433D,
        _ => a => double.NaN,
    };

    public AngleMeasures AngleMeasure { get; set; } = AngleMeasures.rad;
    #endregion

    /// <summary>
    /// Возвращает обратную польскую запись исходного выражения.
    /// </summary>
    public string? PostfixNotation => GetPostfixNotation(
        WhitespaceOrdering(Expression?
            .Replace("E", $"{E.ToCurrentCultureString()}")
            .Replace("Pi", $"{Pi.ToCurrentCultureString()}")
            .Replace("Tau", $"{Tau.ToCurrentCultureString()}")
            .ToLower()));

    /// <summary>
    /// Возвращает состояние валидности обратной польской записи. 
    /// </summary>
    protected bool RpnIsValid
    {
        get
        {
            var rpn = PostfixNotation?.Split(' ');
            if (rpn is null || !rpn.Any()) return false;
            if (rpn.Length == 1) return GetElementType(rpn[0]) == ElementType.Number;

            var counter = 0;
            var previousIsOperator = 0;
            foreach (var instruction in rpn)
            {
                var type = GetElementType(instruction);
                if (type is ElementType.UnknownOperator) return false;

                counter += (int)type + previousIsOperator;

                if (counter < 0) return false;

                previousIsOperator = type == ElementType.Number ? 0 : 1;
            }

            return counter == 0;
        }
    }

    /// <summary>
    /// Возвращает результат вычислений.
    /// </summary>
    public string Result => Calculate(PostfixNotation).ToCurrentCultureString();
    #endregion

    #region Protected & Private Methods
    protected virtual int GetOperationPriority(string operation) => operation switch
    {
        "(" or ")" => 10,
        "+" or "-" or "+%" or "-%" => 0,
        "*" or "/" or "^" or "log" => 1,
        "sin" or "cos" or "tg" or "ctg" or "ln" or "lg" or "lb" or "sqrt" => 2,
        "exp" or "rad" or "2^" or "gr" => 3,
        "!" or "%" => 4,
        _ => -1
    };

    protected virtual double UnaryFunc(string operation, double x) => operation switch
    {
        "sin" => Math.Sin(AngleToRadians(x)),
        "cos" => Math.Cos(AngleToRadians(x)),
        "tg" => Math.Tan(AngleToRadians(x)),
        "ctg" => 1 / Math.Tan(AngleToRadians(x)),
        "ln" or "lg" or "lb" when x < 0 =>
            throw new ArgumentException("Попытка вычислить логарифм у отрицательного числа.", nameof(x)),
        "ln" => Math.Log(x),
        "lg" => Math.Log10(x),
        "lb" => Math.Log2(x),
        "sqrt" when x < 0 => throw new ArgumentException("Попытка извлечь корень из отрицательного числа."),
        "sqrt" => Math.Sqrt(x),
        "exp" => Math.Exp(x),
        "2^" => Math.Pow(2D, x),
        "rad" => x % 360D * 0.0174532925199433D, // Pi / 180
        "gr" => x % Tau * 57.2957795130823D,     // 180 / Pi
        "%" => x / 100D,
        "!" => x < 2 ? 1 : checked(
            (double)Enumerable
                .Range(2, (int)x - 1)
                .Aggregate(BigInteger.One, (a, b) => a * b)),
        _ => double.NaN
    };

    protected virtual double BinaryFunc(string operation, double y, double x) => operation switch
    {
        "+" => x + y,
        "-" => x - y,
        "*" => x * y,
        "/" when y is 0 => throw new DivideByZeroException("Попытка деления на 0."),
        "/" => x / y,
        "log" when y < 0 => throw new ArgumentException("Попытка вычислить логарифм у отрицательного числа.", nameof(y)),
        "log" when x is < 0 or 1 => throw new ArgumentException("Основание логарифма должно быть больше нуля и не должно равняться единице.", nameof(x)),
        "log" => Math.Log(y, x),
        "^" when x == 0 && y < 0 => throw new ArgumentException("Попытка возвести ноль в отрицательную степень."),
        "^" when x < 0 && y - Math.Ceiling(y) != 0 => throw new ArgumentException("Попытка возвести отрицательное число в нецелую степень."),
        "^" => Math.Pow(x, y),
        "-%" => x - x / 100D * y,
        "+%" => x + x / 100D * y,
        _ => double.NaN
    };

    protected virtual double TernaryFunc(string operation, double z, double y, double x) => double.NaN;

    protected virtual double QuaternaryFunc(string operation, double t, double z, double y, double x) => double.NaN;

    protected virtual double QuinaryFunc(string operation, double s, double t, double z, double y, double x) => double.NaN;

    protected ElementType GetElementType(string element) =>
        double.TryParse(element, out _) ? ElementType.Number
        : BinaryOperators.Contains(element) ? ElementType.BinaryOperator
        : UnaryOperators.Contains(element) ? ElementType.UnaryOperator
        : TernaryOperators.Contains(element) ? ElementType.TernaryOperator
        : QuaternaryOperators.Contains(element) ? ElementType.QuaternaryOperator
        : QuinaryOperators.Contains(element) ? ElementType.QuinaryOperator
        : ElementType.UnknownOperator;

    private static string? WhitespaceOrdering(string? expression)
    {
        if (expression == null) return null;
        expression = expression.Trim();
        var sb = new StringBuilder(string.Empty, expression.Length);
        var count = false;
        foreach (var el in expression)
        {
            if (char.IsWhiteSpace(el)) count = true;
            else
            {
                if (count) sb.Append(' ', 1);
                sb.Append(el, 1);
                count = false;
            }
        }
        return sb.ToString();
    }
    #endregion

    #region PostfixNotation

    protected string GetPostfixNotation(string? expression) =>
        string.IsNullOrWhiteSpace(expression)
            ? string.Empty
            : GetPostfixNotation(expression.Split(' '));
    protected string GetPostfixNotation(IList<string>? instructions)
    {
        if (instructions == null || instructions.Count == 0) return string.Empty;

        IStack<string> operationsStack = new TrivialStack<string>();
        var result = "";

        string Join(string? element) => string.Join(' ', result, element);

        foreach (var instruction in instructions)
        {
            if (double.TryParse(instruction, out var number))
                result = Join(number.ToCurrentCultureString());
            else switch (instruction)
                {
                    case ")":
                        {
                            while (operationsStack.Peek() != "(")
                                result = Join(operationsStack.Pop());
                            operationsStack.Pop();
                            break;
                        }
                    case "%" when !operationsStack.IsEmpty && operationsStack.Peek() is "-" or "+":
                        operationsStack.Push(operationsStack.Pop() + "%");
                        break;
                    case "%":
                        result = Join("%");
                        break;
                    default:
                        {
                            if (PostfixOperators.Contains(instruction)) result = Join(instruction);
                            else
                            {
                                if (!operationsStack.IsEmpty)
                                {
                                    var operation = operationsStack.Peek();
                                    if (GetOperationPriority(instruction) <= GetOperationPriority(operation!) && operation != "(")
                                        result = Join(operationsStack.Pop());
                                }
                                operationsStack.Push(instruction);
                            }
                            break;
                        }
                }
        }
        while (!operationsStack.IsEmpty) result = Join(operationsStack.Pop());

        return result.TrimStart();
    }

    protected double Calculate(string? postfixNotation)
    {
        if (string.IsNullOrWhiteSpace(postfixNotation))
            throw new ArgumentException("Пустое выражение.", nameof(postfixNotation));

        var instructions = postfixNotation.Split(' ');
        if (instructions.Length == 1)
            return double.TryParse(instructions[0], out var result) ? result : double.NaN;

        var numbersStack = new TrivialStack<double>();
        foreach (var instruction in instructions)
        {
            if (!double.TryParse(instruction, out var number))
            {
                if (BinaryOperators.Contains(instruction))
                    numbersStack.Push(BinaryFunc(instruction, numbersStack.Pop(), numbersStack.Pop()));
                else if (UnaryOperators.Contains(instruction))
                    numbersStack.Push(UnaryFunc(instruction, numbersStack.Pop()));
                else if (TernaryOperators.Contains(instruction))
                    numbersStack.Push(TernaryFunc(instruction,
                            numbersStack.Pop(), numbersStack.Pop(), numbersStack.Pop()));
                else if (QuaternaryOperators.Contains(instruction))
                    numbersStack.Push(QuaternaryFunc(instruction,
                            numbersStack.Pop(), numbersStack.Pop(), numbersStack.Pop(), numbersStack.Pop()));
                else if (QuinaryOperators.Contains(instruction))
                    numbersStack.Push(QuinaryFunc(instruction,
                            numbersStack.Pop(), numbersStack.Pop(), numbersStack.Pop(), numbersStack.Pop(), numbersStack.Pop()));
                else
                    throw new ArgumentException($"В выражении неизвестный оператор. ({instruction})", nameof(postfixNotation));
            }
            else numbersStack.Push(number);
        }
        return numbersStack.Pop();
    }
    #endregion
}