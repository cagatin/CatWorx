using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;

namespace CatWorx.BadgeMaker
{
    class Util
    {
        // Prints all Employee data within the passed in List.
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
        // Returns a List of Employees based on user Input
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
                int id = Int32.Parse(Console.ReadLine() ?? "");

                Console.WriteLine("Please enter a photo URL:");
                string url = Console.ReadLine() ?? "";


                Employee currentEmployee = new Employee(firstName, lastName, id, url);
                employees.Add(currentEmployee);
            }
            return employees;
        }

        // Creates a CSV File
        public static void MakeCSV(List<Employee> employees)
        {
            // Check to see if a "data" folder exists
            if (!Directory.Exists("data"))
            {
                // If the directory does not exist, create it.
                Directory.CreateDirectory("data");
            }

            // Use StreamWriter class to create a employees csv file
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                // Generate the column titles for the CSV file.
                file.WriteLine("ID,Name,PhotoURL");

                // Loop over employees list and populate data fields
                for (int i = 0; i < employees.Count; i++)
                {
                    //Write each employee to the file
                    string template = "{0}, {1}, {2}";
                    file.WriteLine(String.Format(
                        template,
                        employees[i].getId(),
                        employees[i].getFullName(),
                        employees[i].getPhotoUrl()
                        )
                    );
                }
            };
        }

        // Create Badges 
        public static void MakeBadges(List<Employee> employees)
        {
            //import the badge template image file that will work as the background
            SKImage newImage = SKImage.FromEncodedData(File.OpenRead("badge.png"));

            //use the Encode() method to encode the image in a png format.
            SKData data = newImage.Encode();

            // Save the SKImage encoded data into a file employeeBadge
            data.SaveTo(File.OpenWrite("data/employeeBadge.png"));

            //customize each employee badge by adding specific info to each employee
            //(name, picture, id number)

            //add the new image file to the data folder.
        }
    }
}