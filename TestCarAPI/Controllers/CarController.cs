using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TestCarAPI.Models.Car.DTO;
using TestCarAPI.Models.Helper;
using TestCarAPI.Repositories.Interfaces;

namespace TestCarAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion(@"1.0")]
    [Produces("application/json")]
    public class CarController : ControllerBase
    {
        private ICarRepository _carRepository { get; set; }

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet(Name = nameof(GetAllCarsAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllCarsAsync()
        {
            try
            {
                var cars = await _carRepository.GetAllCarsAsync();
                return cars != null ? Ok(cars) : StatusCode(StatusCodes.Status404NotFound, "Cars not found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{slice}",Name = nameof(GetCarsByIdsAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCarsByIdsAsync(CollectionSlice slice)
        {
            try
            {
                var cars = await _carRepository.GetCarsByIdsAsync(slice.StartId, slice.EndId);
                return cars != null ? Ok(cars) : StatusCode(StatusCodes.Status404NotFound, "Cars not found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{carId}/info", Name = nameof(GetCarByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCarByIdAsync(int carId)
        {
            try
            {
                var cars = await _carRepository.GetCarByIdAsync(carId);
                return cars != null ? Ok(cars) : StatusCode(StatusCodes.Status404NotFound, "Car not found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost(Name = nameof(AddNewCarAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddNewCarAsync(Car car)
        {
            try
            {
                await _carRepository.AddNewCarAsync(car);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{carId}", Name = nameof(DeleteCarByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCarByIdAsync(int carId)
        {
            var car = _carRepository.GetCarByIdAsync(carId);
            try
            {
                if (car != null)
                {
                    await _carRepository.DeleteCarAsync(carId);
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Car with id {carId} was not found.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Deleting car with id {carId} finished unsuccessfully");
            }
        }

        [HttpPut("{carId}", Name = nameof(UpdateCarByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCarByIdAsync(Car car)
        {
            var carToUpdate = _carRepository.GetCarByIdAsync(car.Id);
            try
            {
                if (carToUpdate != null)
                {
                    await _carRepository.UpdateCarAsync(car);
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Car with id {car.Id} was not found.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Updating car with id {car.Id} car.Id unsuccessfully");
            }
        }
    }
}
