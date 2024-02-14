using Diplom.Models.Entity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Authorization
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public Role Role { get; set; }

        public Basket Basket { get; set; }
    }
}
