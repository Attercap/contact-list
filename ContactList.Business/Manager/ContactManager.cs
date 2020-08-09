using Microsoft.Data.SqlClient;
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
        /// <param name="contact">ContactRow with contact data</param>
        /// <returns>DtoBase with any error messaging</returns>
        public static DtoBase AddContact(ContactRow contact)
        {
            if(string.IsNullOrWhiteSpace(contact.UserName))
            {
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "Usename missing from transaction, please log in."
                };
            }

            DtoBase verifyTable = VerifyContactTable(contact.UserName);

            if (verifyTable.HasErrors)
            {
                return verifyTable;
            }

            try
            {
                contact.ContactId = Guid.NewGuid();

                string insertContact = "insert into Contact_{0}(ContactId,FirstName,LastName,EmailAddress,StreetAddress1,StreetAddress2,City,StateProvince,PostalCode,Country,LastModified)" +
                    "values(@contactId,@firstName,@lastName,@emailAddress,@streetAddress1,@streetAddress2,@city,@stateProvince,@postalCode,@country,@lastModified)";
                insertContact = string.Format(insertContact, contact.UserName);

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
                    return new DtoBase
                    {
                        HasErrors = true,
                        DtoMessage = "Error saving contact row."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "A general error occured creating the contact row."
                };
            }

            return new DtoBase
            {
                HasErrors = false
            };          
        }

        /// <summary>
        /// Edits a contact row in users's contact datatable
        /// </summary>
        /// <param name="contact">ContactRow with contact data</param>
        /// <returns>DtoBase with any error messaging</returns>
        public static DtoBase EditContact(ContactRow contact)
        {
            if (string.IsNullOrWhiteSpace(contact.UserName))
            {
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "Usename missing from transaction, please log in."
                };
            }

            DtoBase verifyTable = VerifyContactTable(contact.UserName);

            if (verifyTable.HasErrors)
            {
                return verifyTable;
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
                updateContact = string.Format(updateContact, contact.UserName);

                List<SqlParameter> updateParams = new List<SqlParameter>
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

                if (!DataConnection.ExecuteNonQuery(updateContact, updateParams))
                {
                    return new DtoBase
                    {
                        HasErrors = true,
                        DtoMessage = "Error saving contact row."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "A general error occured editing the contact row."
                };
            }

            return new DtoBase
            {
                HasErrors = false
            };
        }

        /// <summary>
        /// Removes a contact row in users's contact datatable
        /// </summary>
        /// <param name="contact">ContactRow with base contact data</param>
        /// <returns>DtoBase with any error messaging</returns>
        public static DtoBase DeleteContact(ContactRow contact)
        {
            if (string.IsNullOrWhiteSpace(contact.UserName))
            {
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "Usename missing from transaction, please log in."
                };
            }

            DtoBase verifyTable = VerifyContactTable(contact.UserName);

            if (verifyTable.HasErrors)
            {
                return verifyTable;
            }

            try
            {
                string updateContact = "delete from Contact_{0} where ContactId = @contactId";
                updateContact = string.Format(updateContact, contact.UserName);

                List<SqlParameter> updateParams = new List<SqlParameter>
                {
                    new SqlParameter("@contactId",contact.ContactId)
                };

                if (!DataConnection.ExecuteNonQuery(updateContact, updateParams))
                {
                    return new DtoBase
                    {
                        HasErrors = true,
                        DtoMessage = "Error removing contact row."
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "A general error occured removing the contact row."
                };
            }

            return new DtoBase
            {
                HasErrors = false
            };
        }

        /// <summary>
        /// Gets total count of contacts for user, used to determine page count
        /// </summary>
        /// <param name="getter">ContactGet object for data transfer of username</param>
        /// <returns>DtoBase with error message or count of users in message</returns>
        public static DtoBase GetContactCount(ContactGet getter)
        {
            if (string.IsNullOrWhiteSpace(getter.UserName))
            {
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "Usename missing from transaction, please log in."
                };
            }

            DtoBase verifyTable = VerifyContactTable(getter.UserName);
            if(verifyTable.HasErrors)
            {
                return verifyTable;
            }

            string contactCount = "select count(*) from Contact_{0}";
            contactCount = string.Format(contactCount, getter.UserName);

            return new DtoBase
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
        public static List<ContactRow> GetContacts(ContactGet getter)
        {
            List<ContactRow> contacts = new List<ContactRow>();

            string contactSelect = "select * from" +
                " (select row_number() over (order by LastName) as RowNum, *" +
                " from Contact_{0} ) as RowConstrainedResult" +
                " where RowNum >= {1}" +
                " and RowNum <= {2}" +
                " order by RowNum";

            contactSelect = string.Format(contactSelect, getter.UserName, (((getter.PageNumber - 1) * getter.RowsPerPage) + 1).ToString(), ((getter.PageNumber) * getter.RowsPerPage).ToString());

            try
            {
                DataTable dt = DataConnection.ExecuteQuery(contactSelect, new List<SqlParameter>());

                if (dt != null && !dt.HasErrors && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        contacts.Add(new ContactRow
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
            }

            return contacts;
        }

        /// <summary>
        /// Verifies Contact Table exists; adds some minor weight to methods but ensures fewer errors
        /// </summary>
        /// <param name="username">string username of user</param>
        /// <returns>DtoBase with any pertinent messaging in case of error</returns>
        internal static DtoBase VerifyContactTable(string username)
        {
            string checkTableExists = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'Contact_'+@username";
            List<SqlParameter> checkTableParams = new List<SqlParameter>
            {
                    new SqlParameter("@userName", username)
            };

            if (DataConnection.ExecuteScalarInt(checkTableExists, checkTableParams) == 0)
            {
                return CreateContactTable(username);
            }

            return new DtoBase
            {
                HasErrors = false
            };
        }

        /// <summary>
        /// Creates the custom contact table for a user
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>DtoBase with any pertinent messaging in case of error</returns>
        internal static DtoBase CreateContactTable(string username)
        {
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

            createtable = string.Format(createtable, username);
            if (!DataConnection.ExecuteNonQuery(createtable, new List<SqlParameter>()))
            {
                return new DtoBase
                {
                    HasErrors = true,
                    DtoMessage = "Error createing contact table. Contacts cannot be saved. Please contact an administrator."
                };
            }

            return new DtoBase
            {
                HasErrors = false
            };
        }
    }
}
