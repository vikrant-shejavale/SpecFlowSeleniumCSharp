using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace SpecFlowCsharpSelenium.StepDefinitions
{
    [Binding]
    public sealed class HomePageStepDefinitions: BaseStepDefinition
    {
        
        private By productList = By.XPath("//ul[@class='products-grid']//div//img");
        private By addToCartBtn = By.Id("product-addtocart-button");
        private By selectColor = By.XPath("//Span[text()='Color']/parent::label/following-sibling::div/select");
        private By cartSideSlide = By.Id(" minicart - content - wrapper");
        private By editCart = By.XPath("//span[text()='View and Edit Cart']");
        private String homeHeadtab = "//ul/li/a/span[text()='{0}']";
        private String removeItem = "//a[@title='Remove item']/Span[text()='Remove'][{0}]";
        private String removeItemPopUp = "//button[contains(@class,'action-')]/span[text()='{0}']";
        



        [Then(@"New window with title ""([^""]*)"" opened")]
        public void ThenNewWindowWithTitleOpened(string title)
        {
            ReadOnlyCollection<String> windows=driver.WindowHandles;
            Boolean titleFound = false;
            for (int i = 0;i< windows.Count;i++)
            {
                driver.SwitchTo().Window(windows[i]);
                if (title.Equals(driver.Title))
                {
                    titleFound = true;
                }
            }
            Assert.True(titleFound);
        }
        [Given(@"I click on ""([^""]*)"" tab on home page")]
        public void GivenIClickOnTabOnHomePage(string tab)
        {
            String tabLocator=String.Format(homeHeadtab,tab);
            driver.FindElement(By.XPath(tabLocator)).Click();
        }

        [When(@"I add product on position ""(.*)"" to cart")]
        public void GivenIAddProductOnPositionToCart(int position)
        {
            IList<IWebElement> products = driver.FindElements(productList);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", products[position-1]);
            Actions action = new Actions(driver);
            action.MoveToElement(products[position-1]).Click().Build().Perform();
            IWebElement dropdown = WaitForElementToVisible(selectColor, 7);
            if (dropdown!= null && dropdown.Displayed)
            {
                SelectElement dropDown = new SelectElement(dropdown);
                dropDown.SelectByIndex(1);
            }
            WaitForElementToVisible(addToCartBtn, 5);
            driver.FindElement(addToCartBtn).Click();

        }

        [When(@"I remove product at position (.*) from cart")]
        public void WhenIRemoveProductAtPositionFromCart(int position)
        {
           
            WaitForElementToVisible(cartSideSlide, 7);
            driver.FindElement(By.XPath(String.Format(removeItem,position))).Click();
            
            WaitForElementToVisible(By.XPath(String.Format(removeItemPopUp,"OK")),5).Click();
            
        }
        
        [When(@"I click View and Edit Cart link")]
        public void WhenIClickViewAndEditCartLink()
        {
            WaitForElementToVisible(cartSideSlide, 7);
            GetElementInFocus(driver.FindElement(editCart)).Click();
        }



    }
}