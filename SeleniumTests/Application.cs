using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
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
    public class Application
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
        public void RealizarCandidatura()
        {
            driver = new ChromeDriver(@"C:\Users\Bernardo\Desktop\PROJETOSW\drivers");
            //Para firefox
            //driver = new FirefoxDriver();
            baseURL = "http://cimob.azurewebsites.net";

            //usar esta linha quando testamos com a BD local
            //baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);

            loginAsCandidato("bernardoamguerreiro@gmail.com", "123456.");
      
            driver.FindElement(By.CssSelector("[href*='/ProgramasMobilidade']")).Click();
            driver.FindElement(By.XPath("//*[@data-id='4']")).Click();

            driver.FindElement(By.XPath("//select[@id='EscolaList']")).Click();
            driver.FindElement(By.XPath("//select[@id='EscolaList']")).FindElement(By.CssSelector("[value*='1']")).Click();

            driver.FindElement(By.XPath("//select[@id='CursoList']")).Click();
            driver.FindElement(By.XPath("//select[@id='CursoList']")).FindElement(By.CssSelector("[value*='18']")).Click();
            driver.FindElementById("btn_next").Click();

            driver.FindElementById("EmailAlternativo").SendKeys("bernardo_a_guerreiro@hotmail.com");
            driver.FindElementById("ContactoPessoal").SendKeys("917703523");
            driver.FindElementById("ContactoEmergencia").SendKeys("917703523");
            driver.FindElementById("ParentescoList").Click();
            driver.FindElementById("ParentescoList").FindElement(By.CssSelector("[value*='1']")).Click();
            driver.FindElementById("btn_next").Click();

            driver.FindElementById("file_name").SendKeys(@"C:\Users\Bernardo\Downloads\GUERREIRO_BERNARDO.pdf");
            driver.FindElementById("btn_submit_docs").Click();

            driver.FindElementById("btn_next").Click();

            driver.FindElement(By.TagName("checkbox")).Click();
            driver.FindElementById("btn_submit");
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
