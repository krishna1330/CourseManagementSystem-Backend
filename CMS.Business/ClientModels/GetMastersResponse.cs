using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Business.ClientResponseModels
{
    public class GetMastersResponse
    {
        public int? UserId { get; set; }
        public string? MasterFirstName { get; set; }
        public string? MasterLastName { get; set; }
        public string? MasterEmailId { get; set; }
        public string? MasterMobile { get; set; }
    }
}
