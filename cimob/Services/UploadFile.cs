using cimob.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace cimob.Services
{
    public class FileHandling
    {
        public static async Task<Documento> Upload(IFormFile file, int cimob)
        {
            var path = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return new Documento { FicheiroCaminho = path, FicheiroNome = file.FileName, OrigemCimob = cimob };
        }
    }
}