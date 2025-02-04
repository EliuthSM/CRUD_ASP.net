using CRUD_ASP.net.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRUD_ASP.net.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    }

}
