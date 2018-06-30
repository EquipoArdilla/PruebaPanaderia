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
        public int CreaFamilias() {
            LoginReceta();
            driver.Navigate().GoToUrl(url + "/Familias/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Avellanas" + idBusca);
            driver.FindElement(By.Id("lineaId")).SendKeys("2");
            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            return idBuscaNumero;
        }
        public string CreaInsumo( string idBusca)
        {
            string buscaTexto = "insumo " + idBusca;
            string seleccionInsumo = "Sal";

            driver.Navigate().GoToUrl(url + "/DetalleReceta/Index/" + idBusca);

            IWebElement element = driver.FindElement(By.Id("selectProducto"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(seleccionInsumo);

            driver.FindElement(By.Id("txtCantidad")).SendKeys("11");
            driver.FindElement(By.ClassName("btn")).Click();
            return seleccionInsumo;
        }

        [Test]
        public void RegistroRecetaTest()
        {
            int idBuscaNumero = 0;
            // Ingreso Login
            LoginReceta();

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Recetas/Create");

            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

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
        }

        [Test]
        public void EditarFamiliasTest()
        {
            LoginReceta();
            string idBusca = Convert.ToString(CreaFamilias());
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Familias/Edit/"+ idBusca);

            //Limpio formulario
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("lineaId")).Clear();

            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);
            driver.FindElement(By.Id("lineaId")).SendKeys("2");
            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);
        }
        /*
        [Test]
        public void RecetaNoActivaTest()
        {
            LoginReceta();
            // Variables para busqueda 
            String idBusca = Convert.ToString(CreaReceta());
            String buscaNombre = "estado "+ idBusca;
            String textoAtributo = "Busco";

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Recetas/Edit/" + idBusca);
            
            //Modifico Campos
            driver.FindElement(By.Id("estado")).Click();
            
            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaNombre));
            textoAtributo = element.GetAttribute("checked");
           
            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual("true", textoAtributo);
        }

        [Test]
        public void RegistraInsumoTest()
        {
            LoginReceta();
            string idBusca = Convert.ToString(CreaReceta());
            string buscaTexto = "insumo " + idBusca;
            string seleccionInsumo = "Sal";

            driver.Navigate().GoToUrl(url + "/DetalleReceta/Index/"+ idBusca);
            
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
        public void EliminaInsumoTest()
        {
            LoginReceta();
            string idBusca = Convert.ToString(CreaReceta());
            string seleccionInsumo = CreaInsumo(idBusca);
            string insumo = "";

            driver.Navigate().GoToUrl(url + "/DetalleReceta/Index/" + idBusca);

            //click  boton eliminar
            driver.FindElement(By.Id(seleccionInsumo + " " + idBusca)).Click();

            var element = driver.FindElement(By.Id("tablaInsumos"));
            List<IWebElement> elementoTabla = new List<IWebElement>(element.FindElements(By.TagName("tr")));
            insumo = elementoTabla.Count == 1 ? "" : "No es correcto"; 
        
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", insumo);

            }*/
        
    }
}
