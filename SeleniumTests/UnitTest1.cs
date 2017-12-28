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
    public class UnitTest1
    {

        private string baseURL;
        private RemoteWebDriver driver;

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        //É testado se quando o utilizador não insere uma data de nascimento se aparece a mensagem de erro.
        public void RegisterTest()
        {
            driver = new ChromeDriver();
            //Para firefox
            //driver = new FirefoxDriver();
            //baseURL = "http://cimob.azurewebsites.net/Account/Register?returnurl=%2F";
            
            //usar esta linha quando testamos com a BD local
            baseURL = "https://localhost:44307/Account/Register?returnurl=%2F";
            driver.Navigate().GoToUrl(baseURL);

            //Encontra os elementos do form de autenticação
            RemoteWebElement number = (RemoteWebElement)driver.FindElementById("Numero");
            RemoteWebElement name = (RemoteWebElement)driver.FindElementById("Nome");
            RemoteWebElement birthDate = (RemoteWebElement)driver.FindElementById("DataNascimento");
            RemoteWebElement email = (RemoteWebElement)driver.FindElementById("Email");
            RemoteWebElement password = (RemoteWebElement)driver.FindElementById("Password");
            RemoteWebElement confirmpassword = (RemoteWebElement)driver.FindElementById("ConfirmPassword");
            RemoteWebElement button = (RemoteWebElement)driver.FindElement(By.XPath("//button[@type='submit'][text()='Registar']"));

            //Preenche o formulário e o botão Registar é clicado
            number.SendKeys("150221021");
            name.SendKeys("Andreia Pereira");
            email.SendKeys("150221021@estudantes.ips.pt");
            password.SendKeys("123456");
            confirmpassword.SendKeys("123456");

            button.Click();

            //Encontra o elemento que mostra a mensagem de erro
            RemoteWebElement errorDataNascimento = (RemoteWebElement)driver.FindElementById("DataNascimento-error");
            Assert.AreEqual("O campo Data de Nascimento é obrigatório.", errorDataNascimento.Text);
        }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        //É testado se aparece uma mensagem de erro se o utilizador tentar entrar com um email não verificado.
        //REGISTAR PRIMEIRO O andreiiap02@hotmail.com para depois dar o teste bem
        public void LoginWithoutConfirmedEmailTest()
        {
            driver = new ChromeDriver();
            //Para firefox
            //driver = new FirefoxDriver();
            //baseURL = "http://cimob.azurewebsites.net/Account/Login";

            //usar esta linha quando testamos com a BD local
            baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);

            //Encontra os elementos do form de autenticação
            RemoteWebElement email = (RemoteWebElement)driver.FindElementById("Email");
            RemoteWebElement password = (RemoteWebElement)driver.FindElementById("Password");
            RemoteWebElement button = (RemoteWebElement)driver.FindElement(By.XPath("//button[@type='submit'][text()='Entrar']"));

            //Preenche o formulário e o botão Entrar é clicado
            email.SendKeys("andreiiap02@hotmail.com");
            password.SendKeys("123456");

            button.Click();

            //Encontra o elemento que mostra a mensagem de erro
            //RemoteWebElement emailNotConfirmed = (RemoteWebElement)driver.FindElement(By.TagName("li"));
            Assert.AreEqual("O Email ainda não está verificado.", ((RemoteWebElement)driver.FindElement(By.TagName("li"))).Text);
        }

        [TestMethod]
         [TestCategory("Selenium")]
         [Priority(1)]
         // É testado se aparece uma mensagem de erro se o utilizador usar as credenciais erradas
         public void LoginInvalidCredentials()
         {
             driver = new ChromeDriver();
            //Para firefox
            //driver = new FirefoxDriver();
            //baseURL = "http://cimob.azurewebsites.net/Account/Login";

            //usar esta linha quando testamos com a BD local
            baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);

             //Encontra os elementos do form de autenticação
             RemoteWebElement email = (RemoteWebElement)driver.FindElementById("Email");
             RemoteWebElement password = (RemoteWebElement)driver.FindElementById("Password");
             RemoteWebElement button = (RemoteWebElement)driver.FindElement(By.XPath("//button[@type='submit'][text()='Entrar']"));

             //Preenche o formulário e o botão Entrar é clicado
             /*email.SendKeys("150220189@estudantes.pt");
             password.SendKeys("123456");*/

            email.SendKeys("150221021@estudantes.pt");
            password.SendKeys("123123");

            button.Click();

            RemoteWebElement loginInvalido = (RemoteWebElement)driver.FindElement(By.TagName("li"));

             //Encontra o elemento que mostra a mensagem de erro
            Assert.AreEqual("Tentativa de login inválida.", loginInvalido.Text);
         }

        [TestMethod]
         [TestCategory("Selenium")]
         [Priority(1)]
         // É testado se quando o utilizador insere o email e password corretos é iniciada a sessão e redirecionado para a home page
         public void LoginSuccess()
         {
             driver = new ChromeDriver();
            //Para firefox
            //driver = new FirefoxDriver();
            //baseURL = "http://cimob.azurewebsites.net/Account/Login";

            //usar esta linha quando testamos com a BD local
            baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);


            //Encontra os elementos do form de autenticação
             RemoteWebElement email = (RemoteWebElement)driver.FindElementById("Email");
             RemoteWebElement password = (RemoteWebElement)driver.FindElementById("Password");
             RemoteWebElement button = (RemoteWebElement)driver.FindElement(By.XPath("//button[@type='submit'][text()='Entrar']"));

            //Preenche o formulário e o botão Entrar é clicado
            /* email.SendKeys("150220189@estudantes.ips.pt");
             password.SendKeys("123456");*/
            email.SendKeys("150221021@estudantes.ips.pt");
            password.SendKeys("123456");
            button.Click();

            //Encontra o elemento que mostra a mensagem de erro
            // Assert.AreEqual("http://cimob.azurewebsites.net/", driver.Url);
            //usar esta linha quando testamos com a BD local
            Assert.AreEqual("https://localhost:44307/", driver.Url);
            
         }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }

}
