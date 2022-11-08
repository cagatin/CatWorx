namespace CatWorx.BadgeMaker
{
    class Employee
    {
        public string FirstName;
        public string LastName;
        public int Id;
        public string PhotoURL;

        public Employee(string first, string last, int id, string url)
        {
            FirstName = first;
            LastName = last;
            Id = id;
            PhotoURL = url;
        }

        public string getFullName()
        {
            return FirstName + " " + LastName;
        }

        public int getId()
        {
            return Id;
        }

        public string getPhotoUrl()
        {
            return PhotoURL;
        }

        public string getCompanyName()
        {
            return "Cat Worx";
        }
    }
}