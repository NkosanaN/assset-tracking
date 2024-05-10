namespace Application.User;

public record AppUserDto(
    string DisplayName,
    string? AddressLine1,
    string? AddressLine2,
    string? UserName,
    string? NormalizedUserName,
    string? Email
    );
