﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestPanaderia
{
    [TestClass]
    public class ProveedorsUnitTest
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
        public void CreaProveedors() {
            LoginReceta();

            driver.Navigate().GoToUrl(url + "/proveedors/Create");

            Random rnd = new Random();
            int aletetoreo = rnd.Next(0, 1000);
            //Ingreso registros
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito" + aletetoreo);


            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            IWebElement element = driver.FindElement(By.Id("Pan Malito" + aletetoreo));
            String textoAtributo = element.GetAttribute("id");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Pan Malito" + aletetoreo, textoAtributo);


        }

        public void EditarproveedorTest()
        {
            LoginReceta();
            
            string nombreModifico = "Modifico " + DateTime.Today ;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Proveedors/Edit/" );

            //Limpio formulario
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("precio")).Clear();

            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);
            driver.FindElement(By.Id("precio")).SendKeys("14");
           

            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id("Pan Malito"));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);
        }
    }
}