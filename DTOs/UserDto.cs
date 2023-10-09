public record UserDto
{
    public UserDto() { } // Construtor sem parâmetros

    public UserDto( string email, string? name,  DateTime? dateOfBirth)
    {
        Email = email;
        Name = name;
        DateOfBirth = dateOfBirth;
    }

    public string Email { get; init; }
    public string? Name { get; init; }
    public DateTime? DateOfBirth { get; init; }
}
