using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPanaderia
{
    [TestClass]
    public class LoginUitTest
    {
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        [Test]
        public void LoginTest()
        {
            driver.Navigate().GoToUrl(url + "/Home/Login");

            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");

            driver.FindElement(By.XPath("/html/body/form/div/div/div/div[2]/div[4]/div/input")).Click();


            IWebElement element = driver.FindElement(By.XPath("/html/body/div[2]/header/div/a/img"));

            String src_imagen = element.GetAttribute("src");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(url + "/Img/ardilla-meme-panadero.jpg", src_imagen);



            driver.Close();

            driver.Quit();
        }



    }
}
