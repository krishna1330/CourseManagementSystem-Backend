using CMS.Business.ClientModels;
using CMS.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class UsersDAC
    {
        public async Task<string> AddUserAsync(CMSDbContext _cmsDbContext, AddUserPayload userPayload)
        {
            try
            {
                Users? existedUser = (from user in _cmsDbContext.Users
                                    where user.EmailId == userPayload.EmailId
                                    select user).FirstOrDefault();

                if (existedUser != null)
                {
                    return "Email already registered";
                }

                Users newUser = new Users
                {
                    UserType = userPayload.UserType,
                    FirstName = userPayload.FirstName,
                    LastName = userPayload.LastName,
                    EmailId = userPayload.EmailId,
                    Mobile = userPayload.Mobile,
                    Password = userPayload.Password,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                };

                _cmsDbContext.Users.Add(newUser);
                await _cmsDbContext.SaveChangesAsync().ConfigureAwait(false);

                return userPayload.UserType == "Master" ? "Master added sccessfully" : "Account created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
