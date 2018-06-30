using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPanaderia
{
    [TestClass]
    public class MedidaTest
    {
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        [Test]
        public void RegistroMedidaTest()
        {
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

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
        public void EditarMedidaTest()
        {
            // Variables para busqueda 
            //string idBusca = "1";
            string nombreBusca = "Crea_Prueba_" + DateTime.Today;
            //Modifico nombre de receta
            string nombreModifico = "Prueba_Actualizada" + DateTime.Today;

            //Id para comprobar nombre de variables
            string buscaNombre = "Prueba_Actualizada" + DateTime.Today;

            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/medida");

            //Presiono click 
            //driver.FindElement(By.Id(idBusca)).Click();
            //Presiono click 
            driver.FindElement(By.Id(nombreBusca)).Click();

            //Limpio formulario
            driver.FindElement(By.Id("nombre")).Clear();
            
            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);

            //Presiono boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para obtener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaNombre));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);

            driver.Close();
            driver.Quit();
        }

        [Test]
        public void EliminaMedidaTest()
        {
            //elimino Medida    
            string seleccionMedida = "Prueba_Actualizada" + DateTime.Today;

            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/medida");
                       
            //click  boton eliminar
            driver.FindElement(By.Id(seleccionMedida)).Click();

            //Presiono boton
            //driver.FindElement(By.ClassName("btnEliminar")).Click();

            //Cierro Driver y ventana 
            //driver.Close();
            //driver.Quit();
        }
    }
}
