using Magic_Villa_API.Modelos;
using Magic_Villa_API.Modelos.DTOS;

namespace Magic_Villa_API.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        bool IsUsuarioUnico(string userName);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
