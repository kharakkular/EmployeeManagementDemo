using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementDemo.Models
{
    public class SqlRepository : IEmployeeRepository
    {
        private AppDbContext _db;
        public SqlRepository(AppDbContext context)
        {
            _db = context;
        }
        public Employee Add(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            var tempEmployee = _db.Employees.Find(id);
            if (tempEmployee != null)
            {
                _db.Employees.Remove(tempEmployee);
                _db.SaveChanges();
            }
            return tempEmployee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _db.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _db.Employees.Find(id);
        }

        public Employee Update(Employee employee)
        {
            var tempEmployee = _db.Employees.Attach(employee);
            tempEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return employee;
        }
    }
}
