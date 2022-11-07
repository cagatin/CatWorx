using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        // this will return a list of employees
        static List<Employee> GetEmployees()
        {
            // Create and list to store employee names
            List<Employee> employees = new List<Employee>();

            while (true)
            {
                Console.WriteLine("Please enter a name: (Leave empty to exit): ");
                string input = Console.ReadLine() ?? "";
                if (input == "")
                {
                    break;
                }
                Employee currentEmployee = new Employee(input, "Smith");
                employees.Add(currentEmployee);
            }
            return employees;
        }
        static void PrintEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine(employees[i].getFullName());
            }
        }
        static void Main(string[] args)
        {
            // Create and list to store employee names
            List<Employee> employees = GetEmployees();
            PrintEmployees(employees);
        }
    }
}

