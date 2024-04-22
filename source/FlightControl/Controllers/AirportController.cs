using FlightControl.DAL.Models;
using FlightControl.DTOs;
using FlightControl.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AirportController : FlightControllerBase
    {
        private readonly AirportService _airportService;

        public AirportController(AirportService airportService, ILogger<AirportController> logger) : base(logger)
        {
            _airportService = airportService;
        }

        [Authorize]
        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Airport? airport = _airportService.ReadOneWithFlights(id);
                return airport is null ? NotFound() : Ok(new AirportDTOFlights(airport));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("getByName/{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                Airport? airport = _airportService.ReadOneWithFlights(name);
                return airport is null ? NotFound() : Ok(new AirportDTOFlights(airport));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Airport> airports = _airportService.ReadAll();
                if (airports is null || !airports.Any())
                {
                    return NotFound();
                }
                IEnumerable<AirportDTO> dtos = airports.Select(a => new AirportDTO(a));
                return Ok(dtos);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Authorize]
        [HttpGet("matchByName/{name}")]
        public IActionResult MatchByName(string name)
        {
            try
            {
                IEnumerable<Airport> airports = _airportService.MatchByName(name);
                if (airports is null || !airports.Any())
                {
                    return NotFound();
                }
                IEnumerable<AirportDTO> dtos = airports.Select(a => new AirportDTO(a));
                return Ok(dtos);
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
                int affected = _airportService.DeleteOne(id);
                if (affected == 0)
                {
                    return NotFound();
                }
                return Ok();
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
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] AirportModel model) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var airport = _airportService.ChangeName(model.Name, id);
                return Ok(new AirportDTO(airport));
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
        [HttpPost]
        public IActionResult Create([FromBody] AirportModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Airport airport = _airportService.CreateOne(model.Name);
                return Ok(new AirportDTO(airport));
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
    }

    public class AirportModel
    {
        public string Name { get; set; }
    }
}
