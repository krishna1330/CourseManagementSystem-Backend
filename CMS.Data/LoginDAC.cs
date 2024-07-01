using CMS.Business.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class LoginDAC
    {
        public Users IsValidUser(CMSDbContext _cmsDbContext, LoginCredentials loginCredentials)
        {
            Users? user = new Users();
            try
            {
                if (loginCredentials != null)
                {
                    user = (from u in _cmsDbContext.Users
                            where u.EmailId == loginCredentials.EmailId
                                  && u.Password == loginCredentials.Password
                                  && u.IsActive
                                  && !u.IsDeleted
                            select u).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        //public Users? IsValidUser(string connectionString, LoginCredentials loginCredentials)
        //{
        //    Users? user = null;
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            con.Open();
        //            using (SqlCommand command = new SqlCommand("sp_ValidateUser", con))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add(new SqlParameter("@EmailId", loginCredentials.EmailId));
        //                command.Parameters.Add(new SqlParameter("@Password", loginCredentials.Password));

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        user = new Users
        //                        {
        //                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
        //                            UserType = reader.GetString(reader.GetOrdinal("UserType")),
        //                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
        //                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
        //                            EmailId = reader.GetString(reader.GetOrdinal("EmailId")),
        //                            Mobile = reader.GetString(reader.GetOrdinal("Mobile")),
        //                            Password = reader.GetString(reader.GetOrdinal("Password")),
        //                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
        //                            IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"))
        //                        };
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("An error occurred: " + ex.Message);
        //        }
        //        finally
        //        {
        //            con.Close();
        //        }
        //    }
        //    return user;
        //}

    }
}
