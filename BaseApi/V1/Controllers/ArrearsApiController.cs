using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ArrearsApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/arrears")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
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
        /// Get Arrears based on Arrears Id
        /// </summary>
        /// <param name="id">Arrears Id</param>
        /// <returns>
        ///  <response code="200">ok result</response>
        ///  <response code="404">No arrears found for the specified ID</response>
        ///  <response code="400">Bad request result</response>
        ///  <response code="500">Internal Server Error</response>
        /// </returns>
        [ProducesResponseType(typeof(ArrearsResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id}",Name ="Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            return HandleResult( await _getByIdUseCase.ExecuteAsync(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Get number of High Arrears based on count and based on current balance descending
        /// </summary>
        /// <param name="targettype">target type as tenure/estate/block</param>
        /// <param name="count">number of result count</param>
        /// <returns>
        ///  <response code="200">ok result</response>
        ///  <response code="404">No arrears found for the specified ID</response>
        ///  <response code="400">Bad request result</response>
        ///  <response code="500">Internal Server Error</response>
        /// </returns>
        [ProducesResponseType(typeof(ArrearsResponseObjectList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("asset/{targettype}")]
        public async Task<IActionResult> GetAllAsync(string targettype, int count)
        {
            return HandleResult( await _getAllUseCase.ExecuteAsync(targettype, count).ConfigureAwait(false));
        }

        /// <summary>
        /// Get number of Arrear record based on count and based on current balance descending
        /// </summary>
        /// <param name="count">number of result count</param>
        /// <returns>
        ///  <response code="200">ok result</response>
        ///  <response code="404">No arrears found for the specified ID</response>
        ///  <response code="400">Bad request result</response>
        ///  <response code="500">Internal Server Error</response>
        /// </returns>
        [ProducesResponseType(typeof(ArrearsResponseObjectList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("tenure")]
        public async Task<IActionResult> GetAllTenureAsync(int count)
        {
            return HandleResult(await _getAllUseCase.ExecuteAsync(Constants.TenureTargetType, count).ConfigureAwait(false));
        }

        /// <summary>
        /// Create a new Arrears record
        /// </summary>
        /// <param name="arrears">Arrears request object</param>
        /// <returns>
        ///  <response code="201">Created at route result</response>
        ///  <response code="400">Bad request result</response>
        ///  <response code="500">Internal Server Error</response>
        /// </returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post(Arrears arrears)
        {
            var _arrears = await _getByIdUseCase.ExecuteAsync(arrears.Id).ConfigureAwait(false);
            if (_arrears != null)
                return BadRequest("This record is exists");
            arrears.Id = Guid.NewGuid();
            var response =  await _addUseCase.ExecuteAsync(arrears).ConfigureAwait(false);
            if (response != null)
            {
                return CreatedAtRoute("Get", new { id = arrears.Id }, arrears);
            }
            return BadRequest(new AppException((int) HttpStatusCode.BadRequest, "Arrears record save failed!"));
        }

    }
}
