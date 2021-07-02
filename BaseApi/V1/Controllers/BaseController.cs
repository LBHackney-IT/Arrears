using System.Collections.Generic;
using System.Linq;
using System.Net;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ArrearsApi.V1.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ConfigureJsonSerializer();
        }
        protected ActionResult HandleResult<T>(T result)
        {
            if (result == null) return NotFound(new AppException((int) HttpStatusCode.NotFound, "No Arrear by provided arrearId cannot be found!"));
            if (result!=null)
                return Ok(result);
            
            return BadRequest(result);
        }
        public string GetCorrelationId()
        {
            StringValues correlationId;
            HttpContext.Request.Headers.TryGetValue(Constants.CorrelationId, out correlationId);

            if (!correlationId.Any())
                throw new KeyNotFoundException("Request is missing a correlationId");

            return correlationId.First();
        }

        public static void ConfigureJsonSerializer()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;

                return settings;
            };
        }
    }
}
