using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace cimob.Services
{
    public class FileHandling
    {
        public static async Task<string> Upload(IFormFile file)
        {
            var path = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return path;
        }
    }
}