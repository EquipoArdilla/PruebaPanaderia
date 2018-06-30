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
    public class productoesUnitTest
    {
         
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        public void Loginproducto()
        {
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

        }
        public int Creaproducto() {

            driver.Navigate().GoToUrl(url + "/productoes/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys("343");
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito" + idBusca);
            driver.FindElement(By.Id("formato")).SendKeys("12");
            driver.FindElement(By.Id("familiaId")).SendKeys("12");
            driver.FindElement(By.Id("usuarioId")).SendKeys("12");
            driver.FindElement(By.Id("medidaId")).SendKeys("12");

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            return idBuscaNumero;
        }
        public string Creaproducto( string idBusca)
        {
            string buscaTexto = "producto " + idBusca;
            string seleccionproducto = "Sal";

            driver.Navigate().GoToUrl(url + "/Detalleproducto/Index/" + idBusca);

            IWebElement element = driver.FindElement(By.Id("selectproducto"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(seleccionproducto);

            driver.FindElement(By.Id("txtCantidad")).SendKeys("11");
            driver.FindElement(By.ClassName("btn")).Click();
            return seleccionproducto;
        }

        [Test]
        public void RegistroproductoTest()
        {
            int idBuscaNumero = 0;
            // Ingreso Login
            Loginproducto();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/productoes/Create");

            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca =Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito" + idBusca);
            driver.FindElement(By.Id("formato")).SendKeys("1");
            driver.FindElement(By.Id("familiaId")).SendKeys("1");
            driver.FindElement(By.Id("usuarioId")).SendKeys("1");
            driver.FindElement(By.Id("medidaId")).SendKeys("1");
            driver.FindElement(By.Id("precio")).SendKeys("1000");

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("id");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(idBusca, textoAtributo);
        }

        [Test]
        public void EditarproductoTest()
        {
            Loginproducto();
            string idBusca = Convert.ToString(Creaproducto());
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/productoes/Edit/"+ idBusca);

            //Limpio formulario
            driver.FindElement(By.Id("Id")).Clear();
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("formato")).Clear();
            driver.FindElement(By.Id("familiaId")).Clear();
            driver.FindElement(By.Id("usuarioId")).Clear();
            driver.FindElement(By.Id("medidaId")).Clear();
            driver.FindElement(By.Id("precio")).Clear();


            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);
            driver.FindElement(By.Id("formato")).SendKeys("14");
            driver.FindElement(By.Id("precio")).SendKeys("1200");

            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);
        }

        [Test]
        public void productoNoActivaTest()
        {
            Loginproducto();
            // Variables para busqueda 
            String idBusca = Convert.ToString(Creaproducto());
            String buscaNombre = "formato " + idBusca;
            String textoAtributo = "Busco";

            //Busca url 
            driver.Navigate().GoToUrl(url + "/productoes/Edit/" + idBusca);
            
            //Modifico Campos
            driver.FindElement(By.Id("formato")).Click();
            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaNombre));
            //textoAtributo = element.GetAttribute("checked");
           
            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual("true", textoAtributo);
        }

        [Test]
        public void RegistraproductoTest()
        {
            Loginproducto();
            string idBusca = Convert.ToString(Creaproducto());
            string buscaTexto = "producto " + idBusca;
            string seleccionproducto = "Sal";

            driver.Navigate().GoToUrl(url + "productoes/Details" + idBusca);
            
            IWebElement element = driver.FindElement(By.Id("selectproducto"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(seleccionproducto);

            driver.FindElement(By.Id("txtCantidad")).SendKeys("11");
            driver.FindElement(By.ClassName("btn")).Click();

            IWebElement elementVerfica = driver.FindElement(By.Id(seleccionproducto));
            String textoAtributo = elementVerfica.GetAttribute("Id");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(seleccionproducto, textoAtributo);
        }

        [Test]
        public void EliminaproductoTest()
        {
            Loginproducto();
            string idBusca = Convert.ToString(Creaproducto());
            string seleccionproducto = Creaproducto(idBusca);
            string producto = "";

            driver.Navigate().GoToUrl(url + "/Detalleproducto/Index/" + idBusca);

            //click  boton eliminar
            driver.FindElement(By.Id(seleccionproducto + " " + idBusca)).Click();

            var element = driver.FindElement(By.Id("tablaproductos"));
            List<IWebElement> elementoTabla = new List<IWebElement>(element.FindElements(By.TagName("tr")));
            producto = elementoTabla.Count == 1 ? "" : "No es correcto"; 
        
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", producto);

            }
        
    }
}
