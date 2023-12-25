namespace AirBnB.Application.Common.Notifications.Models;

public class TemplatePlaceholder
{
    public string Placeholder { get; set; } = default!;

    public string PlaceholderValue { get; set; } = default!;

    public string? Value { get; set; } = default!;

    public bool IsValid { get; set; }
}
