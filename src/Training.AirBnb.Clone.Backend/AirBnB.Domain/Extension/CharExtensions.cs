namespace AirBnB.Domain.Extension;

/// <summary>
/// This static class provides extension methods for generating random characters.
/// </summary>
public static class CharExtensions
{
    
    /// <summary>
    /// Generates a random digit character ('0' to '9').
    /// </summary>
    /// <param name="random">Optional random number generator. Uses a new instance if null.</param>
    /// <returns>A random digit character.</returns>
    public static char GetRandomDigit(Random? random = null)
    {
        return (char)(random ?? new Random()).Next('0', '9' + 1);
    }

    /// <summary>
    /// Generates a random uppercase letter ('A' to 'Z').
    /// </summary>
    /// <param name="random">Optional random number generator. Uses a new instance if null.</param>
    /// <returns>A random uppercase letter.</returns>
    public static char GetRandomUppercase(Random? random = null)
    {
        return (char)(random ?? new Random()).Next('A', 'Z' + 1);
    }

    /// <summary>
    /// Generates a random lowercase letter ('a' to 'z').
    /// </summary>
    /// <param name="random">Optional random number generator. Uses a new instance if null.</param>
    /// <returns>A random lowercase letter.</returns>
    public static char GetRandomLowercase(Random? random = null)
    {
        return (char)(random ?? new Random()).Next('a', 'z' + 1);
    }

    /// <summary>
    /// Generates a random non-alphanumeric character from predefined set (!@#$%^&*()_-+=<>?).
    /// </summary>
    /// <param name="random">Optional random number generator. Uses a new instance if null.</param>
    /// <returns>A random non-alphanumeric character.</returns>
    public static char GetRandomNonAlphanumeric(Random? random = null)
    {
        return "!@#$%^&*()_-+=<>?"[(random ?? new Random()).Next(16)];
    }

    /// <summary>
    /// Generates a random character within the range of printable ASCII characters (32 to 126).
    /// </summary>
    /// <param name="random">Optional random number generator. Uses a new instance if null.</param>
    /// <returns>A random character.</returns>
    public static char GetRandomCharacter(Random? random)
    {
        return (char)(random ?? new Random()).Next(32, 126);
    }
}