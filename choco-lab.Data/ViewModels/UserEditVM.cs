using choco_lab.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data.ViewModels
{
    public class UserEditVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }

        public UserEditVM() { }

        public UserEditVM(ApplicationUser appUser)
        {
            Email = appUser.Email;
            Password = appUser.PasswordHash;
            FullName = appUser.FullName;
            UserName = appUser.UserName;
            Address = appUser.Address;
        }
    }
}
