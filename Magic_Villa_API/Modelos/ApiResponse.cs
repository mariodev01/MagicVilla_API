using System.Net;

namespace Magic_Villa_API.Modelos
{
    public class ApiResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public bool isExitoso { get; set; } = true;

        public List<string> ErrorMessage { get; set; }

        public object Resultado { get; set; }
    }
}
