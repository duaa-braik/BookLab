using BookLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLab.Infrastructure
{
    public class BookLabDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<AuthorBook> AuthorBook { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<CartItem> CartItem { get; set; }

        public DbSet<Review> Review { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Discount> Discount { get; set; }

        public DbSet<BookCategory> BookCategory { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public BookLabDbContext() { }

        public BookLabDbContext(DbContextOptions<BookLabDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            configureAuthorTable(modelBuilder);

            configureBookTable(modelBuilder);

            configureUserTable(modelBuilder);

            configureReviewTable(modelBuilder);

            configureOrderItemTable(modelBuilder);

            configureCartItemTable(modelBuilder);

            configureDiscountTable(modelBuilder);

            configureCatgeoryTable(modelBuilder);

            configureRoleTable(modelBuilder);

            configureCartStatus(modelBuilder);

            configureOrderStatusTable(modelBuilder);

            configureOrderTable(modelBuilder);

            configurePublisherTable(modelBuilder);

            configureAdminTable(modelBuilder);

            configureCustomerTable(modelBuilder);
        }

        private static void configureCustomerTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName)
                .HasMaxLength(20);

            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName)
                .HasMaxLength(20);

            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void configureAdminTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(a => a.FirstName)
                .HasMaxLength(20);

            modelBuilder.Entity<Admin>()
                .Property(a => a.LastName)
                .HasMaxLength(20);

            modelBuilder.Entity<Admin>()
                .Property(a => a.PhoneNumber)
                .HasMaxLength(50);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void configurePublisherTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>()
                .Property(p => p.FoundedAt)
                .HasColumnType("date");

            modelBuilder.Entity<Publisher>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Publisher>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void configureOrderTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Total)
                .HasColumnType("decimal(18, 4)");
        }

        private static void configureOrderStatusTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>()
                .Property(c => c.Name)
                .HasMaxLength(20);

            modelBuilder.Entity<OrderStatus>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderStatus>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void configureCartStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartStatus>()
                .Property(c => c.Name)
                .HasMaxLength(20);

            modelBuilder.Entity<CartStatus>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartStatus>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void configureRoleTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .HasMaxLength(20);

            modelBuilder.Entity<Role>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = 1, Name = "Customer", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = null, UpdatedBy = null },
                    new Role { Id = 2, Name = "Admin", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = null, UpdatedBy = null }
                );
        }

        private static void configureCatgeoryTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Category>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void configureDiscountTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discount>(discount =>
                discount.ToTable(d => d.HasCheckConstraint("CK_Discount_DiscountPercentage", "[DiscountPercentage] <= 100")));

            modelBuilder.Entity<Discount>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Discount>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void configureCartItemTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(cartItem =>
                cartItem.ToTable(ci => ci.HasCheckConstraint("CK_CartItem_Quantity", "[Quantity] <= 12")));
        }

        private static void configureOrderItemTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>(orderItem =>
                orderItem.ToTable(oi => oi.HasCheckConstraint("CK_OrderItem_Quantity", "[Quantity] <= 12")));
        }

        private static void configureReviewTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(review =>
                review.ToTable(r => r.HasCheckConstraint("CK_Review_Rating", "[Rating] <= 5")));
        }

        private static void configureUserTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(20);
        }

        private static void configureBookTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .HasMaxLength(1000);

            modelBuilder.Entity<Book>()
                .Property(e => e.ISBN)
                .HasColumnType("varchar(13)");

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN);

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Title);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void configureAuthorTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .HasMaxLength(20);

            modelBuilder.Entity<Author>()
                .Property(a => a.LastName)
                .HasMaxLength(20);

            modelBuilder.Entity<Author>()
                .Property(a => a.Email)
                .HasMaxLength(30);

            modelBuilder.Entity<Author>()
                .Property(a => a.Nationality)
                .HasMaxLength(30);

            modelBuilder.Entity<Author>()
                .HasIndex(a => a.FirstName)
                .IncludeProperties(a => a.LastName);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.AdminCreated)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.AdminUpdated)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
