using System.Globalization;

namespace TrivialCollections.Library.SingleLinked.PolishNotation;

/// <summary>
/// Добавляет расширения к значениям типа <see cref="T:System.Double" />.
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    /// Преобразует числовое значение этого экземпляра в его эквивалентное строковое представление,
    /// используя текущий формат и сведения о этом формате, зависящие от языка и региональных параметров.
    /// </summary>
    /// <param name="value">Число типа <see cref="T:System.Double" />, к-ое следует преобразовать.</param>
    /// <returns>Строковое представление значения входящего числа.</returns>
    internal static string ToCurrentCultureString(this double value) => value
        .ToString("F15", CultureInfo.CurrentCulture)
        .TrimEnd('0')
        .TrimEnd(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator.ToCharArray());
}