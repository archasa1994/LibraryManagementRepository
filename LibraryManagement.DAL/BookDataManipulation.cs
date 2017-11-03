using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryManagement.DAL
{
    public class BookDataManipulation
    {
        //DROP DOWN BINDING OF USERID
        public List<SelectListItem> GetUserIDList()
        {

            List<SelectListItem> UserIDList = new List<SelectListItem>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var userList = (from users in entity.User
                                select users).ToList();
                foreach (var user in userList)
                {
                    if(user.IsActive)
                    {
                        SelectListItem newUser = new SelectListItem();
                        newUser.Value = user.UserID.ToString();
                        newUser.Text = user.UserName;
                        UserIDList.Add(newUser);
                    }                   
                }
            }
            return UserIDList;
        }

        //DROP DOWN BINDING OF BOOKID
        public List<SelectListItem> GetBookIDList()
        {
            List<SelectListItem> BookIDList = new List<SelectListItem>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var bookList = (from books in entity.BookDetail
                                select books).ToList();
                foreach (var book in bookList)
                {
                    SelectListItem bookID = new SelectListItem();
                    bookID.Value = book.BookID.ToString();
                    bookID.Text = book.BookName;
                    BookIDList.Add(bookID);
                }
            }
            return BookIDList;
        }

        //DROP DOWN BINDING OF CATEGORY LIST
        public List<SelectListItem> GetCategoryList()
        {
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var categoryList = (from categories in entity.Categories
                                    select categories).ToList();
                foreach (var category in categoryList)
                {
                    SelectListItem newCategory = new SelectListItem();
                    newCategory.Value = category.CategoryID.ToString();
                    newCategory.Text = category.CategoryName;
                    CategoryList.Add(newCategory);
                }
            }
            return CategoryList;
        }

        //DROP DOWN BINDING OF SHELF LIST
        public List<SelectListItem> GetShelfList(int ID)
        {
            List<SelectListItem> ShelfList = new List<SelectListItem>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var shelfList = (from shelfs in entity.shelfcategory
                                 where shelfs.CategoryID == ID
                                 select shelfs).ToList();
                foreach (var shelf in shelfList)
                {
                    var newShelf = entity.Shelf.Single(m => m.ShelfID == shelf.ShelfID);
                    if (newShelf.ShelfCapacity != 0)
                    {
                        SelectListItem shelfID = new SelectListItem();
                        shelfID.Value = shelf.ShelfID.ToString();
                        shelfID.Text = shelf.ShelfID.ToString(); ;
                        ShelfList.Add(shelfID);
                    }
                }
            }
            return ShelfList;
        }


        #region NEW BOOK ISSUED
        public string EnterBookIssued(DailyBookIssues bookIssued)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                try
                {
                    entity.BookIssue.Add(bookIssued);
                    entity.SaveChanges();
                }
                catch(Exception ex)
                {
                    return ex.Message.ToString();
                }   
                return "success";           
            }
        }
        #endregion

        #region MANAGING BOOKS
        public List<BookDetails> ManageBooks()
        {
            List<BookDetails> bookList = new List<BookDetails>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                bookList = entity.BookDetail.ToList();
            }
            return bookList;
        }
        #endregion

        #region ADDING NEW BOOK        
        public void AddNewBook(Books book)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                int shelfID = book.ShelfID;
                entity.Book.Add(book);
                var shelf = entity.Shelf.Single(m => m.ShelfID == shelfID);
                shelf.ShelfCapacity = shelf.ShelfCapacity - 1;
                entity.SaveChanges();
            }
        }

        //ADDING NEW BOOK DETAILS
        public void AddNewBookDetails(BookDetails newbook)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                entity.BookDetail.Add(newbook);
                entity.SaveChanges();
            }
        }
        #endregion

        #region DELETING A BOOK
        //DELETING DATA FROM BOOK DETAILS TABLE
        public void DeleteBookDetails(string bookName)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var book = entity.BookDetail.Single(m => m.BookName == bookName);
                entity.BookDetail.Remove(book);
                entity.SaveChanges();
            }
        }

        //DELETING DATA FROM BOOKS TABLE
        public void DeleteBook(int bookID)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var book = entity.Book.Single(m => m.BookID == bookID);
                entity.Book.Remove(book);
                var bookDetail = entity.Book.Single(m => m.BookID == bookID);
                int shelfID = bookDetail.ShelfID;
                var shelf = entity.Shelf.Single(m => m.ShelfID == shelfID);
                shelf.ShelfCapacity = shelf.ShelfCapacity + 1;
                entity.SaveChanges();
            }
        }
        #endregion

        #region MANAGING CATEGORIES
        public List<BookCategories> ManageCategories()
        {
            List<BookCategories> categoryList = new List<BookCategories>();
            BookCategories category = new BookCategories();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                categoryList = entity.Categories.ToList();
            }
            return categoryList;
        }
        #endregion

        #region ADDING CATEGORY
        public void AddNewCategory(BookCategories newCategory)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                entity.Categories.Add(newCategory);
                entity.SaveChanges();
            }
        }
        #endregion

        #region MANAGING SHELFS

        public List<ShelfDetails> ShelfDetails()
        {
            var shelfs = new List<ShelfDetails>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                shelfs = entity.Shelf.ToList();
            }
            return shelfs;
        }
        public int GetCategory(int ID)
        {
            var shelfCategory = new ShelfCategory();
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                shelfCategory = entity.shelfcategory.Single(m => m.ShelfID == ID);
            }
            return shelfCategory.CategoryID;
        }
        #endregion

        #region ADDING NEW SHELF       
        //POST
        public void AddNewShelf(ShelfDetails newShelf)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                entity.Shelf.Add(newShelf);
                entity.SaveChanges();
            }
        }

        //ADDING NEW SHELF IN SHELF CATEGORY TABLE
        public void AddNewShelfCategory(ShelfCategory newShelf)
        {
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                entity.shelfcategory.Add(newShelf);
                entity.SaveChanges();
            }
        }
        #endregion

        #region DAILY BOOK ISSUE
        public List<DailyBookIssues> GetDailyIssueReport()
        {
            var dailyBookIssueList = new List<DailyBookIssues>();
            using (LibraryDatabase entity = new LibraryDatabase())
            {

                dailyBookIssueList = (from bookIssues in entity.BookIssue
                     where bookIssues.IssueDate == DateTime.Today
                     select bookIssues).ToList();
            }
            return dailyBookIssueList;
        }        
        public string GetUserName(int userID)
        {
            string userName;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.User.Single(s => s.UserID == userID);
                userName = user.UserName;
            }
            return userName;
        }

        public string GetUserEmail(int userID)
        {
            string email;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var user = entity.User.Single(s => s.UserID == userID);
                email = user.Email;
            }
            return email;
        }

        public string GetBookName(int bookID)
        {
            string bookName;
            using (LibraryDatabase entity = new LibraryDatabase())
            {
                var book = entity.BookDetail.Single(s => s.BookID == bookID);
                bookName = book.BookName;
            }
            return bookName;
        }
        #endregion
    }
}
