using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace LibraryManagement.BAL
{
    public class UserManager
    {
        public UserListViewModel ManageUsers()
        {
            UserDataManipulation maintain = new UserDataManipulation();
            UserListViewModel userListViewModel = new UserListViewModel();
            List<UserViewModel> userList = new List<UserViewModel>();
            List<Users> users = maintain.ManageUsers();
            foreach (var user in users)
            {
                UserViewModel userModel = new UserViewModel();
                userModel.UserID = user.UserID;
                userModel.UserName = user.UserName;
                userModel.Email = user.Email;
                userModel.Fine = user.Fine;
                userList.Add(userModel);
            }
            userListViewModel.UserList = userList;
            return userListViewModel;
        }

        public UserDetailsViewModel GetUserDetails(int userID)
        {
            UserDataManipulation manipulate = new UserDataManipulation();
            Users user = manipulate.GetUserDetails(userID);
            UserDetailsViewModel userDetails = new UserDetailsViewModel();
            List<UserBookDetails> booklist = new List<UserBookDetails>();
            userDetails.UserName = user.UserName;
            userDetails.Email = user.Email;
            userDetails.Fine = user.Fine;
            List<DailyBookIssues> issuedBookList = manipulate.GetBookDetails(userID);
            foreach(var book in issuedBookList)
            {
                UserBookDetails userBook = new UserBookDetails();
                userBook.BookID = book.BookID;
                userBook.BookName = manipulate.GetBookName(book.BookID);
                userBook.IssueDate = book.IssueDate;
                userBook.ReturnDate = book.ReturnDate;
                userBook.Fine = book.fine;
                booklist.Add(userBook);
            }
            userDetails.BookList = booklist;
            return userDetails;
        }

        public void DeleteUser(DeleteUserViewModel user)
        {
            UserDataManipulation maintain = new UserDataManipulation();
            Users deleteUser = new Users();
            deleteUser.UserID = user.UserID;
            deleteUser.UserName = user.UserName;
            maintain.DeleteUser(deleteUser);
        }

        public void AddNewUser(AddNewUserViewModel newUser)
        {
            UserDataManipulation maintain = new UserDataManipulation();
            Users user = new Users();
            user.UserName = newUser.UserName;
            user.Email = newUser.Email;
            maintain.AddNewUser(user);
        }

        public void CalculateFine()
        {
            UserDataManipulation user = new UserDataManipulation();
            user.CalculateFine();
        }

        public UserListViewModel GetLockedUsers()
        {
            UserDataManipulation manipulate = new UserDataManipulation();
            List<Users> lockedUsers = manipulate.GetLockedUsers();
            UserListViewModel lockedUserList = new UserListViewModel();
            List<UserViewModel> userList = new List<UserViewModel>();
            foreach (var lockedUser in lockedUsers)
            {
                UserViewModel user = new UserViewModel();
                user.UserID = lockedUser.UserID;
                user.UserName = lockedUser.UserName;
                user.Email = lockedUser.Email;
                user.Fine = lockedUser.Fine;
                userList.Add(user);
            }
            lockedUserList.UserList = userList;
            return lockedUserList;
        }

        public void UnlockUser(int userID, string userName)
        {
            UserDataManipulation manipulate = new UserDataManipulation();
            manipulate.UnlockUser(userID, userName);
        }

        public decimal GetUserFine(int ID)
        {
            UserDataManipulation manipulate = new UserDataManipulation();
            return (manipulate.GetUserFine(ID));
        }

        public UserListViewModel GetUsers()
        {
            UserDataManipulation manipulate = new UserDataManipulation();
            List<Users> users = manipulate.GetUsers();
            UserListViewModel userListViewModel = new UserListViewModel();
            List<UserViewModel> userList = new List<UserViewModel>();
            foreach (var item in users)
            {
                UserViewModel user = new UserViewModel();
                user.UserID = item.UserID;
                user.UserName = item.UserName;
                user.Email = item.Email;
                user.Fine = item.Fine;
                userList.Add(user);
            }
            userListViewModel.UserList = userList;
            return userListViewModel;
        }
    }
}
