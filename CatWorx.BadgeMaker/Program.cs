using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Util 
    {
        public static void PrintEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                string template = "{0, -10}\t{1, -20}\t{2}";
                Console.WriteLine(String.Format(
                    template, 
                    employees[i].getId(),
                    employees[i].getFullName(), 
                    employees[i].getPhotoUrl()
                    )
                );
            }
        }
        // this will return a list of employees
        public static List<Employee> GetEmployees()
        {
            // Create and list to store employee names
            List<Employee> employees = new List<Employee>();

            while (true)
            {
                Console.WriteLine("Please enter a first name: (Leave empty to exit): ");
                string firstName = Console.ReadLine() ?? "";
                if (firstName == "")
                {
                    break;
                }

                Console.WriteLine("Please enter a last name:");
                string lastName = Console.ReadLine() ?? "";

                Console.WriteLine("Please enter an ID number:");
                int id = Int32.Parse(Console.ReadLine()?? "");

                Console.WriteLine("Please enter a photo URL:");
                string url = Console.ReadLine() ?? "";


                Employee currentEmployee = new Employee(firstName, lastName, id, url);
                employees.Add(currentEmployee);
            }
            return employees;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Create and list to store employee names
            List<Employee> employees = Util.GetEmployees();
            Util.PrintEmployees(employees);
        }
    }
}

