using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.CompilerServices;

namespace Ensemble_Elegance.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }

        public List<T> GetPagination<T>(List<T> inputValues)
        {
            int itemsCount = inputValues.Count();

            TotalPageCount = (int)Math.Ceiling(itemsCount / (double)PageSize);

            var paginatedData = inputValues.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            return paginatedData;
        }
    }

}
