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
    public class ProductoesUnitTest
    {
         
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        public void LoginProducto()
        {
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();

        }
        public int CreaProducto() {

            driver.Navigate().GoToUrl(url + "/Productoes/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito" + idBusca);
            driver.FindElement(By.Id("formato")).SendKeys("12");
            driver.FindElement(By.Id("familiaId")).SendKeys("12");
            driver.FindElement(By.Id("usuarioId")).SendKeys("12");
            driver.FindElement(By.Id("medidaId")).SendKeys("12");

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            return idBuscaNumero;
        }
        public string CreaProducto( string idBusca)
        {
            string buscaTexto = "Producto " + idBusca;
            string seleccionInsumo = "Sal";

            driver.Navigate().GoToUrl(url + "/DetalleProducto/Index/" + idBusca);

            IWebElement element = driver.FindElement(By.Id("selectProducto"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(seleccionInsumo);

            driver.FindElement(By.Id("txtCantidad")).SendKeys("11");
            driver.FindElement(By.ClassName("btn")).Click();
            return seleccionInsumo;
        }

        [Test]
        public void RegistroProductoTest()
        {
            int idBuscaNumero = 0;
            // Ingreso Login
            LoginProducto();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Productoes/Create");

            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca =Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito" + idBusca);
            driver.FindElement(By.Id("formato")).SendKeys("12");
            driver.FindElement(By.Id("familiaId")).SendKeys("12");
            driver.FindElement(By.Id("usuarioId")).SendKeys("12");
            driver.FindElement(By.Id("medidaId")).SendKeys("12");

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("id");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(idBusca, textoAtributo);
        }

        [Test]
        public void EditarProductoTest()
        {
            LoginProducto();
            string idBusca = Convert.ToString(CreaProducto());
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Productoes/Edit/"+ idBusca);

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
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);
        }

        [Test]
        public void ProductoNoActivaTest()
        {
            LoginProducto();
            // Variables para busqueda 
            String idBusca = Convert.ToString(CreaProducto());
            String buscaNombre = "estado "+ idBusca;
            String textoAtributo = "Busco";

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Productoes/Edit/" + idBusca);
            
            //Modifico Campos
            driver.FindElement(By.Id("estado")).Click();
            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaNombre));
            //textoAtributo = element.GetAttribute("checked");
           
            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual("true", textoAtributo);
        }

        [Test]
        public void RegistraInsumoTest()
        {
            LoginProducto();
            string idBusca = Convert.ToString(CreaProducto());
            string buscaTexto = "insumo " + idBusca;
            string seleccionInsumo = "Sal";

            driver.Navigate().GoToUrl(url + "productoes/Details" + idBusca);
            
            IWebElement element = driver.FindElement(By.Id("selectProducto"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(seleccionInsumo);

            driver.FindElement(By.Id("txtCantidad")).SendKeys("11");
            driver.FindElement(By.ClassName("btn")).Click();

            IWebElement elementVerfica = driver.FindElement(By.Id(seleccionInsumo));
            String textoAtributo = elementVerfica.GetAttribute("Id");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(seleccionInsumo, textoAtributo);
        }

        [Test]
        public void EliminaProductoTest()
        {
            LoginProducto();
            string idBusca = Convert.ToString(CreaProducto());
            string seleccionProducto = CreaProducto(idBusca);
            string Producto = "";

            driver.Navigate().GoToUrl(url + "/DetalleProducto/Index/" + idBusca);

            //click  boton eliminar
            driver.FindElement(By.Id(seleccionProducto + " " + idBusca)).Click();

            var element = driver.FindElement(By.Id("tablaProductos"));
            List<IWebElement> elementoTabla = new List<IWebElement>(element.FindElements(By.TagName("tr")));
            insumo = elementoTabla.Count == 1 ? "" : "No es correcto"; 
        
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", insumo);

            }
        
    }
}
