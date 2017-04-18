    using System;
    using System.Data.Entity;
    using System.Linq;

//Database Class
public class DBContext : DbContext
{    
    public DBContext()
        : base("name=DBContext")
    {
    }
    
    public virtual DbSet<Persons> Persons { get; set; }
}
