using System.Text.Json;

namespace Reactivities.API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,
                                               int currentpage,
                                               int itemsPerPage,
                                               int totalItems,
                                               int totalPages)
        {
            var paginationHeader = new
            {
                currentpage,
                itemsPerPage,
                totalItems,
                totalPages
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
