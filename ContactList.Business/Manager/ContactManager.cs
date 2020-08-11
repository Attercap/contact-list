using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Handles base contact list functions for a user (create, get, edit, delete)
    /// </summary>
    public static class ContactManager
    {
        /// <summary>
        /// Adds contact row to users's contact datatable
        /// </summary>
        /// <param name="contact">ContactEdit with contact data</param>
        /// <returns>DtoBase with any error messaging</returns>
        public static DtoReturnBase AddContact(InputContactRecord contact)
        {
            if(contact.UserId == Guid.Empty)
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "User missing from transaction, please log in."
                };
            }

            string contactTableBaseString = GetContactTableBaseString(contact.UserId);
            if(!ContactTableExists(contactTableBaseString))
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "Contact table does not exist and could not be created."
                };
            }

            try
            {
                contact.ContactId = Guid.NewGuid();

                string insertContact = "insert into Contact_{0}(ContactId,FirstName,LastName,EmailAddress,StreetAddress1,StreetAddress2,City,StateProvince,PostalCode,Country,LastModified)" +
                    "values(@contactId,@firstName,@lastName,@emailAddress,@streetAddress1,@streetAddress2,@city,@stateProvince,@postalCode,@country,@lastModified)";
                insertContact = string.Format(insertContact, contactTableBaseString);

                List<SqlParameter> insertParams = new List<SqlParameter>
                {
                    new SqlParameter("@contactId",contact.ContactId),
                    new SqlParameter("@firstName",contact.FirstName),
                    new SqlParameter("@lastName",contact.LastName),
                    new SqlParameter("@emailAddress",contact.EmailAddress),
                    new SqlParameter("@streetAddress1",contact.StreetAddress1),
                    new SqlParameter("@streetAddress2",contact.StreetAddress2),
                    new SqlParameter("@city",contact.City),
                    new SqlParameter("@stateProvince",contact.StateProvince),
                    new SqlParameter("@postalCode",contact.PostalCode),
                    new SqlParameter("@country",contact.Country),
                    new SqlParameter("@lastModified",DateTime.UtcNow),
                };

                if(!DataConnection.ExecuteNonQuery(insertContact,insertParams))
                {
                    return new DtoReturnBase
                    {
                        HasErrors = true,
                        DtoMessage = "Error saving contact row."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "A general error occured creating the contact row."
                };
            }

            return new DtoReturnBase
            {
                HasErrors = false
            };          
        }

        /// <summary>
        /// Edits a contact row in users's contact datatable
        /// </summary>
        /// <param name="contact">ContactEdit with contact data</param>
        /// <returns>DtoBase with any error messaging</returns>
        public static DtoReturnBase EditContact(InputContactRecord contact)
        {
            if (contact.UserId == Guid.Empty || contact.ContactId == null || contact.ContactId == Guid.Empty)
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "Core data missing from transaction."
                };
            }

            string contactTableBaseString = GetContactTableBaseString(contact.UserId);
            if (!ContactTableExists(contactTableBaseString))
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "Contact table does not exist and could not be created."
                };
            }

            try
            {
                string updateContact = "update Contact_{0} set" +
                    " FirstName = @firstName," +
                    " LastName = @lastName," +
                    " EmailAddress = @emailAddress," +
                    " StreetAddress1 = @streetAddress1," +
                    " StreetAddress2 = @streetAddress2," +
                    " City = @city," +
                    " StateProvince = @stateProvince," +
                    " PostalCode = @postalCode," +
                    " Country = @country," +
                    " LastModified = @lastModified" +
                    " where ContactId = @contactId";
                updateContact = string.Format(updateContact, contactTableBaseString);

                List<SqlParameter> updateParams = new List<SqlParameter>
                {
                    new SqlParameter("@contactId",(Guid)contact.ContactId),
                    new SqlParameter("@firstName",contact.FirstName),
                    new SqlParameter("@lastName",contact.LastName),
                    new SqlParameter("@emailAddress",contact.EmailAddress),
                    new SqlParameter("@streetAddress1",contact.StreetAddress1),
                    new SqlParameter("@streetAddress2",contact.StreetAddress2),
                    new SqlParameter("@city",contact.City),
                    new SqlParameter("@stateProvince",contact.StateProvince),
                    new SqlParameter("@postalCode",contact.PostalCode),
                    new SqlParameter("@country",contact.Country),
                    new SqlParameter("@lastModified",DateTime.UtcNow),
                };

                if (!DataConnection.ExecuteNonQuery(updateContact, updateParams))
                {
                    return new DtoReturnBase
                    {
                        HasErrors = true,
                        DtoMessage = "Error saving contact row."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "A general error occured editing the contact row."
                };
            }

            return new DtoReturnBase
            {
                HasErrors = false
            };
        }

        /// <summary>
        /// Removes a contact row in users's contact datatable
        /// </summary>
        /// <param name="contact">ContactRow with base contact data</param>
        /// <returns>DtoBase with any error messaging</returns>
        public static DtoReturnBase DeleteContact(InputContactDelete contact)
        {
            if (contact.ContactId == Guid.Empty || contact.UserId == Guid.Empty)
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "Core data missing from transaction, please log in."
                };
            }

            string contactTableBaseString = GetContactTableBaseString(contact.UserId);
            if (!ContactTableExists(contactTableBaseString))
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "Contact table does not exist and could not be created."
                };
            }

            try
            {
                string updateContact = "delete from Contact_{0} where ContactId = @contactId";
                updateContact = string.Format(updateContact, contactTableBaseString);

                List<SqlParameter> updateParams = new List<SqlParameter>
                {
                    new SqlParameter("@contactId",contact.ContactId)
                };

                if (!DataConnection.ExecuteNonQuery(updateContact, updateParams))
                {
                    return new DtoReturnBase
                    {
                        HasErrors = true,
                        DtoMessage = "Error removing contact row."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "A general error occured removing the contact row."
                };
            }

            return new DtoReturnBase
            {
                HasErrors = false
            };
        }

        /// <summary>
        /// Gets total count of contacts for user, used to determine page count
        /// </summary>
        /// <param name="getter">ContactGet object for data transfer of username</param>
        /// <returns>DtoBase with error message or count of users in message</returns>
        public static DtoReturnBase GetContactCount(InputContactCountGet getter)
        {
            if (getter.UserId == Guid.Empty)
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "User missing from transaction, please log in."
                };
            }

            string contactTableBaseString = GetContactTableBaseString(getter.UserId);
            if (!ContactTableExists(contactTableBaseString))
            {
                return new DtoReturnBase
                {
                    HasErrors = true,
                    DtoMessage = "Contact table does not exist and could not be created."
                };
            }

            string contactCount = "select count(*) from Contact_{0}";
            contactCount = string.Format(contactCount, contactTableBaseString);

            return new DtoReturnBase
            {
                HasErrors = false,
                DtoMessage = DataConnection.ExecuteScalarInt(contactCount, new List<SqlParameter>()).ToString()
            };
        }

        /// <summary>
        /// Gets paged contacts for user (lots of room for new features/expansions)
        /// </summary>
        /// <param name="getter">ContactGet object with desired params</param>
        /// <returns>List of found ContactRow objects</returns>
        public static DtoReturnObject<List<OutputContactRecord>> GetContacts(InputContactListSelect getter)
        {
            if (getter.UserId == Guid.Empty)
            {
                return new DtoReturnObject<List<OutputContactRecord>>(true, "User missing from transaction, please log in.", null);
            }

            string contactTableBaseString = GetContactTableBaseString(getter.UserId);
            if (!ContactTableExists(contactTableBaseString))
            {
                return new DtoReturnObject<List<OutputContactRecord>>(true, "Contact table does not exist and could not be created.", null);
            }

            List<OutputContactRecord> contacts = new List<OutputContactRecord>();

            string contactSelect = "select * from" +
                " (select row_number() over (order by LastName) as RowNum, *" +
                " from Contact_{0} ) as RowConstrainedResult" +
                " where RowNum >= {1}" +
                " and RowNum <= {2}" +
                " order by RowNum";

            contactSelect = string.Format(contactSelect, contactTableBaseString, (((getter.PageNumber - 1) * getter.RowsPerPage) + 1).ToString(), ((getter.PageNumber) * getter.RowsPerPage).ToString());

            try
            {
                DataTable dt = DataConnection.ExecuteQuery(contactSelect, new List<SqlParameter>());

                if (dt != null && !dt.HasErrors && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        contacts.Add(new OutputContactRecord
                        {
                            ContactId = (Guid)row["ContactId"],
                            FirstName = (string)row["FirstName"],
                            LastName = (string)row["LastName"],
                            EmailAddress = (string)row["EmailAddress"],
                            StreetAddress1 = (string)row["StreetAddress1"],
                            StreetAddress2 = (string)row["StreetAddress2"],
                            City = (string)row["City"],
                            StateProvince = (string)row["StateProvince"],
                            PostalCode = (string)row["PostalCode"],
                            Country = (string)row["Country"],
                            LastModifiedFormatted = StringUtil.DisplayDate((DateTime)row["LastModified"], true, getter.UtcOffset)
                        });
                    }
                }
            } catch(Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoReturnObject<List<OutputContactRecord>>(true,"An error occured accessing your contacts, please contact administration.",null);
            }

            return new DtoReturnObject<List<OutputContactRecord>>(false, string.Empty, contacts);
        }

        /// <summary>
        /// Verifies Contact Table exists; adds some minor weight to methods but ensures fewer errors
        /// </summary>
        /// <param name="contactTableBaseName">string table basename</param>
        /// <returns>bool of success/failure</returns>
        internal static bool ContactTableExists(string contactTableBaseName)
        {
            string checkTableExists = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'Contact_'+@username";
            List<SqlParameter> checkTableParams = new List<SqlParameter>
            {
                    new SqlParameter("@userName", contactTableBaseName)
            };

            if (DataConnection.ExecuteScalarInt(checkTableExists, checkTableParams) == 0)
            {
                return CreateContactTable(contactTableBaseName);
            }

            return false;
        }

        /// <summary>
        /// Creates the custom contact table for a user
        /// </summary>
        /// <param name="contactTableBaseName">string of contact table extension</param>
        /// <returns>bool of success/failure</returns>
        internal static bool CreateContactTable(string contactTableBaseName)
        {
            if (string.IsNullOrWhiteSpace(contactTableBaseName))
            {
                return false;
            }

            string createtable = "CREATE TABLE [dbo].[Contact_{0}](" +
                "[ContactId][uniqueidentifier] NOT NULL," +
                "[FirstName] [nvarchar](256) NOT NULL," +
                "[LastName] [nvarchar](256) NOT NULL," +
                "[EmailAddress] [nvarchar](256) NOT NULL," +
                "[StreetAddress1] [nvarchar](256) NOT NULL," +
                "[StreetAddress2] [nvarchar](256) NOT NULL," +
                "[City] [nvarchar](256) NOT NULL," +
                "[StateProvince] [nvarchar](256) NOT NULL," +
                "[PostalCode] [nvarchar](256) NOT NULL," +
                "[Country] [nvarchar](256) NOT NULL," +
                "[LastModified] [datetime] NOT NULL," +
                "CONSTRAINT[PK_Contact{0}] PRIMARY KEY CLUSTERED" +
                "(" +
                    " [ContactId] ASC" +
                    ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                ") ON[PRIMARY]";

            createtable = string.Format(createtable, contactTableBaseName);
            if (!DataConnection.ExecuteNonQuery(createtable, new List<SqlParameter>()))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the custom contact table for a user
        /// </summary>
        /// <param name="userId">Guid of user Id</param>
        /// <returns>bool of success/failure</returns>
        internal static bool CreateContactTable(Guid userId)
        {
            string contactTableBaseName = GetContactTableBaseString(userId);
            return CreateContactTable(contactTableBaseName);
        }

        /// <summary>
        /// Derives the user's custom contact table name extenstion string from a guid
        /// </summary>
        /// <param name="userId">Guid of user's Id</param>
        /// <returns>string of stripped and lowered characters</returns>
        internal static string GetContactTableBaseString(Guid userId)
        {
            try
            {
                return userId.ToString().ToLower().Replace("-", string.Empty);
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return string.Empty;
            }
        }
    }
}
