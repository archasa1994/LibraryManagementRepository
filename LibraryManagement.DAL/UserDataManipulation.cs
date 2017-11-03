using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DAL
{
    public class UserDataManipulation
    {
        public List<Users> ManageUsers()
        {
            List<Users> userList = new List<Users>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                userList = entity.User.ToList();
            }
            return userList;
        }

        public Users GetUserDetails(int userID)
        {
            var user = new Users();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                user = entity.User.Single(m => m.UserID == userID);
            }
            return user;
        }

        public List<DailyBookIssues> GetBookDetails(int userID)
        {
            var bookIssues = new List<DailyBookIssues>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                bookIssues = entity.BookIssue.Where(m => m.UserID == userID).ToList();
            }
            return bookIssues;
        }

        public string GetBookName(int bookID)
        {
            var bookdetails = new BookDetails();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                bookdetails = entity.BookDetail.Single(m => m.BookID == bookID);
            }
            return bookdetails.BookName;
        }

        public void DeleteUser(Users user)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var deleteUser = entity.User.Single(m => m.UserID == user.UserID);
                entity.User.Remove(deleteUser);
                entity.SaveChanges();
            }
        }

        public void AddNewUser(Users user)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                entity.User.Add(user);
                entity.SaveChanges();
            }
        }

        public decimal GetFine(double days, int userID)
        {
            decimal fine = 0;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.User.Single(m => m.UserID == userID);
                {
                    if (user.IsActive == true)
                    {
                        if (days == 7)
                        {
                            fine = 5;

                        }
                        else if (days == 15)
                        {
                            fine = 50;

                        }
                        else if (days == 31)
                        {
                            fine = fine + 750;

                        }
                        else if (days == 181)
                        {
                            fine = 2500;

                        }
                        else if (days > 365)
                        {
                            fine = 5000;
                            user.IsActive = false;
                            entity.SaveChanges();
                        }
                    }
                }
            }
            return fine;
        }

        public void EnterUserFine(decimal fine, int userID)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.User.Single(m => m.UserID == userID);
                user.Fine = user.Fine + fine;
                entity.SaveChanges();
            }
        }
        public void CalculateFine()
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var users = entity.BookIssue.ToList();
                foreach (var userissue in users)
                {
                    double numberofdays = (DateTime.Today - userissue.IssueDate).TotalDays;
                    if (numberofdays > 6 && (!userissue.IsFine))
                    {
                        userissue.IsFine = true;
                        userissue.fine = userissue.fine + GetFine(numberofdays, userissue.UserID);
                        EnterUserFine(userissue.fine, userissue.UserID);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public List<Users> GetLockedUsers()
        {
            List<Users> lockedUserList = new List<Users>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                lockedUserList = entity.User.Where(m => m.IsActive == false).ToList();
            }
            return lockedUserList;
        }

        public void UnlockUser(int userID, string userName)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.User.Single(m => m.UserID == userID);
                user.IsActive = true;
                user.Fine = 0;
                var userEntries = entity.BookIssue.Where(m => m.UserID == userID).ToList();
                foreach (var userEntry in userEntries)
                {
                    entity.BookIssue.Remove(userEntry);
                    entity.SaveChanges();
                }
                entity.SaveChanges();
            }
        }

        public decimal GetUserFine(int ID)
        {
            decimal fine;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.User.Single(m => m.UserID == ID);
                fine = user.Fine;
            }
            return fine;
        }

        public List<Users> GetUsers()
        {
            DateTime today = DateTime.Today;
            List<Users> UserList = new List<Users>();
            BookDataManipulation bookData = new BookDataManipulation();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var users = entity.BookIssue.ToList();
                foreach (var user in users)
                {
                    double numberofdays = (DateTime.Today - user.IssueDate).TotalDays;
                    if (numberofdays > 30)
                    {
                        Users newUser = new Users();
                        newUser.UserID = user.UserID;
                        newUser.UserName = bookData.GetUserName(user.UserID);
                        newUser.Email = bookData.GetUserEmail(user.UserID);
                        UserList.Add(newUser);
                    }
                }
            }
            return UserList;
        }
    }
}
