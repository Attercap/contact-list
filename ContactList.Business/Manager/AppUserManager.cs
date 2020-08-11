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
        /// <returns>AppUserReturn to populate user session informationt</returns>
        public static AppUserReturn CreateUser(Register user)
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
                    return new AppUserReturn
                    {
                        HasErrors = true,
                        DtoMessage = "This UserName already exists in the system. Please try again."
                    };
                }


                user.UserId = Guid.NewGuid();

                string insertUser = "insert into AppUser([UserId],[UserName],[Password],[EmailAddress],[FirstName],[LastName],[CreateDate],[LastLogin]) " +
                    "values(@userId,@userName,@password,@emailAddress,@firstName,@lastName,@createDate,@lastLogin)";

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@userId", user.UserId),
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
                    DtoBase tableCreate = ContactManager.CreateContactTable(user.UserName);

                    if (tableCreate.HasErrors)
                    {
                        return (AppUserReturn)tableCreate;
                    }
                }
                else
                {
                    return new AppUserReturn
                    {
                        HasErrors = true,
                        DtoMessage = "Could not create user record."
                    };
                }

                return new AppUserReturn
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    HasErrors = false
                };
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return new AppUserReturn
                {
                    HasErrors = true,
                    DtoMessage = "An unknown error occured, please contact support."
                };
            }
        }

        /// <summary>
        /// Verifies user/password and, if correct, updates last login date
        /// </summary>
        /// <param name="user">AppUserLogin object for user/pass string</param>
        /// <returns>AppUserReturn to populate common session data</returns>
        public static AppUserReturn LoginUser(AppUserLogin user)
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
                    AppUserReturn appUserReturn = new AppUserReturn
                    {
                        UserId = (Guid)dt.Rows[0]["UserId"],
                        UserName = (string)dt.Rows[0]["UserName"],
                        FirstName = (string)dt.Rows[0]["FirstName"],
                        LastName = (string)dt.Rows[0]["LastName"],
                        EmailAddress = (string)dt.Rows[0]["EmailAddress"],
                        HasErrors = false,
                        DtoMessage = string.Empty
                    };

                    string loginUser = "update AppUser set LastLogin = @lastLogin where UserId = @userId";

                    List<SqlParameter> loginParams = new List<SqlParameter>
                    {
                        new SqlParameter("@lastLogin", DateTime.UtcNow),
                        new SqlParameter("@userId", appUserReturn.UserId)
                    };

                    if (!DataConnection.ExecuteNonQuery(loginUser, loginParams))
                    {
                        appUserReturn.HasErrors = true;
                        appUserReturn.DtoMessage = "A minor error occurred updating your login date.";
                    }

                    return appUserReturn;
                }
                else
                {
                    return new AppUserReturn
                    {
                        HasErrors = true,
                        DtoMessage = "Your username/password combination is incorrect."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return new AppUserReturn
                {
                    HasErrors = true,
                    DtoMessage = "An unknown error occured, please contact support."
                };
            }
        }

        /// <summary>
        /// Updates a user's email, first name, last name, and password (if provided)
        /// </summary>
        /// <param name="user">AppUser object</param>
        /// <returns>DtoBase of success/fail with message</returns>
        public static DtoBase UpdateUser(Update user)
        {
            try
            {
                string checkUser = "select count(*) from AppUser where UserId = @userId";
                List<SqlParameter> checkParms = new List<SqlParameter>{
                    new SqlParameter("@userId", user.UserId)
                };

                if (DataConnection.ExecuteScalarInt(checkUser, checkParms) > 0)
                {
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

                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        updateUser += " ,[Password] = @password";
                        updateParams.Add(new SqlParameter("@password", StringUtil.HashPassword(user.Password)));
                    }

                    updateUser += " where UserId = @userId";


                    if (DataConnection.ExecuteNonQuery(updateUser, updateParams))
                    {
                        return new DtoBase
                        {
                            HasErrors = false
                        };
                    }
                    else
                    {
                        return new DtoBase
                        {
                            HasErrors = true,
                            DtoMessage = "Error updating information."
                        };
                    }
                }
                else
                {
                    return new AppUserReturn
                    {
                        HasErrors = true,
                        DtoMessage = "Your user account cannot be found. Please log in again."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);

                return new AppUserReturn
                {
                    HasErrors = true,
                    DtoMessage = "An unknown error occured, please contact support."
                };
            }
        }


        /// <summary>
        /// Returns list of top N users, used for stress testing
        /// </summary>
        /// <param name="userCount">int number of users to be returned</param>
        /// <returns>List of N AppUsers</returns>
        public static List<Register> GetUsers(int userCount)
        {
            List<Register> users = new List<Register>();

            try
            {
                string listUser = "select top {0} * from AppUser";
                listUser = string.Format(listUser, userCount.ToString());

                DataTable dt = DataConnection.ExecuteQuery(listUser, new List<SqlParameter>());

                if (dt != null && !dt.HasErrors && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        users.Add(new Register
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

