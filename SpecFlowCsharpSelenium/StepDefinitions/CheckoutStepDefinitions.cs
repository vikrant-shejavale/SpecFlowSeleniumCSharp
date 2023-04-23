using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SpecFlowCsharpSelenium.StepDefinitions
{
    [Binding]
    public sealed class CheckoutStepDefinitions : BaseStepDefinition
    {
        
        private By checkoutPageTitle = By.XPath("//h1[text()='Checkout']");
        private By payNowBtn = By.XPath("//button[@title=\"Pay Now\"]/span[text()='Pay Now']");
        private By errorMsg = By.Id("customer-email-error");
        private By ShippingPrice = By.XPath("//span[@data-th=\"Shipping\"]");
        private String shippingMethodLocator = "//td[contains(text(),'{0}')]/preceding-sibling::td/input";
        private String shippingPriceValue;

        public CheckoutStepDefinitions(IWebDriver driver): base(driver)
        {

        }
        
        [Then(@"I navigate to Checkout page")]
        public void ThenINavigateToCheckoutPage()
        {
           Assert.True( WaitForElementToVisible(checkoutPageTitle, 7).Text.Equals("Checkout"));
        }

        [Then(@"I click PAY NOW button")]
        public void ThenIClickPAYNOWButton()
        {
            WaitForElementToVisible(payNowBtn, 7).Click();
        }

        [Then(@"I verify Error message dispalyed ""([^""]*)""")]
        public void ThenIVerifyErrorMessageDispalyed(string errorTxt)
        {
            Assert.True(WaitForElementToVisible(errorMsg, 7).Text.Equals(errorTxt));
        }

        [Then(@"I change shipping method to ""([^""]*)""")]
        public void ThenIChangeShippingMethodTo(string shippingMethod)
        {
            shippingPriceValue = WaitForElementToVisible(ShippingPrice, 7).Text;
            String locator= String.Format(shippingMethodLocator, shippingMethod);
            WaitForElementToVisible(By.XPath(locator), 7).Click();
        }
        
        [Then(@"I verify shipping price is updated")]
        public void ThenIVerifyShippingPriceIsUpdated()
        {
            Assert.AreNotEqual(WaitForElementToVisible(ShippingPrice, 7).Text,shippingPriceValue);
        }





    }
}