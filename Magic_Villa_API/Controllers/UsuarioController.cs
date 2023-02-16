using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;
using Magic_Villa_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_Villa_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository usuariorepo;
        private ApiResponse _response;
        public UsuarioController(IUsuarioRepository usuariorepo)
        {
            this.usuariorepo = usuariorepo;
            _response = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = await this.usuariorepo.Login(modelo);
            if (loginResponse.Usuario == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.isExitoso = false;
                _response.ErrorMessage.Add("UserName o Password son incorrectos");
                return BadRequest(_response);
            }
            _response.isExitoso = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Resultado = loginResponse;
            return Ok(_response);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDTO modelo)
        {
            bool isUsuarioUnico = usuariorepo.IsUsuarioUnico(modelo.UserName);
            if (!isUsuarioUnico) {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.isExitoso = false;
                _response.ErrorMessage.Add("Usuario ya existe");
                return BadRequest(_response);
            }
            var usuario = await usuariorepo.Registrar(modelo);
            if(usuario == null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.isExitoso = false;
                _response.ErrorMessage.Add("Error al registrar Usuario");
                return BadRequest(_response);
            }
            _response.statusCode = HttpStatusCode.OK;
            _response.isExitoso = true;
            return Ok(_response);
        }

    }
}
