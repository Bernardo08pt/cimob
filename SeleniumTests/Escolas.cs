using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumTests
{
    [TestClass]
    public class Escolas
    {
        private string baseURL;
        private RemoteWebDriver driver;
        
        private void LoginAsFuncionario(string user, string pw)
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
        //É testado se uma escola inserida corretamente
        public void AdicionarEscola()
        {
            driver = new ChromeDriver();

            //usar esta linha quando testamos com a BD local
            baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);

            LoginAsFuncionario("andreiiap02@hotmail.com", "!123456");

            driver.FindElementByCssSelector("a[href=\"/Escolas\"]").Click();
            driver.FindElementByCssSelector("a[href=\"/Escolas/Create\"]").Click();
            driver.FindElementById("Nome").SendKeys("Teste");
            driver.FindElementById("Email").SendKeys("teste@hotmail.com");
            driver.FindElement(By.XPath("//select[@id='Pais']")).Click();
            driver.FindElement(By.XPath("//select[@id='Pais']")).FindElement(By.CssSelector("[value*='1']")).Click();

            driver.FindElementById("btn_next").Click();

            driver.FindElementByCssSelector("input[name=mobilidade]").Click();

            driver.FindElementById("btn_submit").Click();
        }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        //É testado a edição do nome da escola e é adicionado mais um curso
        public void EditarEscola()
        {
            driver = new ChromeDriver();

            //usar esta linha quando testamos com a BD local
            baseURL = "https://localhost:44307/Account/Login?ReturnUrl=%2F";
            driver.Navigate().GoToUrl(baseURL);

            LoginAsFuncionario("andreiiap02@hotmail.com", "!123456");

            driver.FindElementByCssSelector("a[href=\"/Escolas\"]").Click();
            driver.FindElementByCssSelector("a[href=\"/Escolas/1/Edit/\"]").Click();

            //Editar o nome da escola
            driver.FindElementById("Nome").Clear();
            driver.FindElementById("Nome").SendKeys("Escola Editada");
            driver.FindElement(By.XPath("//select[@id='Pais']")).Click();
            driver.FindElement(By.XPath("//select[@id='Pais']")).FindElement(By.CssSelector("[value*='2']")).Click();

            //Adicionado um curso a escola
            driver.FindElementById("btn_add_curso").Click();
            driver.FindElementById("NomeCurso").SendKeys("Curso teste");
            driver.FindElementById("Vagas").SendKeys("2");
            driver.FindElementById("btn_submit_curso").Click();

            driver.FindElementById("btn_next").Click();

            driver.FindElementByCssSelector("input[name=mobilidade]").Click();

            driver.FindElementById("btn_submit").Click();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
