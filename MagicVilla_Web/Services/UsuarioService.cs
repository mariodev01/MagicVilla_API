using MagicVilla_Utilities;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOS;

namespace MagicVilla_Web.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IHttpClientFactory _httpClient;
        private string _villaUrl;
        public UsuarioService(IHttpClientFactory httpClient,
            IConfiguration configuration): base(httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Login<T>(LoginRequestDto dto)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl+"/api/usuario/login"
            });
        }

        public Task<T> Registrar<T>(RegistroRequestDto dto)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl + "/api/usuario/registrar"
            });
        }
    }
}
