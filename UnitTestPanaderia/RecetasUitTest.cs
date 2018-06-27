using NUnit.Framework;
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
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Recetas");

            //presiona click en botón crear
            driver.FindElement(By.Id("crear")).Click(); 

            //Genero id Aleatoreo
            Random rnd = new Random();
            var idBusca =  Convert.ToString(rnd.Next(0, 100)); 

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito"+ idBusca);
            driver.FindElement(By.Id("valor_venta")).SendKeys("12");

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
        public void EditarRecetaTest()
        {
            // Variables para busqueda 
            string idBusca = "1";

            //Modifico nombre de receta
            string nombreModifico = "chao" + DateTime.Today;

            //Id para comprobar nombre de variables
            string buscaNombre = "nombreReceta";

            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Recetas");

            //Presiono click 
            driver.FindElement(By.Id(idBusca)).Click();

            //Limpio formulario
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("valor_venta")).Clear();

            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);
            driver.FindElement(By.Id("valor_venta")).SendKeys("14");
            driver.FindElement(By.Id("estado")).Click();
            
            //Presiono boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaNombre));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);

            driver.Close();
            driver.Quit();
        }

        [Test]
        public void AgregaInsumoTest()
        {
            //string idBusca = "1";
            //string buscaTexto = "insumo " + idBusca;
            //string buscaNombre = "nombreReceta";

            //driver.Navigate().GoToUrl(url + "/Home/Login");
            //driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            //driver.FindElement(By.Id("clave")).SendKeys("1234");
            //driver.FindElement(By.ClassName("btn-block")).Click();

            //driver.Navigate().GoToUrl(url + "/Recetas");

            //driver.FindElement(By.Id(idBusca)).Click();
            //driver.FindElement(By.Id("nombre")).Clear();
            //driver.FindElement(By.Id("valor_venta")).Clear();
            //driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);
            //driver.FindElement(By.Id("valor_venta")).SendKeys("14");
            //driver.FindElement(By.Id("estado")).Click();
            //driver.FindElement(By.ClassName("btn")).Click();

            //IWebElement element = driver.FindElement(By.Id(buscaNombre));
            //String textoAtributo = element.GetAttribute("name");
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);

            //driver.Close();
            //driver.Quit();
        }
    }
}
