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
            optionsBuilder.UseMySQL("datasource=127.0.0.1;port=3307;username=root;password=;database=musicspot");
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

