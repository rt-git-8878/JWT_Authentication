using JWT_Authentication.Context;
using JWT_Authentication.Interfaces;
using JWT_Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT_Authentication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtContext _jwtContext;
        public EmployeeService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }
        public List<Employee> GetEmployeeDetails()
        {
            var employees = _jwtContext.Employees.ToList();
            return employees;
        }

        public Employee GetEmployeeDetails(int id)
        {
            var emp = _jwtContext.Employees.SingleOrDefault(x => x.Id == id);
            return emp;
        }
        public Employee AddEmployee(Employee employee)
        {
            var emp=_jwtContext.Add(employee);
            _jwtContext.SaveChanges();
            return emp.Entity;
            
        }
        public Employee UpdateEmployee(Employee employee)
        {
            var updated = _jwtContext.Employees.Update(employee);
            _jwtContext.SaveChanges();
            return updated.Entity;
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                var emp = _jwtContext.Employees.SingleOrDefault(x => x.Id == id);
                if (emp != null)
                {
                    throw new Exception("User Not Found");
                }
                else
                {
                    _jwtContext.Remove(emp);
                    _jwtContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

       

      

            }
}
