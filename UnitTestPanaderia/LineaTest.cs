using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPanaderia
{
    [TestClass]
    public class LineaTest
    {


        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        [Test]
        public void RegistroLinea()
        {
            driver.Navigate().GoToUrl(url + "/Home/Login");

            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");

            // driver.FindElement(By.XPath("/html/body/form/div/div/div/div[2]/div[4]/div/input")).Click();
            driver.FindElement(By.ClassName("btn-block")).Click();

            driver.Navigate().GoToUrl(url + "/lineas");
            driver.FindElement(By.Id("crear")).Click();
            driver.FindElement(By.Id("nombre")).SendKeys("PruebaLinea");            
            driver.FindElement(By.Id("guardar")).Click();
            IWebElement element = driver.FindElement(By.Id("crear"));


            String textoAtributo = element.GetAttribute("Id");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("crear", textoAtributo);



            driver.Close();

            driver.Quit();
        }




    }
}
