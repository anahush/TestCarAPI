using TestCarAPI.Models.Car.Interfaces;
using TestCarAPI.Models.AutoData;

namespace TestCarAPI.Models.Car.DTO
{
    public class Car : ICar
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
