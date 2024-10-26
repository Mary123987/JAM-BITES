using System.Threading.Tasks;
using JAM_BITES.Models;

namespace JAM_BITES.Services
{
    public interface IServicioNoticias
    {
        Task<RespuestaNoticias> ObtenerNoticiasAsync();
    }
}
