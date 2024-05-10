using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;
/*
 * This model is design to hold User data 
 * A)  FK => UserPhoto
 */
public class AppUser : IdentityUser         
{
    public string DisplayName { get; set; } = string.Empty;
    public string? AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    //  public Guid ActivationCode { get; set; }
    public ICollection<UserPhoto> UserPhotos { get; set; } = new List<UserPhoto>();

    //[InverseProperty("Issuer")]
    //public ItemEmployeeAssignment IssuerDetails { get; set; }

    //[InverseProperty("Receiver")] 
    //public ItemEmployeeAssignment ReceiverDetails { get; set; }
}
