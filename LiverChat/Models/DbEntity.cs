using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LiverChat.Models
{
    public class DbEntity : DbContext
    {
        public DbEntity() : base("DbEntity")
        {

        }
        public DbSet<liver> liver { get; set; }
    }
}