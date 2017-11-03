using LibraryManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModels;

namespace LibraryManagement.BAL
{
    public class BookManager
    {
        #region BOOK ISSUAL
        //GET
        public BookIssueViewModel BookIssual()
        {
            BookDataManipulation book = new BookDataManipulation();
            BookIssueViewModel issuemodel = new BookIssueViewModel();
            issuemodel.UserID = book.GetUserIDList();
            issuemodel.BookID = book.GetBookIDList();
            return issuemodel;
        }

        //POST
        public string EnterBookIssued(BookIssueViewModel bookIssued)
        {            
            BookDataManipulation manipulate = new BookDataManipulation();
            DailyBookIssues newBookIssued = new DailyBookIssues();
            newBookIssued.UserID = bookIssued.SelectedUserID;
            newBookIssued.BookID = bookIssued.SelectedBookID;
            newBookIssued.IssueDate = bookIssued.IssueDate;
            newBookIssued.ReturnDate = DateTime.Now.AddDays(7);           
            return(manipulate.EnterBookIssued(newBookIssued));
        }
        #endregion

        #region MANAGING BOOKS
        public BookListViewModel ManageBooks()
        {
            BookDataManipulation maintain = new BookDataManipulation();
            BookListViewModel bookListViewModel = new BookListViewModel();
            List<BookViewModel> bookList = new List<BookViewModel>();
            List<BookDetails> newBookList = maintain.ManageBooks();
            foreach (var item in newBookList)
            {
                BookViewModel book = new BookViewModel();
                book.BookID = item.BookID;
                book.BookName = item.BookName;
                book.Author = item.Author;
                bookList.Add(book);
            }
            bookListViewModel.BookList = bookList;
            return bookListViewModel;
        }
        #endregion

        #region ADDING NEW BOOK
        //GET
        public AddNewBookViewModel NewBook()
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            AddNewBookViewModel newbook = new AddNewBookViewModel();
            newbook.CategoryID = manipulate.GetCategoryList();            
            return newbook;
        }

