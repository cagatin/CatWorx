namespace CatWorx.BadgeMaker 
{
    class Employee 
    {
        public string FirstName;
        public string LastName;
        public int Id;
        public string PhotoURL;

        public Employee(string first, string last) {
            FirstName = first;
            LastName = last;
        }

        public string getFullName() {
            return FirstName + " " + LastName;
        }
    }
}