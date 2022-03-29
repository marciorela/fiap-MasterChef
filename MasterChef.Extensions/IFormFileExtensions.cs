using Microsoft.AspNetCore.Http;

namespace MasterChef.Extensions
{
    public static class IFormFileExtensions
    {

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using var ms = new MemoryStream();
            await formFile.CopyToAsync(ms);

            return ms.ToArray();
        }

    }
}