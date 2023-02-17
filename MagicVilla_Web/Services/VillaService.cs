using MagicVilla_Utilities;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOS;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory httpClient;
        private string _villaUrl;
        public VillaService(IHttpClientFactory httpClient,
            IConfiguration configuration): base(httpClient)
        {
            this.httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Crear<T>(VillaCreacionDto dto, string token)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url= _villaUrl+"/api/villa",
                Token  = token
            });
        }

        public Task<T> Actualizar<T>(VillaActualizacionDto dto, string token)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/villa"+dto.Id,
                Token = token
            });
        }

        public Task<T> Obtener<T>(int id, string token)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                
                Url = _villaUrl + "/api/villa/",
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                
                Url = _villaUrl + "/api/villa",
                Token = token
            });
        }

        public Task<T> Remover<T>(int id, string token)
        {   
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/villa/" +id,
                Token = token
            });
        }
    }
}
