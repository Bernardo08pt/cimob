using Xunit;
using cimob.Controllers;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;

namespace XUnitTests
{
    public class Candidatura
    {
        private ApplicationController controller;
        
        public Candidatura()
        {    
            controller = new ApplicationController(null, null, null, Globals.Context);
        }

        [Fact]
        public void UploadCorrectFile()
        {
            var file = new Mock<IFormFile>();
            
            var source = File.OpenRead(@"TestFiles/1mbFile.pdf");

            var stream = new MemoryStream { Position = 0 };
            var writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            stream.Position = 0;

            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.Length).Returns(stream.Length);

            var res = controller.UploadFile(file.Object);

            file.Verify();
        }

        [Fact]
        public void UploadBigFile()
        {
            var file = new Mock<IFormFile>();

            var source = File.OpenRead(@"TestFiles/2mbFile.pdf");
            var stream = new MemoryStream { Position = 0 };
            var writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            stream.Position = 0;

            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.Length).Returns(stream.Length);

            var res = controller.UploadFile(file.Object);

            file.Verify();
        }

        [Fact]
        public void UploadIncorrectFileType()
        {
            var file = new Mock<IFormFile>();

            var source = File.OpenRead(@"TestFiles/notPDF.png");
            var stream = new MemoryStream { Position = 0 };
            var writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            stream.Position = 0;

            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.Length).Returns(stream.Length);

            var res = controller.UploadFile(file.Object);

            file.Verify();
        }
    }
}
