using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;
using System.Threading.Tasks;

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
        async public static Task MakeBadges(List<Employee> employees)
        {
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;

            int PHOTO_LEFT_X = 184;
            int PHOTO_TOP_Y = 215;
            int PHOTO_RIGHT_X = 486;
            int PHOTO_BOTTOM_Y = 517;

            int COMPANY_NAME_Y = 150;
            int EMPLOYEE_NAME_Y = 600;

            int EMPLOYEE_ID_Y = 730;

            // Here, we use HttpClient to import/download employee info from the employee list.
            // Can also be used to send HTTP requests, read files, download webpages, upload data from a resource, etc. 
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    // Retrieve each employee image and convert it into a SKImage.
                    // We use the HttpClient.GetStreamAsync() method to send a GET request to the specified URI and return 
                    // the response as a Stream object. 
                    SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].getPhotoUrl()));
                    SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));

                    // use SKBitmap to create a canvas to place images and text. 
                    SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);

                    // use SKCanvas to convert the Bitmap to a canvas.
                    SKCanvas canvas = new SKCanvas(badge);

                    // Draw the background on the badge
                    canvas.DrawImage(
                        background,
                        new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT)
                    );

                    // Draw the photo onto the badge
                    canvas.DrawImage(
                        photo,
                        new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y)
                    );

                    SKPaint paint = new SKPaint();
                    paint.TextSize = 42.0f;
                    paint.IsAntialias = true;
                    paint.Color = SKColors.White;
                    paint.IsStroke = false;
                    paint.TextAlign = SKTextAlign.Center;
                    paint.Typeface = SKTypeface.FromFamilyName("Arial");

                    canvas.DrawText(
                        employees[i].getCompanyName(),
                        BADGE_WIDTH / 2f,
                        COMPANY_NAME_Y,
                        paint
                    );

                    // Employee name
                    paint.Color = SKColors.Black;
                    canvas.DrawText(
                        employees[i].getFullName(),
                        BADGE_WIDTH / 2f,
                        EMPLOYEE_NAME_Y,
                        paint
                    );

                    // Employee ID
                    paint.Typeface = SKTypeface.FromFamilyName("Courier New");
                    canvas.DrawText(
                        employees[i].getId().ToString(),
                        BADGE_WIDTH / 2f,
                        EMPLOYEE_ID_Y,
                        paint
                    );

                    SKImage finalImage = SKImage.FromBitmap(badge);
                    SKData data = finalImage.Encode();

                    string template = "data/{0}_badge.png";

                    data.SaveTo(File.OpenWrite(string.Format(template, employees[i].getId())));
                }
            }
        }
    }
}