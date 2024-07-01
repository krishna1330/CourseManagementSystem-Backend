using CMS.Business.ClientResponseModels;
using CMS.Business.Models;
using CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Implementation
{
    public class MastersComponent
    {
        public List<GetMastersResponse> GetMasters(CMSDbContext _cmsDbContext)
        {
            MastersDAC mastersDAC = new MastersDAC();
            return mastersDAC.GetMasters(_cmsDbContext);
        }
    }
}
