namespace API.DTOs;
//public record UserDto(
//    string DisplayName, 
//    string Token, 
//    string Img, 
//    string Username, 
//    string Firstname, 
//    string Lastname);


public class UserDto
{
    public string DisplayName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Img { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
}