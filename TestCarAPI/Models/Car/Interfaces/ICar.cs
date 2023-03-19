namespace TestCarAPI.Models.Car.Interfaces
{
    public interface ICar
    {
        int Id { get; set; }
        string ClientName { get; set; }
        int ProductionYear { get; set; }
        string Model { get; set; }
        string Manufacturer { get; set; }
        decimal Price { get; set; }
    }
}
