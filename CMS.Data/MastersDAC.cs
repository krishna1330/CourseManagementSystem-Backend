using CMS.Business.ClientResponseModels;
using CMS.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class MastersDAC
    {
        public List<GetMastersResponse> GetMasters(CMSDbContext _cmsDbContext)
        {
            List<GetMastersResponse> mastersList = new List<GetMastersResponse>();
            try
            {
                mastersList = (from master in _cmsDbContext.Users
                               where master.UserType == "Master"
                                     && master.IsActive
                                     && !master.IsDeleted
                               select new GetMastersResponse
                               {
                                   UserId = master.UserId,
                                   MasterFirstName = master.FirstName,
                                   MasterLastName = master.LastName,
                                   MasterEmailId = master.EmailId,
                                   MasterMobile = master.Mobile
                               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mastersList;
        }
    }
}
