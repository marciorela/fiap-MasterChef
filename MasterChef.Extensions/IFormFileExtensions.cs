using MasterChef.Domain.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

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

        public static async Task<string> ToBase64(this IFormFile? formFile)
        {
            if (formFile == null)
            {
                return "";
            }
            return Convert.ToBase64String(await formFile.GetBytes());
        }

    }
}