using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Authorization
{
    public enum Role
    {
        [Display(Name = "Пользователь")]
        User = 0,
        [Display(Name = "Админ")]
        Admin = 1
    }
}
