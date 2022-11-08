using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SenwesAssignment_API.Data;
using SenwesAssignment_API.Models.Employees;
using SenwesAssignment_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SenwesAssignment_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly LoadData _loadData;
        private IEnumerable<Employee> _employees;

        public EmployeeService()
        {
            _loadData = new LoadData();
            _employees = _loadData.LoadEmployeeData();
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            return await Task.FromResult(_employees);
        }

        public async Task<IEnumerable<Employee>> GetAsyncByAge()
        {
            var employees = _employees.Where(a => a.Age > 30);
            return await Task.FromResult(employees);
        }

        public async Task<IEnumerable<Salaries>> GetAsyncByFirstName()
        {
            var employeeSalries = _employees
                .Where(a => a.FirstName == "Treasure")
                .Select(s => new Salaries
                {
                    Salary = s.Salary,
                    EmployeeName = $"{s.FirstName} {s.LastName}"
                }).ToList();
            return await Task.FromResult(employeeSalries);
        }

        public async Task<Employee> GetAsyncById(int id)
        {
            var employee = _employees
                .Where(x => x.EmpID == id).FirstOrDefault();

            return await Task.FromResult(employee);
        }

        public async Task<IEnumerable<Employee>> GetAsyncByJoiningDate()
        {
            var employees = _employees
                .Where(d => d.YearOfJoining == DateTime.Today.Year - 5);

            return await Task.FromResult(employees);
        }

        public async Task<IEnumerable<Cities>> GetAsyncCities()
        {
            var cities = _employees
               .Select(c => new Cities
               {
                   Name = c.City
               }).ToList();
            return await Task.FromResult(cities);
        }

        public async Task<IEnumerable<Employee>> GetAsyncHighestPaid()
        {
            var highestPaid = _employees.OrderByDescending(a => a.Salary)
                .Take(10);
            return await Task.FromResult(highestPaid);
        }

        public async Task<Employee> SearchEmployee(string name, string surname, string city)
        {          
            var employee = _employees
                .FirstOrDefault(a => (a.FirstName == name || a.LastName == surname) 
                && a.City == city);
            return await Task.FromResult(employee);
        }
    }
}
