using Microsoft.AspNetCore.Identity;

namespace Chat.Data.Entity
{
    public class ManageUser : IdentityUser
    {
        public string DisPlayName { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
