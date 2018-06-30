using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestPanaderia
{
    [TestClass]
    public class FamiliasUnitTest
    {
         
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        public void LoginReceta()
        {
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

        }
        [Test]
        public void CreaFamilias()
        {
            LoginReceta();
            driver.Navigate().GoToUrl(url + "/familias/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("nombre")).SendKeys("Enojon " + idBusca);
            IWebElement element = driver.FindElement(By.Id("lineaId"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue("2");
            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

        }
        
    }
}
