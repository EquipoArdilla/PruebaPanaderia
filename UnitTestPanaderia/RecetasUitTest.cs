﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPanaderia
{
    [TestClass]
    public class RecetasUnitTest
    {
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        [Test]
        public void RegistroRecetaTest()
        {
            driver.Navigate().GoToUrl(url + "/Home/Login");

            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
         
            driver.FindElement(By.XPath("/html/body/form/div/div/div/div[2]/div[4]/div/input")).Click();
            driver.Navigate().GoToUrl(url + "/Recetas");
            driver.FindElement(By.Id("crear")).Click();
            //IWebElement element = driver.FindElement(By.XPath("/html/body/div[2]/header/div/a/img"));
            //IWebElement element = driver.FindElement(By.Id("Id"));
            driver.FindElement(By.Id("Id")).SendKeys("210");
            driver.FindElement(By.Id("nombre")).SendKeys("chao");
            driver.FindElement(By.Id("valor_venta")).SendKeys("12");
            driver.FindElement(By.ClassName("btn")).Click();
            IWebElement element = driver.FindElement(By.Id("valida"));


            String textoAtributo = element.GetAttribute("value");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("si", textoAtributo);



            driver.Close();

            driver.Quit();
        }



    }
}
