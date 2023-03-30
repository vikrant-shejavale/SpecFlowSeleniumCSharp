using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowCsharpSelenium.StepDefinitions
{
    [Binding]
    public sealed class YourCartStepDefinition :BaseStepDefinition
    {
        private By grandTotal = By.XPath("//tr[@class='grand totals']//span[@class='price']");
        private By proceedToCheckout = By.XPath("//button[@title='Proceed to Checkout']");
        private String editItemQty = "//input[@title='Quantity'][{0}]";
        private String grandTotalprice;

        [When(@"I change quatity for (.*)(st|nd|rd|th) item in cart to (.*)")]
        public void WhenIChangeQuatityForStItemInCartTo(int item, String suffix, int qty)
        {
            grandTotalprice = WaitForElementToVisible(grandTotal, 10).Text;
            driver.FindElement(By.XPath(String.Format(editItemQty, item))).Clear();
            driver.FindElement(By.XPath(String.Format(editItemQty, item))).SendKeys(qty.ToString());
            
        }
        [Then(@"I verify total price is updated after changing quantity")]
        public void ThenIVerifyTotalPriceIsUpdatedAfterChangingQuantity()
        {
            String priceAfterQtyChnage = WaitForElementToVisible(grandTotal, 6).Text;
            Assert.AreNotEqual(priceAfterQtyChnage, grandTotalprice);
            Thread.Sleep(5000);
        }
        
        [Then(@"I click on Proceed to Checkout button")]
        public void ThenIClickOnProceedToCheckoutButton()
        {
            WaitForElementToVisible(proceedToCheckout, 10).Click();
        }


    }
}
