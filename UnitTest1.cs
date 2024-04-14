namespace AutotestForWebform;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[TestFixture]
public class Tests
{
    [SetUp]
    public void Setup()
    {
        
        
    }
    

    [Test]
    public void RegistrationAndAuthorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--no-default-browser-check");
        ChromeDriver driverForChrome = new ChromeDriver(options);
        driverForChrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000/signup");

        int counter = 0;
        string email = "123+123@mail.ru";
        string pass = "qwerty";

        driverForChrome.FindElement(By.Name("email")).SendKeys(email);
        driverForChrome.FindElement(By.Name("name")).SendKeys("Alex");
        driverForChrome.FindElement(By.Name("password")).SendKeys(pass);
        

        var confirmButton = driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/button"));
        confirmButton.Click();

        if (driverForChrome.FindElement(By.CssSelector("body > section > div.hero-body > div > div > div > div")).Enabled)
        {
            while (driverForChrome.Url != "http://localhost:5000/login")
            {
                driverForChrome.FindElement(By.Name("email")).SendKeys(counter+email);
                driverForChrome.FindElement(By.Name("name")).SendKeys("Alex");
                driverForChrome.FindElement(By.Name("password")).SendKeys(pass);
                counter++;
                driverForChrome.FindElement(By.CssSelector("body > section > div.hero-body > div > div > div > form > button")).Click();
            }
            
        }
        
        driverForChrome.FindElement(By.Name("email")).SendKeys(email);
        driverForChrome.FindElement(By.Name("password")).SendKeys(pass);
        driverForChrome.FindElement(By.CssSelector("body > section > div.hero-body > div > div > div > form > button")).Click();
        
        Assert.That(driverForChrome.Url == "http://localhost:5000/profile");
        driverForChrome.Quit();
    }

    [TestCase("test@mail", "Sam", "")]
    [TestCase("~!@#$%^&*()_+\\|[]{}'\";?/<>,.", "~!@#$%^&*()_+\\|[]{}'\";?/<>,.", "~!@#$%^&*()_+\\|[]{}'\";?/<>,.")]
    [TestCase("zxcf@"," ", "12321")]
    [TestCase("8798", " ", "4545")]
    [TestCase("","", "")]
    [TestCase("null", "null", "null")]
    [Test]
    public void NegativeInputDataForRegistration(string email, string name, string password)
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--no-default-browser-check");
        ChromeDriver driverForChrome = new ChromeDriver(options);
        driverForChrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000/signup");

        var emailInput =
            driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[1]/div/input"));
        var nameInput =
            driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[2]/div/input"));
        var passInput =
            driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[3]/div/input"));
        
        emailInput.SendKeys(email);
        nameInput.SendKeys(name);
        passInput.SendKeys(password);

        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/button")).Click();
        
        Assert.That(driverForChrome.Url != "http://localhost:5000/login");
        
        driverForChrome.Quit();

    }
    
    [TestCase("--window-size=762,1024")]
    [TestCase("--window-size=820,1180")]
    [TestCase("--window-size=1024,1366")]
    [TestCase("--window-size=1280,800")]
    [TestCase("--window-size=412, 915")]
    [TestCase("--window-size=375,667")]
    [TestCase("--window-size=1920,1080")]
    [TestCase("--window-size=800,600")]
    [Test]
    public void DisplayingTopToolbarTest(string resolution)
    {
        var options = new ChromeOptions();
        options.AddArguments(resolution, "--no-default-browser-check");
        ChromeDriver driverForChrome = new ChromeDriver(options);
        driverForChrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000");

        var homeButton =driverForChrome.FindElement(By.XPath("//*[@id=\"navbarMenuHeroA\"]/div/a[1]"));
        
        Assert.That(homeButton.Displayed, Is.True);
        
        driverForChrome.Quit();
    }

    [Test]
    public void TopToolbarButtonsTesting()
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--no-default-browser-check");
        ChromeDriver driverForChrome = new ChromeDriver(options);
        driverForChrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000");
        
        driverForChrome.FindElement(By.XPath("//*[@id=\"navbarMenuHeroA\"]/div/a[2]")).Click();
        
        Assert.That(driverForChrome.Url == "http://localhost:5000/login", "тест провалился на кнопке Login");
        
        driverForChrome.FindElement(By.XPath("//*[@id=\"navbarMenuHeroA\"]/div/a[3]")).Click();
        
        Assert.That(driverForChrome.Url == "http://localhost:5000/signup", "тест провалился на кнопке Sing Up");
        
        driverForChrome.FindElement(By.XPath("//*[@id=\"navbarMenuHeroA\"]/div/a[1]")).Click();
        
        Assert.That(driverForChrome.Url =="http://localhost:5000/", "тест провалился на кнопке Home");
    }

    [Test]
    public void LogOutButtonTest()
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--no-default-browser-check");
        ChromeDriver driverForChrome = new ChromeDriver(options);
        driverForChrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000/signup");

        var login = "111@mail.ru";
        var password = "123";

        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[1]/div/input"))
            .SendKeys(login);
        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[3]/div/input")).SendKeys(password);
        
        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/button")).Click();
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000/login");
        
        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[1]/div/input")).SendKeys(login);
        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[2]/div/input")).SendKeys(password);
        
        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/button")).Click();
        
        Assert.That(driverForChrome.Url == "http://localhost:5000/profile", "не зашел в аккаунт");
        
        driverForChrome.FindElement(By.XPath("//*[@id=\"navbarMenuHeroA\"]/div/a[3]")).Click();
        
        Assert.That(driverForChrome.FindElement(By.XPath("//*[@id=\"navbarMenuHeroA\"]/div/a[3]")).Enabled, Is.True, "не вышел из аккаута");
        
        driverForChrome.Quit();
    }
    
    [TestCase("null", "null", "null")]i
    [TestCase("~!@#$%^&*()_+\\|[]{}'\";?/<>,.", "~!@#$%^&*()_+\\|[]{}'\";?/<>,.", "~!@#$%^&*()_+\\|[]{}'\";?/<>,.")]
    [TestCase("zxcf@"," ", "12321")]
    [TestCase("8798", " ", "4545")]
    [Test]
    public void NegativeInputDataForAuthorization(string email, string name, string password)
    {
        var options = new ChromeOptions();
        options.AddArguments("--start-maximized", "--no-default-browser-check");
        ChromeDriver driverForChrome = new ChromeDriver(options);
        driverForChrome.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        
        driverForChrome.Navigate().GoToUrl("http://localhost:5000/login");

        var emailInput =
            driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[1]/div/input"));
        var passInput =
            driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/div[2]/div/input"));
        
        emailInput.SendKeys(email);
        
        passInput.SendKeys(password);

        driverForChrome.FindElement(By.XPath("/html/body/section/div[2]/div/div/div/form/button")).Click();
        
        Assert.That(driverForChrome.Url == "http://localhost:5000/login");
        
        driverForChrome.Quit();

    }
}