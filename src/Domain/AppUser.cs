using Microsoft.AspNetCore.Identity;

namespace Domain
{
    /*
     * This model is design to hold User data 
     * A)  FK => TrackedItems
     */
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public ICollection<ItemTranfer> ItemTranferHistory { get; set; }
    }
}
