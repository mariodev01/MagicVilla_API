using AutoMapper;
using Magic_Villa_API.Datos;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa_API.Controllers
{
    [Route("api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db,
            IMapper mapper)
        {
            _logger = logger;
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("obtener las villas");
            IEnumerable<Villa> villas = await db.villas.ToListAsync();
            return Ok(mapper.Map<IEnumerable<Villa>>(villas));
          //  return new List<VillaDto>
            //{
              //  new VillaDto{Id = 1, Name = "villa san juan"},
                //new VillaDto{Id = 2, Name = "villa campo lindo"}
            //};
        }

        [HttpGet("{id:int}",Name ="GetVilla")]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if(id <= 0)
            {
                _logger.LogError("error con la villa de id " + id);
                return BadRequest();
            }
            var villa = await db.villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreacionDto villa)
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

            Villa modelo = mapper.Map<Villa>(villa);

            await db.villas.AddAsync(modelo);
            await db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new {id = modelo.Id},modelo);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVilla(int id) {

            if (id <= 0)
            {
                return BadRequest();
            }

            var villa = await db.villas.FirstOrDefaultAsync(v=>v.Id== id);
            if (villa == null)
            {
                return NotFound();
            }
            db.villas.Remove(villa);
            await db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVilla(int id, VillaActualizacionDto villaDto)
        {
            if (villaDto == null|| villaDto.Id != id)
            {
                return BadRequest();
            }
            Villa modelo = mapper.Map<Villa>(villaDto);

            db.villas.Update(modelo);
            await db.SaveChangesAsync();
            return NoContent();


        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaActualizacionDto> jsonPatch)
        {
            if (jsonPatch == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await db.villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            VillaActualizacionDto villaActualizacionDto = mapper.Map<VillaActualizacionDto>(villa);

            if(villa ==null) return BadRequest();
            jsonPatch.ApplyTo(villaActualizacionDto,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa modelo = mapper.Map<Villa>(villaActualizacionDto);
            db.villas.Update(modelo);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
