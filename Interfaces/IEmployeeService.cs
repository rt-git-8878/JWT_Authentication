﻿using JWT_Authentication.Models;

namespace JWT_Authentication.Interfaces
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeeDetails();
        public Employee GetEmployeeDetails(int id);
        public Employee AddEmployee(Employee employee);
        public Employee UpdateEmployee(Employee employee);
        public bool DeleteEmployee(int id);
    }
}
