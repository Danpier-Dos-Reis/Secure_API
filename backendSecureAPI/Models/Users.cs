using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendSecureAPI.Models
{
    public class Users
    {
        public int UniqueID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
    }
}