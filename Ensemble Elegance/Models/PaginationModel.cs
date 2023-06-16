using Microsoft.Identity.Client;
using System.Net;

namespace Ensemble_Elegance.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