        //SHELF DROP DOWN BINDING BASED ON CATEGORY
        public List<SelectListItem> GetShelfList(int ID)
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            return(manipulate.GetShelfList(ID));
        }

        //POST
        public void AddNewBook(AddNewBookViewModel newbook)
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            Books book = new Books();
            BookDetails bookdetail = new BookDetails();
            book.CategoryID = newbook.SelectedCategory;
            book.ShelfID = newbook.SelectedShelf;
            bookdetail.BookName = newbook.BookName;
            bookdetail.Author = newbook.Author;
            //ADDING NEW BOOK TO BOOKS TABLE
            manipulate.AddNewBook(book);
            bookdetail.BookID = book.BookID;
            //ADDING NEW BOOK TO BOOK DETAILS TABLE
            manipulate.AddNewBookDetails(bookdetail);
        }
        #endregion

        #region DELETING A BOOK
        public void DeleteBook(DeleteBookViewModel deletebook)
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            string bookName = deletebook.BookName;
            //DELETING BOOK FROM BOOK DETAILS TABLE
            manipulate.DeleteBookDetails(bookName);
            int bookID = deletebook.BookID;
            //DELETING BOOK FROM BOOK TABLE
            manipulate.DeleteBook(bookID);
        }
        #endregion

        #region MANAGING CATEGORIES
        public CategoryListViewModel ManageCategories()
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            List<BookCategories> categoryList = manipulate.ManageCategories();
            CategoryListViewModel categoryListViewModel = new CategoryListViewModel();
            List<CategoryViewModel> newCategoryList = new List<CategoryViewModel>();
            foreach (var category in categoryList)
            {
                CategoryViewModel newCategory = new CategoryViewModel();
                newCategory.CategoryID = category.CategoryID;
                newCategory.CategoryName = category.CategoryName;
                newCategoryList.Add(newCategory);
            }
            categoryListViewModel.CategoryList = newCategoryList;
            return categoryListViewModel;
        }
        #endregion

        #region ADDING NEW CATEGORY
        public void AddNewCategory(AddNewCategoryViewModel category)
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            BookCategories newCategory = new BookCategories();
            newCategory.CategoryName = category.CategoryName;
            //ADDING NEW CATEGORY
            manipulate.AddNewCategory(newCategory);
            //ADDING NEW SHELF
            ShelfDetails newShelf = new ShelfDetails();
            newShelf.ShelfCapacity = category.ShelfCapacity;            
            manipulate.AddNewShelf(newShelf);
            //ADDING NEW CATEGORY IN SHELF CATEGORY TABLE
            ShelfCategory newShelfCategory = new ShelfCategory();
            newShelfCategory.CategoryID = newCategory.CategoryID;
            newShelfCategory.ShelfID = newShelf.ShelfID;
            manipulate.AddNewShelfCategory(newShelfCategory);
        }
        #endregion

        #region MANAGING SHLEFS
        public ShelfListViewModel ShelfDetails()
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            List<ShelfDetails> shelfs = manipulate.ShelfDetails();
            ShelfListViewModel shelfListViewModel = new ShelfListViewModel();
            List<ShelfViewModel> shelfList = new List<ShelfViewModel>();
            foreach (var shelf in shelfs)
            {
                ShelfViewModel s = new ShelfViewModel();
                s.CategoryID = GetCategory(shelf.ShelfID);
                s.ShelfID = shelf.ShelfID;
                s.ShelfCapacity = shelf.ShelfCapacity;
                shelfList.Add(s);
            }
            shelfListViewModel.ShelfList = shelfList;
            return shelfListViewModel;
        }

        //GETTING CATEGORY ID OF A SHELF
        public int GetCategory(int ID)
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            return (manipulate.GetCategory(ID));
        }
        #endregion

        public ViewShelfViewModel GetCategoryList()
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            ViewShelfViewModel shelf = new ViewShelfViewModel();
            shelf.Category = manipulate.GetCategoryList();

            return shelf;
        }

        #region ADDING NEW SHELF
        //GET
        public AddNewShelfViewModel NewShelf()
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            AddNewShelfViewModel newShelf = new AddNewShelfViewModel();
            newShelf.Category = manipulate.GetCategoryList();
            return newShelf;
        }

        //POST
        public void AddNewShelf(AddNewShelfViewModel newshelf)
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            ShelfDetails shelf = new ShelfDetails();
            shelf.ShelfCapacity = newshelf.ShelfCapacity;
            int capacity = newshelf.ShelfCapacity;
            //ADDING NEW SHELF
            manipulate.AddNewShelf(shelf);
            ShelfCategory newshelfcategory = new ShelfCategory();
            newshelfcategory.CategoryID = newshelf.SelectedCategory;
            newshelfcategory.ShelfID = shelf.ShelfID;
            //ADDING NEW SHELF IN SHELF CATEGORY TABLE
            manipulate.AddNewShelfCategory(newshelfcategory);
        }
        #endregion

        #region DAILY ISSUE REPORT
        public DailyIssueReportListViewModel GetDailyIssueReport()
        {
            BookDataManipulation manipulate = new BookDataManipulation();
            DailyIssueReportListViewModel dailyReport = new DailyIssueReportListViewModel();
            List<DailyBookIssues> dailyList = manipulate.GetDailyIssueReport();
            List<DailyIssueReportViewModel> dailyissuelist = new List<DailyIssueReportViewModel>();
            foreach (var dailyissue in dailyList)
            {
                DailyIssueReportViewModel report = new DailyIssueReportViewModel();
                report.UserID = dailyissue.UserID;
                report.UserName = manipulate.GetUserName(dailyissue.UserID);
                report.Email = manipulate.GetUserEmail(dailyissue.UserID);
                report.BookID = dailyissue.BookID;
                report.BookName = manipulate.GetBookName(dailyissue.BookID);
                dailyissuelist.Add(report);
            }
            dailyReport.dailyIssueList = dailyissuelist;
            dailyReport.Today = DateTime.Today;
            return dailyReport;
        }
        #endregion
    }
}
