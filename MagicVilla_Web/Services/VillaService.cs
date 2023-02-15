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
        public Task<T> Crear<T>(VillaCreacionDto dto)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url= _villaUrl+"/api/villa"
            });
        }

        public Task<T> Actualizar<T>(VillaActualizacionDto dto)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/villa"+dto.Id
            });
        }

        public Task<T> Obtener<T>(int id)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                
                Url = _villaUrl + "/api/villa/"
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                
                Url = _villaUrl + "/api/villa"
            });
        }

        public Task<T> Remover<T>(int id)
        {
            return sendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/villa/" +id
            });
        }
    }
}
