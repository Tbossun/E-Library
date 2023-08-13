using E_Library.Data.Enums;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Data.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> Subcategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<BookRole> BookRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Author and Book
            modelBuilder.Entity<Author>()
                .HasMany(author => author.BooksBy)
                .WithMany(book => book.Authors);

            // Configure one-to-many relationship between Category and Subcategory
            modelBuilder.Entity<Category>()
                .HasMany(category => category.Subcategories)
                .WithOne(subcategory => subcategory.Category);

            // Configure many-to-many relationship between Subcategory and Book
            modelBuilder.Entity<SubCategory>()
                .HasMany(subcategory => subcategory.Books)
                .WithMany(book => book.Subcategories);

            // Configure one-to-many relationship between Book and Publisher
            modelBuilder.Entity<Book>()
                .HasOne(book => book.Publisher)
                .WithMany(publisher => publisher.PublishedBooks)
                .HasForeignKey(book => book.PublisherId);

            // Add unique constraint for ISBN
            modelBuilder.Entity<Book>()
                .HasIndex(book => book.ISBN)
                .IsUnique();

            // Configure many-to-many relationship between ApplicationUser and WishlistItem
            modelBuilder.Entity<WishlistItem>()
                .HasOne(wishlistItem => wishlistItem.User)
                .WithMany(user => user.WishlistItems)
                .HasForeignKey(wishlistItem => wishlistItem.UserId);

            // Configure many-to-many relationship between ApplicationUser and BookRole
            modelBuilder.Entity<BookRole>()
                .HasOne(bookRole => bookRole.User)
                .WithMany(user => user.BookRoles)
                .HasForeignKey(bookRole => bookRole.UserId);

            // Configure one-to-many relationship between ApplicationUser and Address
            modelBuilder.Entity<Address>()
                .HasOne(address => address.User)
                .WithMany(user => user.Addresses)
                .HasForeignKey(address => address.UserId);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            var categories = new List<Category>
    {
        new Category { Id = "1", CategoryName = "Fiction" },
        new Category { Id = "2", CategoryName = "Non-Fiction" },
        // Add more categories...
    };
            modelBuilder.Entity<Category>().HasData(categories);

            // Seed Publishers
            var publishers = new List<Publisher>
    {
        new Publisher { Id = "1", PublisherName = "Publisher A" },
        new Publisher { Id = "2", PublisherName = "Publisher B" },
        // Add more publishers...
    };
            modelBuilder.Entity<Publisher>().HasData(publishers);

            // Seed Authors
            var authors = new List<Author>
    {
        new Author { Id = "1", AuthorName = "Author X", About = "About Author X" },
        new Author { Id = "2", AuthorName = "Author Y", About = "About Author Y" },
        // Add more authors...
    };
            modelBuilder.Entity<Author>().HasData(authors);

            // Seed Books
            var books = new List<Book>
    {
        new Book { Id = "1", Title = "Book 1", Description = "Description of Book 1", PublisherId = "1", ISBN = "123456789",CategoryId = "1", YearPublished = new DateOnly(2022, 1, 1), Availability = BookAvailability.Available, Format = BookFormat.Hardcover, Language = BookLanguage.English, ImageURL = "https://example.com/book1.jpg"},
        new Book { Id = "2", Title = "Book 2", Description = "Description of Book 2", PublisherId = "2", ISBN = "987654321",CategoryId = "2", YearPublished = new DateOnly(2023, 1, 1), Availability = BookAvailability.Available, Format = BookFormat.Hardcover, Language = BookLanguage.French, ImageURL = "https://example.com/book2.jpg" },
        // Add more books...
    };
            modelBuilder.Entity<Book>().HasData(books);

            // Seed Users
            var users = new List<ApplicationUser>
    {
        new ApplicationUser { Id = "user1Id", FirstName = "John", LastName = "Doe", ImageURL = "https://example.com/user1.jpg", UserName = "User1" },
        new ApplicationUser { Id = "user2Id", FirstName = "Jane", LastName = "Smith", ImageURL = "https://example.com/user2.jpg", UserName= "User2" },
        // Add more users...
    };
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Seed Addresses
            var addresses = new List<Address>
    {
        new Address { Id = "address1Id", UserId = "user1Id", Street = "123 Main St", City = "City1", State = "State1", County = "County1" },
        new Address { Id = "address2Id", UserId = "user2Id", Street = "456 Elm St", City = "City2", State = "State2", County = "County2" },
        // Add more addresses...
    };
            modelBuilder.Entity<Address>().HasData(addresses);

            // Seed Reviews
            var reviews = new List<Reviews>
    {
        new Reviews { Id = "review1Id", ReviewText = "Great book!", BookId = "1", UserId = "user1Id", Rating = Rating.Excellent },
        new Reviews { Id = "review2Id", ReviewText = "Enjoyed reading it.", BookId = "2", UserId = "user2Id", Rating = Rating.Poor },
        // Add more reviews...
    };
            modelBuilder.Entity<Reviews>().HasData(reviews);

            // Seed WishlistItems
            var wishlistItems = new List<WishlistItem>
    {
        new WishlistItem { Id = "wishlistItem1Id", UserId = "user1Id", BookId = "1" },
        new WishlistItem { Id = "wishlistItem2Id", UserId = "user2Id", BookId = "2" },
        // Add more wishlist items...
    };
            modelBuilder.Entity<WishlistItem>().HasData(wishlistItems);

            // Seed BookRoles
            var bookRoles = new List<BookRole>
    {
        new BookRole { Id = "bookRole1Id", UserId = "user1Id", BookId = "1", Role = "Admin" },
        new BookRole { Id = "bookRole2Id", UserId = "user2Id", BookId = "2", Role = "User" },
        // Add more book roles...
    };
            modelBuilder.Entity<BookRole>().HasData(bookRoles);

            // Add more seed data for other entities...

            // NOTE: Adjust the properties and data according to your actual models.

        }
    }
}
