using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementDemo.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    ID = 1,
                    Name = "Kharak",
                    Department = Dept.IT,
                    Email = "Kharaksingh@gmail.com"
                },
                    new Employee()
                    {
                        ID = 2,
                        Name = "Kulwant",
                        Department = Dept.HR,
                        Email = "kul@gmail.com"
                    });
        }
    }
}
