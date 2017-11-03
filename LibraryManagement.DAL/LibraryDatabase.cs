namespace LibraryManagement.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class LibraryDatabase : DbContext
    {
        // Your context has been configured to use a 'LibraryDatabase' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LibraryManagement.DAL.LibraryDatabase' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LibraryDatabase' 
        // connection string in the application configuration file.
        public LibraryDatabase()
            : base("name=LibraryDatabase")
        {
        }
        public DbSet<BookCategories> Categories { get; set; }
        public DbSet<Books> Book { get; set; }
        public DbSet<BookDetails> BookDetail { get; set; }
        public DbSet<ShelfDetails> Shelf { get; set; }
        public DbSet<DailyBookIssues> BookIssue { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<Employees> Employee { get; set; }
        public DbSet<ShelfCategory> shelfcategory { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}