using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using WebApiTravel.Models;
using WebApiTravel.Models.Dto;
using WebApiTravel.Repository;
using WebApiTravel.Repository.IRepository;

namespace WebApiTravel.Controllers.v2
{
    [Route("api/v{version:apiVersion}/TravelNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
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
      

        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
