using FlightControl.DAL.Models;
using FlightControl.DTOs;
using FlightControl.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : FlightControllerBase
    {
        private readonly FlightService _flightService;
        private readonly AirportService _airportService;

        public FlightController(FlightService flightService, AirportService airportService, ILogger<FlightController> logger) : base(logger)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [Authorize]
        [HttpGet("ById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Flight? flight = _flightService.ReadOne(id);
                return flight is null ? NotFound() : Ok(flight);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Flight> flights = _flightService.ReadAll();

                if (flights == null || !flights.Any())
                {
                    return NotFound();
                }

                IEnumerable<FlightDTO> dtos = flights.Select(f => new FlightDTO(f));
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("FromTo/{from}/{to}")]
        public IActionResult GetAllFromTo(string from, string to)
        {
            try
            {
                Airport? arrivalAirport = _airportService.ReadOne(from);
                if (arrivalAirport is null)
                {
                    return BadRequestWError("Arrival airport does not exist!");
                }
                Airport? departureAirport = _airportService.ReadOne(to);
                if (departureAirport is null)
                {
                    return BadRequestWError("Departure airport does not exist!");
                }

                IEnumerable<Flight> flights = _flightService.GetAllFromTo(arrivalAirport.Id, departureAirport.Id);

                if (flights == null || !flights.Any())
                {
                    return NotFound();
                }

                IEnumerable<FlightDTO> dtos = flights.Select(f => new FlightDTO(f));
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("InTimeFrame/{lowerBound}/{higherBound}")]
        public IActionResult GetAllInTimeFrame(DateTime lowerBound, DateTime higherBound)
        {
            try
            {
                IEnumerable<Flight> flights = _flightService.GetDeparturesInTimeFrame(lowerBound, higherBound);

                if (flights == null || !flights.Any())
                {
                    return NotFound();
                }

                IEnumerable<FlightDTO> dtos = flights.Select(f => new FlightDTO(f));
                return Ok(dtos);
            }
            catch (InvalidOperationException e)
            {
                return BadRequestWError(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("FromToInTimeFrame/{from}/{to}/{lowerBound}/{higherBound}")]
        public IActionResult GetAllFromToInTimeFrame(string from, string to, DateTime lowerBound, DateTime higherBound)
        {
            try
            {
                Airport? arrivalAirport = _airportService.ReadOne(from);
                if (arrivalAirport is null)
                {
                    return BadRequestWError("Arrival airport does not exist!");
                }
                Airport? departureAirport = _airportService.ReadOne(to);
                if (departureAirport is null)
                {
                    return BadRequestWError("Departure airport does not exist!");
                }

                IEnumerable<Flight> flights = _flightService.GetAllFromToInTimeFrame(departureAirport.Id, arrivalAirport.Id, lowerBound, higherBound);
                
                if (flights == null || !flights.Any())
                {
                    return NotFound();
                }
                IEnumerable<FlightDTO> dtos = flights.Select(f => new FlightDTO(f));

                return Ok(dtos);
            }
            catch (InvalidOperationException e)
            {
                return BadRequestWError(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("MatchByFlightNumber/{flightNumber}")]
        public IActionResult MatchByFlightNumber(string flightNumber)
        {
            try
            {
                IEnumerable<Flight> flights = _flightService.MatchByFlightNumber(flightNumber);

                if (flights is null || !flights.Any())
                {
                    return NotFound();
                }
                IEnumerable<FlightDTO> dtos = flights.Select(f => new FlightDTO(f));

                return Ok(dtos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("MatchByPlaneType/{planeType}")]
        public IActionResult MatchByPlaneType(string planeType)
        {
            try
            {
                IEnumerable<Flight> flights = _flightService.MatchByPlaneType(planeType);

                if (flights is null || !flights.Any())
                {
                    return NotFound();
                }
                IEnumerable<FlightDTO> dtos = flights.Select(f => new FlightDTO(f));
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("ByFlightNumber/{flightNumber}")]
        public IActionResult Get(string flightNumber)
        {
            Flight? flight = _flightService.ReadOne(flightNumber);
            return flight is null ? NotFound() : Ok(flight);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] FlightModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Airport? arrivalAirport = _airportService.ReadOne(model.ArrivalAirportId);
                if (arrivalAirport is null)
                {
                    return BadRequestWError("Arrival airport does not exist!");
                }
                Airport? departureAirport = _airportService.ReadOne(model.DepartureAirportId);
                if (departureAirport is null)
                {
                    return BadRequestWError("Departure airport does not exist!");
                }

                Flight flight = _flightService.CreateOne(
                    model.FlightNumber,
                    model.DepartureDateTime,
                    departureAirport,
                    arrivalAirport,
                    model.PlaneType);

                return Ok(new FlightDTO(flight));
            }
            catch (InvalidOperationException e)
            {
                return BadRequestWError(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FlightModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Airport? arrivalAirport = _airportService.ReadOne(model.ArrivalAirportId);
                if (arrivalAirport is null)
                {
                    return BadRequestWError("Arrival airport does not exist!");
                }
                Airport? departureAirport = _airportService.ReadOne(model.DepartureAirportId);
                if (departureAirport is null)
                {
                    return BadRequestWError("Departure airport does not exist!");
                }

                Flight flight = new Flight()
                {
                    Id = id,
                    FlightNumber = model.FlightNumber,
                    ArrivalAirport = arrivalAirport,
                    DepartureAirport = departureAirport,
                    PlaneType = model.PlaneType
                };

                _flightService.Update(flight);

                return Ok(new FlightDTO(flight));
            }
            catch (InvalidOperationException e)
            {
                return BadRequestWError(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int affected = _flightService.DeleteOne(id);
                if (affected == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }

    public class FlightModel
    {
        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public string PlaneType { get; set; }
    }
}
