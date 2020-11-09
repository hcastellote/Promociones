using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promociones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
        private IPromocionService _promocionService;
        public PromocionController(IPromocionService pPromocionService)
        {
            _promocionService = pPromocionService;
        }
        // GET: api/<PromocionController>
        [HttpGet]
        public IEnumerable<Promocion> Get()
        {
            return _promocionService.GetAll();
        }

        // GET api/<PromocionController>/5
        [HttpGet("{id}")]
        public Promocion Get(Guid id)
        {
            return _promocionService.Get(id);
        }

        // GET api/<PromocionController>/5
        [HttpGet("PromocionesVigentes")]
        public IEnumerable<Promocion> PromocionesVigentes()
        {
            return _promocionService.GetListadoPromocionesVigentes();
        }

        // GET api/<PromocionController>/5
        [HttpGet("PromocionesVigentesFecha/{fecha}")]
        public IEnumerable<Promocion> PromocionesVigentesFecha(string fecha)
        {
            return _promocionService.GetListadoPromocionesVigentesFecha(Convert.ToDateTime(fecha));
        }

        // GET api/<PromocionController>/5
        [HttpPost("PromocionesVigentesVenta")]
        public IEnumerable<Promocion> PromocionesVigentesVenta([FromBody] Promocion value)
        {
            return _promocionService.GetListadoPromocionesVigentesVenta(value.MediosDePago.First(), value.Bancos.First(), value.CategoriasProductos);
        }

        // POST api/<PromocionController>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Promocion value)
        {
            try
            {
                this._promocionService.Save(value);
                return  new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError};
            }
        }

        // PUT api/<PromocionController>/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(Guid id, [FromBody] Promocion value)
        {
            try
            {
                _promocionService.Save(value);
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        // PUT api/<PromocionController>/5
        [HttpPut("ModificarVigenciaPromocion")]
        public HttpResponseMessage ModificarVigenciaPromocion([FromBody] Promocion value)
        {
            try
            {
                _promocionService.ModificaVigenciaPromocion(value.Id, (DateTime)value.FechaInicio, (DateTime)value.FechaFin);
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        // DELETE api/<PromocionController>/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] Promocion value)
        {
            try
            {
                _promocionService.Delete(value);
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
            }
        }


        public ObjectResult Response(int pHttpCode, string pResponseCode, object pData, string pMessage)
        {
            return new ObjectResult(new { HttpCode = pHttpCode, ResponseCode = pResponseCode, Data = pData, Message = pMessage });
        }
        public ObjectResult Response500()
        {
            return Response(500, "ERROR", null, "Internal Error Server.");
        }
        public ObjectResult Response200(object pObject, string pMessage)
        {
            return Response(200, "OK", pObject, pMessage);
        }
        public ObjectResult ResponseError(object pObject, string pMessage)
        {
            return Response(200, "ERROR", pObject, pMessage);
        }
    }
}
