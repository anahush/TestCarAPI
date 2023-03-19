using Dapper;

using TestCarAPI.Context;
using TestCarAPI.Models.Car.DTO;
using TestCarAPI.Repositories.Interfaces;

namespace TestCarAPI.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DapperContext _context;

        public CarRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            var query = @"SELECT * FROM Cars";

            using (var connection = _context.CreateConnection())
            {
                var cars = await connection.QueryAsync<Car>(query);
                return cars.ToList();
            }
        }

        public async Task<IEnumerable<Car>> GetCarsByIdsAsync(int startId, int endId)
        {
            var query = $@"SELECT * FROM Cars WHERE Id >= {startId} AND Id <= {endId}";

            using (var connection = _context.CreateConnection())
            {
                var cars = await connection.QueryAsync<Car>(query);
                return cars.ToList();
            }
        }

        public async Task<Car> GetCarByIdAsync(int carId)
        {
            var query = $@"SELECT * FROM Cars WHERE Id = {carId}";

            using (var connection = _context.CreateConnection())
            {
                var car = await connection.QueryAsync<Car>(query);
                return car != null ? car.FirstOrDefault() : null;
            }
        }

        public async Task AddNewCarAsync(Car car)
        {
            var query = $@"INSERT INTO Cars (ClientName, ProductionYear, Model, Manufacturer, Price) VALUES 
                            ('{car.ClientName}', {car.ProductionYear}, '{car.Model}', '{car.Manufacturer}', {car.Price})";

            using (var connection = _context.CreateConnection())
            {
                await connection.QueryAsync(query);
            }
        }

        public async Task UpdateCarAsync(Car car)
        {
            var query = $@"UPDATE Cars SET ClientName = '{car.ClientName}', ProductionYear = {car.ProductionYear},
                            Model = '{car.Model}', Manufacturer = '{car.Manufacturer}', Price = {car.Price}
                            WHERE Id = {car.Id}";

            using (var connection = _context.CreateConnection())
            {
                await connection.QueryAsync(query);
            }
        }

        public async Task DeleteCarAsync(int carId)
        {
            var query = $@"DELETE FROM Cars WHERE Id = {carId}";

            using (var connection = _context.CreateConnection())
            {
                await connection.QueryAsync(query);
            }
        }
    }
}