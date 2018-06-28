using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            int idBuscaNumero = 0;
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

            try
            {
                // verifico si exite en registro 1
                IWebElement elementValido = driver.FindElement(By.Id(Convert.ToString(1)));
                idBuscaNumero = 1;
            }
            catch (Exception)
            {
            
                idBuscaNumero = rnd.Next(0, 100);
            }

            try
            {
                // verifico que id no este en db
                IWebElement elementValido = driver.FindElement(By.Id(Convert.ToString(idBuscaNumero)));
                idBuscaNumero = idBuscaNumero + rnd.Next(idBuscaNumero, 100);
            }
            catch (Exception)
            {
                Console.WriteLine("No existe producto");
            }

            //convierto id para text
            String idBusca =Convert.ToString(idBuscaNumero);

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
            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaNombre));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);

            // Cierro Driver y ventana 
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void RecetaNoActivaTest()
        {
            // Variables para busqueda 
            string idBusca = "1";

            //Id para comprobar nombre de variables
            string buscaNombre = "estado "+ idBusca;

            String textoAtributo = "Busco";

            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Recetas");


            try
            {
                IWebElement elementActivo = driver.FindElement(By.Id(buscaNombre));
                textoAtributo = elementActivo.GetAttribute("checked");
                if (textoAtributo == "true")
                {
                    //Presiono click 
                    driver.FindElement(By.Id(idBusca)).Click();
                }
                //Modifico Campos
                driver.FindElement(By.Id("estado")).Click();
                //click boton
                driver.FindElement(By.ClassName("btn")).Click();

                //Busco elemento id para optener atributos y verificar  que el id existe 
                IWebElement element = driver.FindElement(By.Id(buscaNombre));
                textoAtributo = element.GetAttribute("checked");
            }
            catch (Exception)
            {
                textoAtributo = "No exite id " + idBusca;
            }

                //comparo elementos
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual("true", textoAtributo);

            // Cierro Driver y ventana 
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void RegistraInsumoTest()
        {
            // Variables para busqueda 
            string idBusca = "1";
            string buscaTexto = "insumo " + idBusca;
            string seleccionInsumo = "Sal";

            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/DetalleReceta/Index/"+ idBusca);

            //atrapo error al no encontrar elemento
            try { 
            
                //Busco elemento tag para optener atributos
                IWebElement elementValido = driver.FindElement(By.Id(seleccionInsumo));

                //click  boton eliminar
                driver.FindElement(By.Id(seleccionInsumo + " " + idBusca)).Click();
            }
            catch (Exception)
            {

                //envio a consola la captura
                Console.WriteLine("No existe producto");
            }
            //ejemplo de buscar select en combo 
            // genero mi elemento web con mi etiqueta select.
            IWebElement seleccion = driver.FindElement(By.TagName("select"));

            //listo mis elementos, se debe agregar: using System.Collections.Generic;
            IList<IWebElement> selecciones = seleccion.FindElements(By.TagName("option"));

            //listo y busco el value que quiero seleccionar
            foreach (IWebElement option in selecciones)
            {
                Console.WriteLine(seleccionInsumo + option.GetAttribute("value"));
                option.Click();
            }

            //Agrego texto
            driver.FindElement(By.Id("txtCantidad")).SendKeys("11");

            //click  boton
            driver.FindElement(By.ClassName("btn")).Click();

            //verificar  que el insumo existe 
            IWebElement element = driver.FindElement(By.Id(seleccionInsumo));
            String textoAtributo = element.GetAttribute("Id");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(seleccionInsumo, textoAtributo);

            //Cierro Driver y ventana 
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void EliminaInsumoTest()
        {
            // Variables para busqueda 
            string idBusca = "1";
            string buscaTexto = "insumo " + idBusca;
            string seleccionInsumo = "Sal";
            String textoAtributo = "Nulo";

            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/DetalleReceta/Index/" + idBusca);

            //atrapo error al no encontrar elemento
            try
            {

                //Busco elemento tag para optener atributos
                IWebElement elementValido = driver.FindElement(By.Id(seleccionInsumo));
            }
            catch (Exception)
            {

                //ejemplo de buscar select en combo 
                // genero mi elemento web con mi etiqueta select.
                IWebElement seleccion = driver.FindElement(By.TagName("select"));

                //listo mis elementos, se debe agregar: using System.Collections.Generic;
                IList<IWebElement> selecciones = seleccion.FindElements(By.TagName("option"));

                //listo y busco el value que quiero seleccionar
                foreach (IWebElement option in selecciones)
                {
                    Console.WriteLine(seleccionInsumo + option.GetAttribute("value"));
                    option.Click();
                }

                //Agrego texto
                driver.FindElement(By.Id("txtCantidad")).SendKeys("11");

                //click  boton
                driver.FindElement(By.ClassName("btn")).Click();
                //Busco elemento tag para optener atributos
                IWebElement elementElimino = driver.FindElement(By.Id(seleccionInsumo));
            }

            //click  boton eliminar
            driver.FindElement(By.Id(seleccionInsumo + " " + idBusca)).Click();

            //verificar  que el insumo existe 
            try
            {
                
                IWebElement element = driver.FindElement(By.Id(seleccionInsumo));
                 textoAtributo = element.GetAttribute("Id");
            }
            catch (Exception)
            {
                //asigno valor para comparar
                textoAtributo = "";
            }
                //comparo elementos
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(seleccionInsumo, textoAtributo);

                //Cierro Driver y ventana 
                driver.Close();
                driver.Quit();
            }
    }
}
