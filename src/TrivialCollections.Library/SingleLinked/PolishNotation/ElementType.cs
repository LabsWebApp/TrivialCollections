namespace TrivialCollections.Library.SingleLinked.PolishNotation;

public enum ElementType
{
    Number = 1,
    UnaryOperator = -1,
    BinaryOperator = -2,
    TernaryOperator = -3,
    QuaternaryOperator = -4,
    QuinaryOperator = -5,
    UnknownOperator = 10,
}