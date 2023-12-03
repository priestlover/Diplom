using Microsoft.AspNetCore.Identity;

namespace Diplom.Models.Authorization
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string lName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }

        public User() { }




    }
}
