using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementDemo.Models
{
    public class MockRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee(){ID=1,Name="Kharak Singh Kular",Department=Dept.HR,Email="qwerty@1233445.com"},
                new Employee(){ID=2,Name="Paramjit Kaur", Department=Dept.IT, Email="Asdfghj@1234.com"},
                new Employee(){ID=3,Name="Kulwant Singh Kular", Department=Dept.Payroll, Email="KulwantSingh@gmial.com"},
                new Employee(){ID=4, Name="Nabhu", Department=Dept.IT, Email="iLoveyou@me.com"},
                new Employee(){ID=5, Name="Amanat", Department=Dept.HR, Email="herPresident@me.com"}
            };
        }

        public Employee Add(Employee emp)
        {
            emp.ID = _employees.Max(e => e.ID) + 1;
            _employees.Add(emp);
            return emp;
        }

        public Employee Delete(int id)
        {
            var tempEmp = _employees.Find(e => e.ID == id);
            if (tempEmp != null)
            {
                _employees.Remove(tempEmp);
            }
            return tempEmp;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public Employee Update(Employee employee)
        {
            var tempEmp = _employees.FirstOrDefault(e => e.ID == employee.ID);
            if (tempEmp != null)
            {
                tempEmp.Name = employee.Name;
                tempEmp.Email = employee.Email;
                tempEmp.Department = employee.Department;
            }
            return tempEmp;
        }
    }
}
