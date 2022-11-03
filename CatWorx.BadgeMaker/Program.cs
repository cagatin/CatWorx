using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        // this will return a list of employees
        static List<string> GetEmployees()
        {
            // Create and list to store employee names
            List<string> employees = new List<string>();

            while (true)
            {
                Console.WriteLine("Please enter a name: (Leave empty to exit): ");
                string input = Console.ReadLine() ?? "";
                if (input == "")
                {
                    break;
                }
                employees.Add(input);
            }
            return employees;
        }
        static void PrintEmployees(List<string> employees)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine(employees[i]);
            }
        }
        static void Main(string[] args)
        {
            // Create and list to store employee names
            List<string> employees = GetEmployees();
            PrintEmployees(employees);
        }
    }
}

