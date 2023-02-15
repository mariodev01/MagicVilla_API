using static MagicVilla_Utilities.DS;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;
        
        public string Url { get; set; }

        public object Datos { get; set; }
    }
}
