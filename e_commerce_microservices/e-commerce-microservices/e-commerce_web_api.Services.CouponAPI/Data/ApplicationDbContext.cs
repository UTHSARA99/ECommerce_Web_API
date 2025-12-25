using e_commerce_web_api.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web_api.Services.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
