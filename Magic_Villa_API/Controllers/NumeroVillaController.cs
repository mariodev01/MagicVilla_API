using AutoMapper;
using Magic_Villa_API.Datos;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;
using Magic_Villa_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Magic_Villa_API.Controllers
{
    [Route("api/Numerovilla")]
    [ApiController]
    [Authorize]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepository _villarepo;
        private readonly INumeroVillaRepository _numerorepo;
        private readonly IMapper mapper;
        protected ApiResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepository villarepo,
            IMapper mapper,INumeroVillaRepository numerorepo)
        {
            _logger = logger;
            _villarepo = villarepo;
            _numerorepo = numerorepo;
            this.mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetNumeroVillas()
        {

            try
            {
                _logger.LogInformation("obtener numero de las villas");
                IEnumerable<NumeroVilla> numerovillas = await _numerorepo.ObtenerTodos();

                _response.Resultado = mapper.Map<IEnumerable<NumeroVillaDTO>>(numerovillas);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.isExitoso = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
            
          //  return new List<VillaDto>
            //{
              //  new VillaDto{Id = 1, Name = "villa san juan"},
                //new VillaDto{Id = 2, Name = "villa campo lindo"}
            //};
        }

        [HttpGet("{id:int}",Name ="GetNumeroVilla")]
        public async Task<ActionResult<ApiResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError("error con la villa de id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Numerovilla = await _numerorepo.Obtener(v => v.VillaNo == id);
                if (Numerovilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = mapper.Map<NumeroVillaDTO>(Numerovilla);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isExitoso=false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
           
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDTO villa)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                if (await _numerorepo.Obtener(v => v.VillaNo == villa.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de la villa ya existe!");
                    return BadRequest(ModelState);
                }
                if (_villarepo.Obtener(v=>v.Id==villa.VillaId)==null)
                {
                    ModelState.AddModelError("claveForanea", "El id de la villa no existe!");
                    return BadRequest();
                }

                if(villa == null)
                {
                    return BadRequest(villa);
                }
                NumeroVilla modelo = mapper.Map<NumeroVilla>(villa);
                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numerorepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response; 
            
        }

        [HttpDelete]
        public async Task<IActionResult<ApiResponse>> DeleteNumeroVilla(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return (IActionResult<ApiResponse>)BadRequest(_response);
                }

                var Numerovilla = await _numerorepo.Obtener(v => v.VillaNo == id);
                if (Numerovilla == null)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return (IActionResult<ApiResponse>)NotFound(_response);
                }
               await _numerorepo.Remover(Numerovilla);

                _response.statusCode = HttpStatusCode.NoContent;
                return (IActionResult<ApiResponse>)Ok(_response);
            }
            catch (Exception ex)
            {
                _response.statusCode = HttpStatusCode.NoContent;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return (IActionResult<ApiResponse>)BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNumeroVilla(int id, NumeroVillaUpdateDTO villaDto)
        {
            if (villaDto == null|| villaDto.VillaNo != id)
            {
                _response.isExitoso=false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if(await _villarepo.Obtener(v=>v.Id == villaDto.VillaId) == null)
            {
                ModelState.AddModelError("claveForanea", "El id de la villa no existe");
                return BadRequest(ModelState);
            }
            NumeroVilla modelo = mapper.Map<NumeroVilla>(villaDto);

           await _numerorepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();


        }
    }
}
