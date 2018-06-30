using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestPanaderia
{
    [TestClass]
    public class RecetasUnitTest
    {

        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();
        int recetaId = 0;
        string insumoNombre = "0";
        int login = 0;
        public void LoginReceta()
        {
            // Ingreso Login
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();
            login = 1;
        }
 
        [Test]
        public void RegistroRecetaTest()
        {
            if(login == 0) LoginReceta();

            driver.Navigate().GoToUrl(url + "/Recetas/Create");

            //Genero id Aleatoreo
            Random rnd = new Random();
            int idBuscaNumero = rnd.Next(0, 1000);
            recetaId = idBuscaNumero;

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
        public void EditarRecetaTest()
        {
            if (login == 0) LoginReceta();
            if (recetaId == 0) RegistroRecetaTest();
            string idBusca = Convert.ToString(recetaId);
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/Recetas/Edit/"+ idBusca);

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
        public void RecetaNoActivaTest()
        {
            if (login == 0) LoginReceta();
            // Variables para busqueda 
            if (recetaId == 0) RegistroRecetaTest();
            string idBusca = Convert.ToString(recetaId);
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
            if (login == 0) LoginReceta();
            if (recetaId == 0) RegistroRecetaTest();
            string idBusca = Convert.ToString(recetaId);
            string buscaTexto = "insumo " + idBusca;
            string seleccionInsumo = "Sal";
            insumoNombre = seleccionInsumo;

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
            if (login == 0) LoginReceta();
            if (recetaId == 0) RegistroRecetaTest();
            string idBusca = Convert.ToString(recetaId);
            if (insumoNombre == "0") RegistraInsumoTest();
            string seleccionInsumo = insumoNombre;

            driver.Navigate().GoToUrl(url + "/DetalleReceta/Index/" + idBusca);
            var elementInicia = driver.FindElement(By.Id("tablaInsumos"));
            List<IWebElement> elementoTabla = new List<IWebElement>(elementInicia.FindElements(By.TagName("tr")));
            int cantidadInicia= elementoTabla.Count(); 

            //click  boton eliminar
            driver.FindElement(By.Id(seleccionInsumo + " " + idBusca)).Click();

            var element = driver.FindElement(By.Id("tablaInsumos"));
            List<IWebElement> elementoTablaFin = new List<IWebElement>(element.FindElements(By.TagName("tr")));
            int cantidadFinal = elementoTablaFin.Count();
        
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(cantidadInicia -1, cantidadFinal);

            }
        
    }
}
