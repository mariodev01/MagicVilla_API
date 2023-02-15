using System.Net;

namespace MagicVilla_Web.Models
{
    public class ApiResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public bool isExitoso { get; set; } = true;

        public List<string> ErrorMessage { get; set; }

        public object Resultado { get; set; }
    }
}
