using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicSpot
{
    public class Data: DbContext
    {
        public DbSet<release> release { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=musicspot;User=root;Password=D1t1s33nP4sw00rd;");
        }
        public void testconnection()
        {
            using (var context = new Data())
            {
                if (context.Database.CanConnect())
                {
                    MessageBox.Show("Succes");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }
        public List<release> GetAllReleases()
        {
            return release.ToList();
        }
    }
}

