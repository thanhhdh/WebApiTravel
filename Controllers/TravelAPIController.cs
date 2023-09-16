using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiTravel.Data;
using WebApiTravel.Logging;
using WebApiTravel.Models;
using WebApiTravel.Models.Dto;

namespace WebApiTravel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        public TravelAPIController(ILogging logger) 
        { 
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TravelDTO>> GetTravels()
        {
            _logger.Log("Get Travel all", "");
            return Ok(TravelStore.travelList);
        }

        [HttpGet("{id:int}", Name = "GetTravel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TravelDTO> GetTravel(int id) 
        {
            if(id == 0)
            {
                _logger.Log("Get Travel Error with id" + id,"error");
                return BadRequest();
            }
            var travel = TravelStore.travelList.FirstOrDefault(x => x.Id == id);
            if(travel == null) 
            {
                return NotFound();
            }
            return Ok(travel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TravelDTO> CreateTravel([FromBody]TravelDTO travelDTO) {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(TravelStore.travelList.FirstOrDefault(x => x.Name.ToLower() == travelDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Travel already Exists");
                return BadRequest(ModelState);
            }
            if(travelDTO == null) { 
                return BadRequest(travelDTO);
            }
            if (travelDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            travelDTO.Id = TravelStore.travelList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            TravelStore.travelList.Add(travelDTO);
            return CreatedAtRoute("GetTravel",new { id = travelDTO.Id}, travelDTO);
        }

        [HttpDelete("{id:int}", Name ="DeleteTravel")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTravel(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var travel = TravelStore.travelList.FirstOrDefault( x =>x.Id == id);
            if(travel == null)
            {
                return NotFound();
            }
            TravelStore.travelList.Remove(travel);
            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateTravel")]
        public IActionResult UpdateTravel(int id, [FromBody]TravelDTO travelDTO)
        {
            if(travelDTO == null || id != travelDTO.Id)
            {
                return BadRequest();
            }
            var travel = TravelStore.travelList.FirstOrDefault(x => x.Id == id);
            travel.Name = travelDTO.Name;
            travel.Occupany = travelDTO.Occupany;
            travel.Sqft = travelDTO.Sqft;
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialTravel")]
        public IActionResult UpdatePartialTravel(int id, JsonPatchDocument<TravelDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var travel = TravelStore.travelList.FirstOrDefault(x => x.Id == id);
            if(travel == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(travel, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            return NoContent();
        }

    }
}
