using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementDemo.Models
{
    public class SqlRepository : IEmployeeRepository
    {
        private AppDbContext _db;
        private ILogger _logger;
        public SqlRepository(AppDbContext context, ILogger<SqlRepository> logger)
        {
            _db = context;
            _logger = logger;
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
            _logger.LogTrace("Trace log");
            _logger.LogDebug("Debug log");
            _logger.LogInformation("Information log");
            _logger.LogWarning("Warning log");
            _logger.LogError("Error log");
            _logger.LogCritical("Critical log");
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
