using CMS.Business.ClientModels;
using CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Implementation
{
    public class UserComponent
    {
        public async Task<string> AddUserAsync(CMSDbContext _cmsDbContext, AddUserPayload userPayload)
        {
            UsersDAC usersDAC = new UsersDAC();
            return await usersDAC.AddUserAsync(_cmsDbContext, userPayload);
        }
    }
}
