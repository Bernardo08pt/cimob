using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using cimob.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace cimob.Services
{
    /// <summary>
    /// Classe auxiliar para tratamento de ficheiros
    /// </summary>
    public class FileHandling
    {
        /// <summary>
        /// Valida o ficheiro (tamanho, format), altera o nome para timestamp de quando foi carregado
        /// e move-o para a respetiva pasta no servidor
        /// </summary>
        /// <param name="file">ficheiro a ser carregado</param>
        /// <param name="folder">pasta para onde o ficheiro vai (candidaturas ou editais)</param>
        /// <returns>string com o caminho do ficheiro</returns>
        internal static async Task<string> Upload(IFormFile file, string folder)
        {
            var dir = "Files/" + folder;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var path = dir + "/" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + ".pdf";

            if (file.Length == 0)
                throw new NoFileExpcetion();

            if (file.Length > 1000000)
                throw new FileSizeException();

            var tmp = file.FileName.Split(".");

            if ((tmp[tmp.Length - 1]).ToLower() != "pdf")
                throw new FormatException();
                
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }
        
        /// <summary>
        /// tenta ir buscar o ficheiro ao servidor e devovle-o para ser descarregado. Se não conseguir lança uma exceção
        /// </summary>
        /// <param name="path">caminho do ficheiro</param>
        /// <param name="name">nome que ficheiro vai ter quando for descarregado</param>
        /// <returns>o ficheiro</returns>
        internal static FileResult Download(string path, string name)
        {
            if (path == null || path == "" || name == "" || name == null)
                throw new NoFileExpcetion();

            return new FileContentResult(File.ReadAllBytes(path), "application/x-msdownload") {
                FileDownloadName = name
            };
        }
        
        /// <summary>
        /// tentar ir buscar o ficheiro ao servidor e retorna-o como pdf para visualização no browser. Se não conseguir lança uma exceção
        /// </summary>
        /// <param name="path">caminho do ficheiro que vamos procurar</param>
        /// <returns>filestream</returns>
        internal static FileResult View(string path)
        {
            if (path == null || path == "")
                throw new NoFileExpcetion();

            return new FileStreamResult(
                new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read
                ),
                "application/pdf"
            );
        }

        /// <summary>
        /// Procura se um ficheiro existe e se existir, apaga-o
        /// </summary>
        /// <param name="caminho">ficheiro que vamos apagar</param>
        internal static void Remove(string caminho)
        {
            if (File.Exists(caminho))
                File.Delete(caminho);
        }
    }
}