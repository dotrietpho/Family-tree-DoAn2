using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CayGiaPhaAPI.Class
{
    public class Context : DbContext
    {
        public Context() : base()
        {
            string databasename = "CayGiaPhaAPI";
            this.Database.Connection.ConnectionString = @"Data Source=PHO-PC\MAYCUAPHO;Initial Catalog=" + databasename + ";Trusted_Connection=Yes";
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonRelationship> PersonRelationship { get; set; }
        public DbSet<Node> Node { get; set; }
    }
}