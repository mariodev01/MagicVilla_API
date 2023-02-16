using Magic_Villa_API.Datos;
using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;
using Magic_Villa_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_Villa_API.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext context;
        private string secretKey;
        public UsuarioRepository(ApplicationDbContext context,
            IConfiguration configuration)
        {
            this.context = context;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUsuarioUnico(string userName)
        {
            var usuario = context.usuarios.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if(usuario == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var usuario = await context.usuarios.FirstOrDefaultAsync(u=>u.UserName.ToLower()== loginRequestDTO.UserName.ToLower()
            && u.Password== loginRequestDTO.Password);

            if(usuario == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
               Subject= new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role,usuario.Rol)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuario
            };
            return loginResponseDTO;
        }

        public async Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO)
        {
            Usuario usuario = new()
            {
                UserName= registroRequestDTO.UserName,
                Password= registroRequestDTO.Password,
                Nombres= registroRequestDTO.Nombres,
                Rol = registroRequestDTO.Rol
            };
            await context.AddAsync(usuario);
            await context.SaveChangesAsync();
            usuario.Password = "";
            return usuario;
        }
    }
}
