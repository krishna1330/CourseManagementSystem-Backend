using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Business.ClientResponseModels
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public int UserId { get; set; }
        public string? UserType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? Mobile { get; set; }
    }
}
