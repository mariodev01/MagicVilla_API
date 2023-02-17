namespace MagicVilla_Web.Models.DTOS
{
    public class LoginResponseDto
    {
        public UsuarioDto Usuario { get; set; }
        public string Token { get; set; }
    }
}
