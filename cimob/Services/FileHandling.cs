using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using cimob.Extensions;

namespace cimob.Services
{
    public class FileHandling
    {
        internal static async Task<string> Upload(IFormFile file, string folder)
        {
            var path = "Files/" + folder + "/" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + ".pdf";

            if (file.Length == 0)
                throw new NoFileExpcetion();

            if (file.Length > 1000000)
                throw new FileSizeException();

            var tmp = file.FileName.Split(".");

            if (tmp[tmp.Length - 1].ToLower() != "pdf")
                throw new FormatException();
                
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }

        // TODO: implement this
        internal static void Download(string path)
        {
            
        }
    }
}