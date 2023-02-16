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
    [Route("api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepository _villarepo;
        private readonly IMapper mapper;
        protected ApiResponse _response;

        public VillaController(ILogger<VillaController> logger, IVillaRepository villarepo,
            IMapper mapper)
        {
            _logger = logger;
            _villarepo = villarepo;
            this.mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetVillas()
        {

            try
            {
                _logger.LogInformation("obtener las villas");
                IEnumerable<Villa> villas = await _villarepo.ObtenerTodos();

                _response.Resultado = mapper.Map<IEnumerable<VillaDto>>(villas);
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

        [HttpGet("{id:int}",Name ="GetVilla")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetVilla(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogError("error con la villa de id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _villarepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = mapper.Map<VillaDto>(villa);
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
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ApiResponse>> CrearVilla([FromBody] VillaCreacionDto villa)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                if (await _villarepo.Obtener(v => v.Name.ToLower() == villa.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("duplicado", "Ya existe una villa con ese nombre");
                    return BadRequest(ModelState);
                }
                if (villa == null)
                {
                    return BadRequest();
                }

                Villa modelo = mapper.Map<Villa>(villa);

                await _villarepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.isExitoso = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response; 
            
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult<ApiResponse>> DeleteVilla(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return (IActionResult<ApiResponse>)BadRequest(_response);
                }

                var villa = await _villarepo.Obtener(v => v.Id == id);
                if (villa == null)
                {
                    _response.isExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return (IActionResult<ApiResponse>)NotFound(_response);
                }
               await _villarepo.Remover(villa);

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVilla(int id, VillaActualizacionDto villaDto)
        {
            if (villaDto == null|| villaDto.Id != id)
            {
                _response.isExitoso=false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            Villa modelo = mapper.Map<Villa>(villaDto);

           await _villarepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();


        }

        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaActualizacionDto> jsonPatch)
        {
            if (jsonPatch == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _villarepo.Obtener(v => v.Id == id,tracked:false);

            VillaActualizacionDto villaActualizacionDto = mapper.Map<VillaActualizacionDto>(villa);

            if(villa ==null) return BadRequest();
            jsonPatch.ApplyTo(villaActualizacionDto,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa modelo = mapper.Map<Villa>(villaActualizacionDto);
           await _villarepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
