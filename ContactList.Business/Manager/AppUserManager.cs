using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.HadrData;

namespace ContactList.Business
{
    /// <summary>
    /// Handles base user functions (create, login, update)
    /// </summary>
    public static class AppUserManager
    {
        /// <summary>
        /// Creates user record in AppUser database and instantiates their custom contact table
        /// </summary>
        /// <param name="user">populated AppUser dto object</param>
        /// <returns>DtoReturnObject with AppUserReturn to populate user session information</returns>
        public static DtoReturnObject<OutputUserBase> CreateUser(InputUserRegister user)
        {
            try
            {
                user.UserName = user.UserName.ToLower();

                string checkUser = "select count(*) from AppUser where UserName = @userName";
                List<SqlParameter> checkParms = new List<SqlParameter>{
                    new SqlParameter("@userName", user.UserName)
                };

                if (DataConnection.ExecuteScalarInt(checkUser, checkParms) > 0)
                {
                    return new DtoReturnObject<OutputUserBase>(true, "This User Name already exists in the system. Please try again.", null);
                }


                Guid userId = Guid.NewGuid();

                string insertUser = "insert into AppUser([UserId],[UserName],[Password],[EmailAddress],[FirstName],[LastName],[CreateDate],[LastLogin]) " +
                    "values(@userId,@userName,@password,@emailAddress,@firstName,@lastName,@createDate,@lastLogin)";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@userName", user.UserName),
                    new SqlParameter("@password", StringUtil.HashPassword(user.Password)),
                    new SqlParameter("@emailAddress", user.EmailAddress),
                    new SqlParameter("@firstName", user.FirstName),
                    new SqlParameter("@lastName", user.LastName),
                    new SqlParameter("@createDate", DateTime.UtcNow),
                    new SqlParameter("@lastLogin", DateTime.UtcNow)
                };

                if (DataConnection.ExecuteNonQuery(insertUser, parameters))
                {
                    if(!ContactManager.CreateContactTable(userId))
                    {
                        return new DtoReturnObject<OutputUserBase>(true, "An erroc occured. You are able to log in but not save contacts.", null);
                    }
                }
                else
                {
                    return new DtoReturnObject<OutputUserBase>(true, "Could not create user record.", null);
                }

                return new DtoReturnObject<OutputUserBase>(false, string.Empty,
                    new OutputUserBase
                    {
                        UserId = userId,
                        UserName = user.UserName,
                        EmailAddress = user.EmailAddress,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoReturnObject<OutputUserBase>(true, "An unknown error occured, please contact support.", null);
            }
        }

        /// <summary>
        /// Verifies user/password and, if correct, updates last login date
        /// </summary>
        /// <param name="user">AppUserLogin object for user/pass string</param>
        /// <returns>DtoReturnObject with AppUserReturn to populate common session data</returns>
        public static DtoReturnObject<OutputUserBase> LoginUser(InputUserLogin user)
        {
            try
            {
                user.UserName = user.UserName.ToLower();
                string checkUser = "select * from AppUser where UserName = @userName and Password = @password";

                List<SqlParameter> checkParams = new List<SqlParameter>
                {
                    new SqlParameter("@userName", user.UserName),
                    new SqlParameter("@password", StringUtil.HashPassword(user.Password))
                };

                DataTable dt = DataConnection.ExecuteQuery(checkUser, checkParams);

                if (dt != null && !dt.HasErrors && dt.Rows.Count > 0)
                {
                    OutputUserBase appUserReturn = new OutputUserBase
                    {
                        UserId = (Guid)dt.Rows[0]["UserId"],
                        UserName = (string)dt.Rows[0]["UserName"],
                        FirstName = (string)dt.Rows[0]["FirstName"],
                        LastName = (string)dt.Rows[0]["LastName"],
                        EmailAddress = (string)dt.Rows[0]["EmailAddress"]
                    };

                    string loginUser = "update AppUser set LastLogin = @lastLogin where UserId = @userId";

                    List<SqlParameter> loginParams = new List<SqlParameter>
                    {
                        new SqlParameter("@lastLogin", DateTime.UtcNow),
                        new SqlParameter("@userId", appUserReturn.UserId)
                    };


                    if (!DataConnection.ExecuteNonQuery(loginUser, loginParams))
                    {
                        return new DtoReturnObject<OutputUserBase>(true, "A minor error occurred updating your login date.", null);
                    }

                    return new DtoReturnObject<OutputUserBase>(false, string.Empty, appUserReturn);
                }
                else
                {
                    return new DtoReturnObject<OutputUserBase>(true, "Your username/password combination is incorrect.", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return new DtoReturnObject<OutputUserBase>(true, "An unknown error occured, please contact support.", null);
            }
        }

        /// <summary>
        /// Updates a user's email, first name, last name, and password (if provided)
        /// </summary>
        /// <param name="user">AppUser object</param>
        /// <returns>DtoBase of success/fail with message</returns>
        public static DtoReturnBase UpdateUser(InputUserUpdate user)
        {
            try
            {
                string checkUser = "select count(*) from AppUser where UserId = @userId";
                List<SqlParameter> checkParms = new List<SqlParameter>{
                    new SqlParameter("@userId", user.UserId)
                };

                if (DataConnection.ExecuteScalarInt(checkUser, checkParms) > 0)
                {
                    if(!string.IsNullOrEmpty(user.NewPassword))
                    {
                        string checkPassword = "select * from AppUser where UserId = @userId and Password = @password";

                        List<SqlParameter> passParams = new List<SqlParameter>
                        {
                            new SqlParameter("@userName", user.UserId),
                            new SqlParameter("@password", StringUtil.HashPassword(user.OldPassword))
                        };

                        if(DataConnection.ExecuteScalarInt(checkPassword, passParams) == 0)
                        {
                            return new DtoReturnBase(true, "Old password must match current password.");
                        }
                    }

                    List<SqlParameter> updateParams = new List<SqlParameter>
                    {
                        new SqlParameter("@emailAddress", user.EmailAddress),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@userId", user.UserId)
                    };

                    string updateUser = "update AppUser set" +
                        " [EmailAddress] = @emailAddress" +
                        " ,[FirstName] = @firstName" +
                        " ,[LastName] = @lastName";

                    if (!string.IsNullOrEmpty(user.NewPassword))
                    {
                        updateUser += " ,[Password] = @password";
                        updateParams.Add(new SqlParameter("@password", StringUtil.HashPassword(user.NewPassword)));
                    }

                    updateUser += " where UserId = @userId";


                    if (DataConnection.ExecuteNonQuery(updateUser, updateParams))
                    {
                        return new DtoReturnBase(false, string.Empty);
                    }
                    else
                    {
                        return new DtoReturnBase(true, "Error updating information.");
                    }
                }
                else
                {
                    return new DtoReturnBase(true, "Your user account cannot be found. Please log in again.");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return new DtoReturnBase(true, "An unknown error occured, please contact support.");
            }
        }


        /// <summary>
        /// Returns list of top N users, used for stress testing only
        /// </summary>
        /// <param name="userCount">int number of users to be returned</param>
        /// <returns>List of N AppUsers</returns>
        public static List<OutputUserBase> GetUsers(int userCount)
        {
            List<OutputUserBase> users = new List<OutputUserBase>();

            try
            {
                string listUser = "select top {0} * from AppUser";
                listUser = string.Format(listUser, userCount.ToString());

                DataTable dt = DataConnection.ExecuteQuery(listUser, new List<SqlParameter>());

                if (dt != null && !dt.HasErrors && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        users.Add(new OutputUserBase
                        {
                            UserId = (Guid)row["UserId"],
                            UserName = (string)row["UserName"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
            }

            return users;
        }

    }
}

