using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaData.Models;

namespace SpaData.Context
{
    public class SpaContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public SpaContext(DbContextOptions<SpaContext> options)
        : base(options)
        { }

        public DbSet<Person> Person{ get; set; }
    }
}
