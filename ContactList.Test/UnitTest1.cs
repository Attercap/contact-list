using NUnit.Framework;
using System;
using System.Threading;
using ContactList.Business;
using System.Collections.Generic;

namespace ContactList.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            AppSettings.Initialize();
        }

        [Test]
        public void UserCreationStressTest()
        {
            if (string.IsNullOrWhiteSpace(AppSettings.ConnectionString))
            {
                AppSettings.Initialize();
            }

            bool wasExceptionThrown = false;
            var threads = new Thread[100];
            for (int i = 0; i < 100; i++)
            {
                threads[i] =
                    new Thread(new ThreadStart((Action)(() =>
                    {
                        try
                        {
                        //use the password generator to create a random username, we'll fill the user with that
                        string testname = PasswordGenerator.GeneratePassword(8, PasswordStrengths.AlphaOnly);
                            AppUserManager.CreateUser(new AppUser
                            {
                                UserName = testname,
                                Password = testname,
                                EmailAddress = testname + "@gmail.com",
                                FirstName = testname,
                                LastName = testname
                            });
                        }
                        catch (Exception)
                        {
                            wasExceptionThrown = true;
                        }

                    })));
            }

            for (int i = 0; i < 100; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < 100; i++)
            {
                threads[i].Join();
            }

            Assert.That(wasExceptionThrown, Is.False);
        }

        [Test]
        public void CreateUserAndContactTest()
        {
            bool wasExceptionThrown = false;

            if (string.IsNullOrWhiteSpace(AppSettings.ConnectionString))
            {
                AppSettings.Initialize();
            }

            //use the password generator to create a random username, we'll fill the user with that
            string testname = PasswordGenerator.GeneratePassword(8, PasswordStrengths.AlphaOnly);

            AppUser user = new AppUser
            {
                UserName = testname,
                FirstName = "Bot",
                LastName = "Builder",
                EmailAddress = "test@tester.com",
                Password = testname
            };

            AppUserReturn createReturn = AppUserManager.CreateUser(user);

            wasExceptionThrown = createReturn.HasErrors;

            if(!wasExceptionThrown)
            {
                DtoBase contactReturn = ContactManager.AddContact(new ContactRow
                {
                    UserName = testname,
                    FirstName = "first name",
                    LastName = "last name",
                    EmailAddress = "test@testing.com",
                    StreetAddress1 = "street 1",
                    StreetAddress2 = "",
                    City = "Portland",
                    StateProvince = "OR",
                    PostalCode = "97232",
                    Country = "USA"
                });

                wasExceptionThrown = contactReturn.HasErrors;
            }

            Assert.That(wasExceptionThrown, Is.False);
        }

        [Test]
        public void ContactCreationStressTest()
        {
            if(string.IsNullOrWhiteSpace(AppSettings.ConnectionString))
            {
                AppSettings.Initialize();
            }

            List<AppUser> users = AppUserManager.GetUsers(100);

            bool wasExceptionThrown = false;
            var threads = new Thread[users.Count];
            for (int i = 0; i < users.Count; i++)
            {
                threads[i] =
                    new Thread(new ThreadStart((Action)(() =>
                    {
                        try
                        {
                            Random rnd = new Random();
                            AppUser user = users[rnd.Next(0, users.Count-1)];

                            DtoBase contactReturn = ContactManager.AddContact(new ContactRow
                            {
                                UserName = user.UserName,
                                FirstName = "first name",
                                LastName = "last name",
                                EmailAddress = "test@testing.com",
                                StreetAddress1 = "street 1",
                                StreetAddress2 = "",
                                City = "Portland",
                                StateProvince = "OR",
                                PostalCode = "97232",
                                Country = "USA"
                            });

                            wasExceptionThrown = contactReturn.HasErrors;
                        }
                        catch (Exception)
                        {
                            wasExceptionThrown = true;
                        }

                    })));
            }

            for (int i = 0; i < users.Count; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < users.Count; i++)
            {
                threads[i].Join();
            }

            Assert.That(wasExceptionThrown, Is.False);
        }
    }
}