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
        public void AgregarAux()
        {
            Random num = new Random();
            int numero = num.Next(0, 100);
            string probar = "MedidaPrueba" + numero.ToString();
            driver.Navigate().GoToUrl(url + "/medida/Create");
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("nombre")).SendKeys(probar);
            driver.FindElement(By.Id("guardar")).Click();

        }
        public int CreaMedida()
        {
            driver.Navigate().GoToUrl(url + "/medida/Create");
            int idBuscaNumero = 0;
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);
            String idBusca = Convert.ToString(idBuscaNumero);
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan");
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
            
            //Busca url 
            driver.Navigate().GoToUrl(url + "/medida/Delete/" + idBusca);

            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            //IWebElement element = driver.FindElement(By.Id(idBusca));
            //String textoAtributo = element.GetAttribute("Id");

            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", textoAtributo);
            driver.Close();
            driver.Quit();

        }
        //[Test]
        //public void EliminarTestMedida()
        //{
        //    LoginMedida();
        //    driver.Navigate().GoToUrl(url + "/medida");
        //    string idBusca = Convert.ToString(CreaMedida());
        //    AgregarAux();
        //    var elemTable = driver.FindElement(By.Id("tablaLineas"));

        //    List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.ClassName("colum1")));

        //    int filas = Convert.ToInt16(lstTrElem.Count.ToString());
        //    int idurl = Convert.ToInt16(filas) + 1; 
        //    string value = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[1]")).Text;

        //    //driver.Navigate().GoToUrl(url + "/lineas/Edit/"+idurl);          
        //    driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[3]/a[3]")).Click();

        //    driver.FindElement(By.ClassName("btn")).Click();

        //    var elemTable2 = driver.FindElement(By.Id("tablaLineas"));

        //    List<IWebElement> lstTrElem2 = new List<IWebElement>(elemTable2.FindElements(By.TagName("tr")));

        //    foreach (var elemTr2 in lstTrElem2)
        //    {
        //        Console.WriteLine(elemTr2.Text);
        //        List<IWebElement> lstTdElem2 = new List<IWebElement>(elemTr2.FindElements(By.ClassName("colum1")));
        //        if (lstTdElem2.Count > 0)
        //        {
        //            foreach (var elemTd in lstTdElem2)
        //            {

        //                if (elemTd.Text.Equals(value))
        //                {
        //                    NUnit.Framework.Assert.AreEqual(elemTd.Text, value);

        //                }
        //                else
        //                {
        //                    NUnit.Framework.Assert.AreNotEqual(elemTd.Text, value);
        //                    Console.WriteLine(idurl);
        //                }
        //            }

        //        }

        //    }
        //    //driver.Close();
        //    //driver.Quit();
        //}
        [Test]
        public void ListaMedidaTest()
        {
            LoginMedida();
            driver.Navigate().GoToUrl(url + "/medida/Index");
            
            var elemTable = driver.FindElement(By.Id("tablaMedida"));

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

            //driver.Close();

            //driver.Quit();
        }
    }
}
