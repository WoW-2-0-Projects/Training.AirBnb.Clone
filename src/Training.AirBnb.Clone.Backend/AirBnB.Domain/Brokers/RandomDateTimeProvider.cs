namespace AirBnB.Domain.Brokers;

/// <summary>
/// Represents a random date time provider.
/// </summary>
public class RandomDateTimeProvider
{
    /// <summary>
    /// Generates a random date time.
    /// </summary>
    /// <param name="start">Optional start time</param>
    /// <param name="end">Optional end time</param>
    /// <returns>Generated instance of <see cref="DateTime"/></returns>
    /// <exception cref="ArgumentException">If start date is later than end date</exception>
    public DateTime Generate(DateTime? start, DateTime? end)
    {
        start ??= DateTime.UnixEpoch;
        end ??= DateTime.Now;

        if (start > end)
            throw new ArgumentException("Start date cannot be greater than end date.");

        var random = new Random();
        var range = end - start;
        var randTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Value.Ticks));
        return start.Value + randTimeSpan;
    }
}