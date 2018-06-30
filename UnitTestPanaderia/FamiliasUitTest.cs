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
        public void CreaFamilias()
        {
            LoginReceta();
            driver.Navigate().GoToUrl(url + "/familias/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("nombre")).SendKeys("Enojon " + idBusca);
            IWebElement element = driver.FindElement(By.Id("lineaId"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue("2");
            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

        }

        public int CreaFamilia()
        {

            driver.Navigate().GoToUrl(url + "/familias/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 20);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("nombre")).SendKeys("Avellanas" + idBusca);
            driver.FindElement(By.Id("lineaId")).SendKeys("2");

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            return idBuscaNumero;
        }

        [Test]
        public void EditarRecetaTest()
        {
            LoginReceta();
            string idBusca = Convert.ToString(CreaFamilia());
            string nombreModifico = "Modifico " + DateTime.Today + idBusca;

            //Busca url 
            driver.Navigate().GoToUrl(url + "/familias/Edit/" + idBusca);

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
            string idBusca = Convert.ToString(CreaFamilia());

            driver.Navigate().GoToUrl(url + "/familia/Index/" + idBusca);

            //click  boton eliminar
            driver.FindElement(By.Id("Delete " + idBusca)).Click();

            var element = driver.FindElement(By.Id("table"));
            List<IWebElement> elementoTabla = new List<IWebElement>(element.FindElements(By.TagName("tr")));
            string insumo = elementoTabla.Count == 1 ? "" : "No es correcto";

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", insumo);

        }

        [Test]
        public void ListarTest()
        {
            LoginReceta();
            driver.Navigate().GoToUrl(url + "/familias/Index");
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
