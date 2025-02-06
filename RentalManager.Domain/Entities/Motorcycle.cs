namespace RentalManager.Domain.Entities
{
    public class Motorcycle
    {
        public Motorcycle(string id, string licencePlate, string model, int year)
        {
            Id = id;
            LicencePlate = licencePlate;
            Model = model;
            Year = year;
        }

        public string Id { get; private set; }
        public string LicencePlate { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }

        public void SetLicencePlate(string licencePlate)
        {
            LicencePlate = licencePlate;
        }
    }
}
