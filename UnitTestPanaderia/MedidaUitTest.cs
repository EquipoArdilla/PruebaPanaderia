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
    public class MedidaTest
    {
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        public void LoginMedida()
        {
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

        }
        public int CreaMedida()
        {

            driver.Navigate().GoToUrl(url + "/medida/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan");

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            return idBuscaNumero;
        }
        [Test]
        public void RegistroMedidaTest()
        {
            // Ingreso Login
            LoginMedida();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/medida");

            //presiona click en botón crear
            driver.FindElement(By.Id("crear")).Click(); 

            //Genero id Aleatoreo
            Random rnd = new Random();
            var idBusca =  Convert.ToString(rnd.Next(0, 100)); 

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Crea_Prueba_"+ DateTime.Today);
           
            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("id");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(idBusca, textoAtributo);
           
            // Cierro Driver y ventana 
            driver.Close();
            driver.Quit();
        }
       
        [Test]
        public void EditaMedidaTest()
        {
            LoginMedida();
            string idBusca = Convert.ToString(CreaMedida());
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/medida/Edit/" + idBusca);

            //Limpio formulario
            driver.FindElement(By.Id("nombre")).Clear();

            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);

            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);
            driver.Close();
            driver.Quit();
        }
       
        [Test]
        public void DeleteMedidaTest()
        {
            LoginMedida();
            string idBusca = Convert.ToString(CreaMedida());
            string nombreElimino = "";

            //Busca url 
            driver.Navigate().GoToUrl(url + "/medida/Delete/" + idBusca);
            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            //IWebElement element = driver.FindElement(By.Id(idBusca));
            //String textoAtributo = element.GetAttribute("name");

            ////comparo elementos
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreElimino, textoAtributo);

        }
    }
}
