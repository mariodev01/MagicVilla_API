using Magic_Villa_API.Datos;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Magic_Villa_API.Controllers
{
    [Route("api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext db;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("obtener las villas");
            return Ok(db.villas.ToList());
          //  return new List<VillaDto>
            //{
              //  new VillaDto{Id = 1, Name = "villa san juan"},
                //new VillaDto{Id = 2, Name = "villa campo lindo"}
            //};
        }

        [HttpGet("{id:int}",Name ="GetVilla")]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id <= 0)
            {
                _logger.LogError("error con la villa de id " + id);
                return BadRequest();
            }
            var villa = db.villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(db.villas.FirstOrDefault(v=>v.Name.ToLower() == villa.Name.ToLower())!= null)
            {
                ModelState.AddModelError("duplicado", "Ya existe una villa con ese nombre");
                return BadRequest(ModelState);
            }
            if(villa == null)
            {
                return BadRequest();
            }

            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa modelo = new()
            {
                Name= villa.Name,
                Detalles =villa.Detalles,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa= villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };

            db.villas.Add(modelo);
            db.SaveChanges();
            return CreatedAtRoute("GetVilla", new {id = villa.Id},villa);
        }

        [HttpDelete]
        public IActionResult DeleteVilla(int id) {

            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = db.villas.FirstOrDefault(v=>v.Id== id);
            if (villa == null)
            {
                return NotFound();
            }
            db.villas.Remove(villa);
            db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateVilla(int id, VillaDto villaDto)
        {
            if (villaDto == null|| villaDto.Id != id)
            {
                return BadRequest();
            }
            Villa modelo = new()
            {
                Name = villaDto.Name,
                Detalles = villaDto.Detalles,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            db.villas.Update(modelo);
            db.SaveChanges();
            return NoContent();


        }

        [HttpPatch("{id:int}")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> jsonPatch)
        {
            if (jsonPatch == null || id == 0)
            {
                return BadRequest();
            }

            var villa = db.villas.FirstOrDefault(v => v.Id == id);

            VillaDto villaDto = new()
            {
                Name = villa.Name,
                Detalles = villa.Detalles,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };

            if(villa ==null) return BadRequest();
            jsonPatch.ApplyTo(villaDto,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa modelo = new()
            {
                Name = villaDto.Name,
                Detalles = villaDto.Detalles,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };
            db.villas.Update(modelo);
            db.SaveChanges();
            return NoContent();
        }
    }
}
