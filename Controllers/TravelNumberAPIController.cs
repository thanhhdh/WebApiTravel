using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using WebApiTravel.Models;
using WebApiTravel.Models.Dto;
using WebApiTravel.Repository;
using WebApiTravel.Repository.IRepository;

namespace WebApiTravel.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/TravelNumberAPI")]
    [ApiController]
    public class TravelNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly ITravelNumberRepository _travelNumberRepository;
        private readonly ITravelRepository _travelRepository;
        public TravelNumberAPIController(ITravelNumberRepository travelNumberRepository, IMapper mapper, ITravelRepository travelRepository)
        {
            _travelNumberRepository = travelNumberRepository;
            _travelRepository = travelRepository;
            _mapper = mapper;
            this._response = new APIResponse();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetTravelNumbers()
        {
            try
            {
                IEnumerable<TravelNumber> travelNumberList = await _travelNumberRepository.GetAllAsync();
                _response.Result = (_mapper.Map<List<TravelNumber>>(travelNumberList));
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

        [HttpGet("{id:int}", Name = "GetTravelNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> GetTravelNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var travelNumber = await _travelNumberRepository.GetAsync(u => u.TravelNo == id);
                if (travelNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<TravelNumberDTO>(travelNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateTravelNumber(TravelNumberCreateDTO travelNumberCreate)
        {
            try
            {
                if (await _travelNumberRepository.GetAsync(x => x.TravelNo == travelNumberCreate.TravelNo) != null)
                {
                    ModelState.AddModelError("CustomError", "Travel Number already Exists");
                    return BadRequest(ModelState);
                }
                if (await _travelRepository.GetAsync(x => x.Id == travelNumberCreate.TravelId) == null)
                {
                    ModelState.AddModelError("CustomError", "Travel ID is Invalid");
                    return BadRequest(ModelState);
                }
                if (travelNumberCreate == null)
                {
                    return BadRequest(travelNumberCreate);
                }
                TravelNumber travelNumber = _mapper.Map<TravelNumber>(travelNumberCreate);

                await _travelNumberRepository.CreateAsync(travelNumber);
                _response.Result = _mapper.Map<TravelNumberDTO>(travelNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetTravelNumber", new { id = travelNumber.TravelNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}"  , Name = "DeleteTravelNumber")]

        public async Task<ActionResult<APIResponse>> DeleteTravelNumber (int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                var travelNumber = await _travelNumberRepository.GetAsync(x => x.TravelNo == id);
                if (travelNumber == null)
                {
                    return NotFound();
                }
                await _travelNumberRepository.RemoveAsync(travelNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
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

        [HttpPut("{id:int}", Name = "UpdateTravelNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateTravelNumber(int id, [FromBody] TravelNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.TravelNo)
                {
                    return BadRequest();
                }
                if (await _travelRepository.GetAsync(x => x.Id == updateDTO.TravelId) == null)
                {
                    ModelState.AddModelError("CustomError", "Travel ID is Invalid");
                    return BadRequest(ModelState);
                }
                TravelNumber model = _mapper.Map<TravelNumber>(updateDTO);
                await _travelNumberRepository.UpdateAsync(model);
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

    }
}
