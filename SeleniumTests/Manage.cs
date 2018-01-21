using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;

/* Passos que tive que fazer para os testes
  Precisamos de ter os drivers como system environment variables, 
    para isso escrevemos no windows (edit the system environment variables -> environment variables -> (system variables) path -> edit -> new -> colocamos o caminho onde esta os ficheiros dos drivers)
  Precisamos de ter instalado no VS: 
    Selenium.WebDriver
    Selenium.WebDriver.ChromeDriver
    Selenium.WebDriver.IEDriver
    Selenium.Firefox.WebDriver
*/

namespace SeleniumTests
{
    [TestClass]
    public class Manage
    {
        private string baseURL;
        private RemoteWebDriver driver;

        private void loginAsCandidato()
        {
            //Encontra os elementos do form de autenticação
            RemoteWebElement email = (RemoteWebElement)driver.FindElementById("Email");
            RemoteWebElement password = (RemoteWebElement)driver.FindElementById("Password");
            RemoteWebElement button = (RemoteWebElement)driver.FindElement(By.XPath("//button[@type='submit'][text()='Entrar']"));

            //Preenche o formulário e o botão Entrar é clicado
            email.SendKeys("bernardoamguerreiro@gmail.com");
            password.SendKeys("123456.");

            button.Click();

        }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        //É testado se quando o utilizador não insere a password atual corretamente
        public void MudarPasswordTest()
        {
            driver = new ChromeDriver(@"C:\Users\Bernardo\Desktop\PROJETOSW\drivers");
            //Para firefox
            //driver = new FirefoxDriver();
            baseURL = "http://cimob.azurewebsites.net";

            //usar esta linha quando testamos com a BD local
            //baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);

            loginAsCandidato();

            //Open Menu
            RemoteWebElement menu = (RemoteWebElement)driver.FindElementById("logoutForm").FindElement(By.TagName("button"));
            menu.Click();
            //Ir para a página para editar o perfil
            RemoteWebElement linkPerfil = (RemoteWebElement)driver.FindElementById("logoutForm").FindElement(By.TagName("li"));
            linkPerfil.Click();


            //Erros
            RemoteWebElement errorOldPassword = (RemoteWebElement)driver.FindElement(By.XPath("//*[@data-valmsg-for='OldPassword']"));
            RemoteWebElement errorNewPassword = (RemoteWebElement)driver.FindElement(By.XPath("//*[@data-valmsg-for='NewPassword']"));
            RemoteWebElement errorConfirmPassword = (RemoteWebElement)driver.FindElement(By.XPath("//*[@data-valmsg-for='ConfirmPassword']"));
            //TextBoxes
            RemoteWebElement textBoxOldPassword = (RemoteWebElement)driver.FindElementById("OldPassword");
            RemoteWebElement textBoxNewPassword = (RemoteWebElement)driver.FindElementById("NewPassword");
            RemoteWebElement textBoxConfirmPassword = (RemoteWebElement)driver.FindElementById("ConfirmPassword");


            //Testar se é preciso preencher os campos
            RemoteWebElement botaoSubmeter = (RemoteWebElement)driver.FindElementById("btnSubmit");
            botaoSubmeter.Click();            

            Assert.AreNotEqual("", errorOldPassword.Text);
            Assert.AreNotEqual("", errorNewPassword.Text);
            Assert.AreNotEqual("", errorConfirmPassword.Text);

            //Testar se confirma que a password atual está correta
            textBoxOldPassword.SendKeys("incorrectPassword");
            textBoxNewPassword.SendKeys("newPassword123");
            textBoxConfirmPassword.SendKeys("newPassword123");
            botaoSubmeter.Click();

            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='OldPassword']")).Text);

            //Testar se confirma que a password nova não está segundo as regras pré-definidas
            textBoxOldPassword.SendKeys("123456.");
            textBoxNewPassword.SendKeys("a");
            textBoxConfirmPassword.SendKeys("a");
            botaoSubmeter.Click();

            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='OldPassword']")).Text);

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }

}
