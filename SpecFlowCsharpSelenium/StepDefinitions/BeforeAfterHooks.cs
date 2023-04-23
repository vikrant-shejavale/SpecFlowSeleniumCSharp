using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using BoDi;

namespace SpecFlowCsharpSelenium.StepDefinitions
{
    [Binding]
    public sealed class BeforeAfterHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        
        public IObjectContainer _container;
        public BeforeAfterHooks()
        {
            _container = new ObjectContainer();
        }
        public BeforeAfterHooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            //TODO: implement logic that has to run before executing each scenario
        }

        [BeforeScenario(Order = 1)]
        public void setUpWebDriver()
        {
            IWebDriver driver = new ChromeDriver("C:\\Repositories\\drivers\\chromedriver.exe");
            driver.Url = "https://eu.wahoofitness.com/";
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _container.RegisterInstanceAs<IWebDriver>(driver);
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver=_container.Resolve<IWebDriver>();
            driver.Close();
        }
    }
}