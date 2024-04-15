using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LG_10_Exercise_01.Models
{
    public class GiftDbContext:DbContext
    {
        public GiftDbContext()
            : base("GiftDbContext")
        {
        }

        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftCategory> GiftCategories { get; set; }
   
    }
}