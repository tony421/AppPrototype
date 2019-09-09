using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModels
{
    public class AccountVM : IViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsNull()
        {
            if (
                Object.ReferenceEquals(Email, null)
                && Object.ReferenceEquals(Password, null)
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
