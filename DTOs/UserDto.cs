public record UserDto
{
    public UserDto() { } // Construtor sem parâmetros

    public UserDto(string username, string email, string? firstName, string? lastName, DateTime? dateOfBirth)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public string Username { get; init; }
    public string Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateTime? DateOfBirth { get; init; }
}
