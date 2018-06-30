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
    public class ProveedorsUnitTest
    {
         
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();
        int IdProveedor = 0;
        string nombreProveedor = "0";
        int idBuscaNumero = 0;

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
            IdProveedor = idBuscaNumero;
            //Ingreso registros
            driver.FindElement(By.Id("nombre")).SendKeys("Pan Malito" + aletetoreo);


            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            IWebElement element = driver.FindElement(By.Id("Pan Malito" + aletetoreo));
            String textoAtributo = element.GetAttribute("id");

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Pan Malito" + aletetoreo, textoAtributo);


        }

        [Test]
        public void EditarproveedorTest()
        {
            LoginReceta();
            string idBusca = Convert.ToString(IdProveedor);
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/proveedors/Edit/" + idBusca);

            //Limpio formulario
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("lineaId")).Clear();

            //Modifico Campos
            driver.FindElement(By.Id("nombre")).SendKeys(nombreModifico);
            driver.FindElement(By.Id("lineaId")).SendKeys("9");

            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(idBusca));
            String textoAtributo = element.GetAttribute("name");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(nombreModifico, textoAtributo);
        }


        [Test]
        public void EliminaFamiliaTest()
        {
            LoginReceta();
            if (nombreProveedor == "0") CreaProveedors();
            string idBusca = nombreProveedor;


            driver.Navigate().GoToUrl(url + "/proveedors/");
            var element = driver.FindElement(By.ClassName("table"));
            List<IWebElement> elementoTabla = new List<IWebElement>(element.FindElements(By.TagName("tr")));
            int cantidadInicia = elementoTabla.Count();

            driver.FindElement(By.Id(nombreProveedor)).Click();

            //click  boton eliminar
            driver.FindElement(By.Id("elimina")).Click();
            var element2 = driver.FindElement(By.ClassName("table"));
            List<IWebElement> elementoElimina = new List<IWebElement>(element2.FindElements(By.TagName("tr")));
            int cantidadFinal = elementoElimina.Count();
            string insumo = elementoTabla.Count == 1 ? "" : "No es correcto";

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(cantidadFinal, cantidadInicia);

        }

        [Test]
        public void ListarTest()
        {
            LoginReceta();
            driver.Navigate().GoToUrl(url + "/proveedors/Index");
            var elemTable = driver.FindElement(By.Id("table"));

            NUnit.Framework.Assert.IsNotEmpty(elemTable.Text);
            Console.WriteLine(elemTable.Text);

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));

            foreach (var elemTr in lstTrElem)
            {
                List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                if (lstTdElem.Count > 0)
                {
                    NUnit.Framework.Assert.NotZero(lstTdElem.Count);
                    Console.WriteLine("Lineas Encontradas " + lstTdElem[0].Text);
                }
            }


        }
    }
}
