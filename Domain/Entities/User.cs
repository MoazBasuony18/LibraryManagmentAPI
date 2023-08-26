using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentAPI.Domain.Entities
{
    public class User:IdentityUser
    {
        public List<Checkout> Checkouts { get; set; }

    }
}
