using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiTravel.Data;
using WebApiTravel.Models;
using WebApiTravel.Models.Dto;
using WebApiTravel.Repository.IRepository;

namespace WebApiTravel.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/TravelAPI")]
    [ApiController]
    public class TravelAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ITravelRepository _travelRepository;
        private readonly IMapper _mapper;
        public TravelAPIController(ITravelRepository travelRepository, IMapper mapper) 
        { 
            _travelRepository = travelRepository; 
            _mapper = mapper;
            this._response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetTravels()
        {
            try
            {
                IEnumerable<Travel> travelList = await _travelRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<TravelDTO>>(travelList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetTravel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTravel(int id) 
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var travel = await _travelRepository.GetAsync(x => x.Id == id);
                if(travel == null) 
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<TravelDTO>(travel);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTravel([FromBody]TravelCreateDTO createDTO) {
            try
            {

                //if(!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if(await _travelRepository.GetAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Travel already Exists");
                    return BadRequest(ModelState);
                }
                if(createDTO == null) { 
                    return BadRequest(createDTO);
                }
                //if (travelDTO.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                Travel model = _mapper.Map<Travel>(createDTO);
                //Travel model = new Travel()
                //{
                //    Name = createDTO.Name,
                //    Details = createDTO.Details,
                //    Amenity = createDTO.Amenity,
                //    Occupancy = createDTO.Occupancy,
                //    ImageUrl = createDTO.ImageUrl,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft,
                //};
                await _travelRepository.CreateAsync(model);
                _response.Result = _mapper.Map<TravelDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetTravel",new { id = model.Id}, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name ="DeleteTravel")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteTravel(int id)
        {
            try
            {

            if(id == 0)
            {
                return BadRequest();
            }
            var travel = await _travelRepository.GetAsync( x =>x.Id == id);
            if(travel == null)
            {
                return NotFound();
            }
                await _travelRepository.RemoveAsync(travel);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpPut("{id:int}", Name = "UpdateTravel")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateTravel(int id, [FromBody]TravelUpdateDTO updateDTO)
        {
            try
            {
                if(updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                //var travel = TravelStore.travelList.FirstOrDefault(x => x.Id == id);
                //travel.Name = travelDTO.Name;
                //travel.Occupany = travelDTO.Occupany;
                //travel.Sqft = travelDTO.Sqft;

                Travel model = _mapper.Map<Travel>(updateDTO);
                await _travelRepository.UpdateAsync(model); 
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialTravel")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialTravel(int id, JsonPatchDocument<TravelUpdateDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var travel = await _travelRepository.GetAsync(x => x.Id == id, tracked:false);

            TravelUpdateDTO travelDTO = _mapper.Map<TravelUpdateDTO>(travel);
            
            if (travel == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(travelDTO, ModelState);
            Travel model = _mapper.Map<Travel>(travelDTO);
           
            await _travelRepository.UpdateAsync(model);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            return NoContent();
        }

    }
}
