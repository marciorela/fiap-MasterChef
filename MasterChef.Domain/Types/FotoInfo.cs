using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Types
{
    public class FotoInfo
    {

        public FotoInfo()
        {

        }

        public FotoInfo(string? fotoName, string? fotoContent)
        {
            FileName = fotoName;
            ContentBase64 = fotoContent;
        }

        public static FotoInfo CreateFrom(string? fotoObjectBase64)
        {
            if (string.IsNullOrWhiteSpace(fotoObjectBase64))
            {
                return new FotoInfo();
            }
            else
            {
                return JsonConvert.DeserializeObject<FotoInfo>(fotoObjectBase64) ?? new FotoInfo();
            }
        }

        public string? FileName { get; set; }

        public string? ContentBase64 { get; set; }
    }
}
