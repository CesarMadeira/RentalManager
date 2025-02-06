namespace RentalManager.Domain.Entities
{
    public class Motorcycle
    {
        public Motorcycle(string identifier, string licencePlate, string model, int year)
        {
            //if (string.IsNullOrWhiteSpace(identifier))
            Identifier = identifier;
            LicencePlate = licencePlate;
            Model = model;
            Year = year;
        }

        public string Identifier { get; set; }
        public string LicencePlate { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
