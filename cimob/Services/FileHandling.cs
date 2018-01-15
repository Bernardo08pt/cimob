using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace cimob.Services
{
    public class FileHandling
    {
        internal static async Task<string> Upload(IFormFile file, string folder)
        {
            var path = "Files/" + folder + "/" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + ".pdf";

            if (file.Length > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return path;
        }

        // TODO: implement this
        internal static void Download(string path)
        {
            
        }
    }
}