using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public record RegisterDto(
    [Required][EmailAddress] string Email, 
    [Required] string Password, 
               string DisplayName, 
    [Required] string Username, 
    [Required] string Firstname,
    [Required] string Lastname, 
               string AddressLine1
    );

