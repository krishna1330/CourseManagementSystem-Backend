using CMS.Business.Models;
using CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Implementation
{
    public class LoginComponent
    {
        public Users IsValidUser(CMSDbContext cmsDbContext, LoginCredentials loginCredentials)
        {
            LoginDAC loginDAC = new LoginDAC();
            return loginDAC.IsValidUser(cmsDbContext, loginCredentials);
        }
    }
}
