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

        private void loginAsCandidato(string user, string pw)
        {
            //Encontra os elementos do form de autenticação
            RemoteWebElement email = (RemoteWebElement)driver.FindElementById("Email");
            RemoteWebElement password = (RemoteWebElement)driver.FindElementById("Password");
            RemoteWebElement button = (RemoteWebElement)driver.FindElement(By.XPath("//button[@type='submit'][text()='Entrar']"));

            //Preenche o formulário e o botão Entrar é clicado
            email.SendKeys(user);
            password.SendKeys(pw);

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

            loginAsCandidato("bernardoamguerreiro@gmail.com", "123456.");

            //Open Menu
            driver.FindElementById("logoutForm").FindElement(By.TagName("button")).Click();
            //Ir para a página para editar o perfil
            driver.FindElementById("logoutForm").FindElement(By.TagName("li")).Click();
            
            //Testar se é preciso preencher os campos
            driver.FindElementById("btnSubmit").Click();

            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='OldPassword']")).Text);
            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='NewPassword']")).Text);
            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='ConfirmPassword']")).Text);

            //Testar se confirma que a password atual está correta
            driver.FindElementById("OldPassword").SendKeys("incorrectPassword");
            driver.FindElementById("NewPassword").SendKeys("newPassword123");
            driver.FindElementById("ConfirmPassword").SendKeys("newPassword123");
            driver.FindElementById("btnSubmit").Click();
            
            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='OldPassword']")).Text);

            //Testar se confirma que a password nova não está segundo as regras pré-definidas
            driver.FindElementById("OldPassword").SendKeys("123456.");
            driver.FindElementById("NewPassword").SendKeys("a");
            driver.FindElementById("ConfirmPassword").SendKeys("a");
            driver.FindElementById("btnSubmit").Click();

            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='NewPassword']")).Text);

            //Testar se confirma que a nova password e a de confirmação estão iguais
            driver.FindElementById("OldPassword").SendKeys("123456.");
            driver.FindElementById("NewPassword").SendKeys("123456..");
            driver.FindElementById("ConfirmPassword").SendKeys("123456...");
            driver.FindElementById("btnSubmit").Click();
            
            Assert.AreNotEqual("", driver.FindElement(By.XPath("//*[@data-valmsg-for='ConfirmPassword']")).Text);

            //Testar se a mudança de password teve sucesso
            driver.FindElementById("OldPassword").SendKeys("123456.");
            driver.FindElementById("NewPassword").SendKeys("123456..");
            driver.FindElementById("ConfirmPassword").SendKeys("123456..");
            driver.FindElementById("btnSubmit").Click();

            //Open Menu
            driver.FindElementById("logoutForm").FindElement(By.TagName("button")).Click();
            driver.FindElementById("logout").Click();

            loginAsCandidato("bernardoamguerreiro@gmail.com", "123456..");
            Assert.AreEqual("http://cimob.azurewebsites.net/Manage/Profile", driver.Url);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
