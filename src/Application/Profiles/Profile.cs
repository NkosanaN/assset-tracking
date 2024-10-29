using Domain;

namespace Application.Profiles;

public class Profile
{
	public required string Username { get; set; }
	public required string DisplayName { get; set; }
	public required string Bio { get; set; }
	public required string Image { get; set; }
	public required ICollection<UserPhoto> Photos { get; set; }
}
