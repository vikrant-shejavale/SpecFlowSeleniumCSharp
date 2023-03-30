using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowCsharpSelenium.StepDefinitions
{
    public class BaseStepDefinition
    {
        protected static IWebDriver driver;
        [BeforeScenario]
        public void setUpWebDriver()
        {
            driver = new ChromeDriver("C:\\Repositories\\drivers\\chromedriver.exe");
            driver.Url = "https://eu.wahoofitness.com/";
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
        [AfterScenario]
        public static void tearDownWebDriver()
        {
            driver.Close();
        }
        public IWebElement WaitForElementToVisible(By locator, int seconds)
        {
            try { 
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(seconds);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(200);
            return fluentWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public IWebElement GetElementInFocus(IWebElement e)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", e);
            return e;
        }
        

    }
}
