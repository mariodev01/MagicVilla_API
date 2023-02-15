using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        public ApiResponse responseModel { get; set; }

        Task<T> sendAsync<T>(APIRequest apiRequest);
    }
}
