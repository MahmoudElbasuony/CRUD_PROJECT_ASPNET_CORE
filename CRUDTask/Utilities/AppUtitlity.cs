using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDTask.Utilities
{
    public static class AppUtitlity
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public static async Task<byte[]> ReadImageFileFromRequest(IList<IFormFile> photos)
        {
            byte[] company_photo = null;
            if (photos.Count > 0)
            {
                var photo = photos[0];
                if (!ImageExtensions.Contains(Path.GetExtension(photo.FileName).ToUpperInvariant()))
                {
                    throw new Exception($"Photo extension should be one of the following ( {string.Join(',', AppUtitlity.ImageExtensions)} )");
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    await photos[0].CopyToAsync(stream);
                    company_photo = stream.ToArray();
                }
            }
            return company_photo;
        }
    }
}
