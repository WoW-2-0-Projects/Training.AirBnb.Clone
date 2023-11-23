namespace AirBnB.Api.Models.DTOs;

public class UserDto
{
    public Guid Id { get; set; } = default!;
    
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
}