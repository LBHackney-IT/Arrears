using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArrearsApi.V1.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v1/arrears")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class ArrearsApiController : BaseController
    {
        private readonly IGetAllUseCase _getAllUseCase;
        private readonly IGetByIdUseCase _getByIdUseCase;
        private readonly IAddUseCase _addUseCase;
        public ArrearsApiController(IGetAllUseCase getAllUseCase, IGetByIdUseCase getByIdUseCase, IAddUseCase addUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _addUseCase = addUseCase;
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">No ? found for the specified ID</response>
        [ProducesResponseType(typeof(ArrearsResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        //TODO: rename to match the identifier that will be used
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false));
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(ArrearsResponseObjectList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("asset/{targettype}")]
        public async Task< IActionResult> GetAllAsync(string targettype, int count)
        {
            return Ok(await _getAllUseCase.ExecuteAsync(targettype, count).ConfigureAwait(false));
        }

        [ProducesResponseType(typeof(ArrearsResponseObjectList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("tenure")]
        public async Task<IActionResult> GetAllTenureAsync(int count)
        {
            string targettype = Constants.TenureTargetType;
            return Ok(await _getAllUseCase.ExecuteAsync(targettype, count).ConfigureAwait(false));
        }
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post(Arrears arrears)
        {
            //var _arrears = await _getByIdUseCase.ExecuteAsync(arrears.Id).ConfigureAwait(false);
            //if (_arrears != null)
            //    return Conflict("This record is exists");
            arrears.Id = Guid.NewGuid();
            await _addUseCase.ExecuteAsync(arrears).ConfigureAwait(false);
            return RedirectToAction("Get", new { id = arrears.Id });
        }

    }
}
