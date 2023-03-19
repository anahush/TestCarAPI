using TestCarAPI.Models.Car.DTO;

namespace TestCarAPI.Repositories.Interfaces
{
    public interface ICarRepository
    {
        public Task<IEnumerable<Car>> GetAllCarsAsync();
        public Task<IEnumerable<Car>> GetCarsByIdsAsync(int startId, int endId);
        public Task<Car> GetCarByIdAsync(int carId);
        public Task AddNewCarAsync(Car car);
        public Task UpdateCarAsync(Car car);
        public Task DeleteCarAsync(int carId);
    }
}
