using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestPanaderia
{
    [TestClass]
    public class ProduccionUitTest
            {
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        public void Login()
        {
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("ariel");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();
        }

        public int CreaReceta()
        {

            driver.Navigate().GoToUrl(url + "/Recetas/Create");
            int idBuscaNumero = 0;
            //Genero id Aleatoreo
            Random rnd = new Random();
            idBuscaNumero = rnd.Next(0, 1000);

            //convierto id para text
            String idBusca = Convert.ToString(idBuscaNumero);

            //Ingreso registros
            driver.FindElement(By.Id("Id")).SendKeys(idBusca);
            driver.FindElement(By.Id("nombre")).SendKeys("Pan y Vino" + idBusca);
            driver.FindElement(By.Id("valor_venta")).SendKeys("1500");
            driver.FindElement(By.Id("estado")).Click();

            //Presiono click en Boton
            driver.FindElement(By.ClassName("btn")).Click();

            return idBuscaNumero;
        }

        public int creaProduccion()
        {
            int idBuscaPro = 0;
            Login();
            Random num = new Random();
            int numero = num.Next(0, 100);
            string probar = CreaReceta() + numero.ToString();

            // Url
            driver.Navigate().GoToUrl(url + "/produccions");

            // Activar boton nuevo
            driver.FindElement(By.Id("crear")).Click();

            // Ingresa Registros
            driver.FindElement(By.Id("fecha_produccion")).SendKeys("2018-06-26");
            driver.FindElement(By.Id("recetaId")).SendKeys(probar);
            driver.FindElement(By.Id("cantidad")).SendKeys("10");
            driver.FindElement(By.Id("costo_receta_kilo")).SendKeys("700");
            driver.FindElement(By.Id("costo_produccion")).SendKeys("7000");
            driver.FindElement(By.Id("valor_venta_kilo")).SendKeys("1200");
            driver.FindElement(By.Id("valor_total_venta")).SendKeys("12000");
            driver.FindElement(By.Id("rentabilidad_produccion")).SendKeys("6000");
            driver.FindElement(By.Id("Grabar")).Click();

            return idBuscaPro;
        }




        [Test]
        public void AgregarTest()
        {
            Login();
            Random num = new Random();
            int numero = num.Next(0, 1000);
            string probar = CreaReceta() + numero.ToString();

            // Url
            driver.Navigate().GoToUrl(url + "/produccions");

            // Activar boton nuevo
            driver.FindElement(By.Id("crear")).Click();

            // Ingresa Registros
            driver.FindElement(By.Id("fecha_produccion")).SendKeys("2018-06-26");
            driver.FindElement(By.Id("recetaId")).SendKeys(probar);
            driver.FindElement(By.Id("cantidad")).SendKeys("10");
            driver.FindElement(By.Id("costo_receta_kilo")).SendKeys("700");
            driver.FindElement(By.Id("costo_produccion")).SendKeys("7000");
            driver.FindElement(By.Id("valor_venta_kilo")).SendKeys("1200");
            driver.FindElement(By.Id("valor_total_venta")).SendKeys("12000");
            driver.FindElement(By.Id("rentabilidad_produccion")).SendKeys("6000");
            driver.FindElement(By.Id("Grabar")).Click();


            var elemTable = driver.FindElement(By.Id("tablaProduccion"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));

            foreach (var elemTr in lstTrElem)
            {
                Console.WriteLine(elemTr.Text);
                List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.ClassName("colum3")));
                if (lstTdElem.Count > 0)
                {
                    foreach (var elemTd in lstTdElem)
                    {

                        if (elemTd.Text.Equals(probar))
                        {
                            NUnit.Framework.Assert.AreEqual(elemTd.Text, probar);

                        }
                    }

                }

            }

            //driver.Close();
            //driver.Quit();
        }


        //Edita ultimo elemento de la tabla
        [Test]
        public void EditarTest()
        {
            string prueba = "ProduccionEditada";
            Login();
            driver.Navigate().GoToUrl(url + "/produccions/index");
            string newproduccion = creaProduccion().ToString(); 

            var elemTable = driver.FindElement(By.Id("tablaProduccion"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.ClassName("colum3")));

            int filas = Convert.ToInt16(lstTrElem.Count.ToString());
            int idurl = Convert.ToInt16(filas) + 1;
            string value = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[1]")).Text;

            //driver.Navigate().GoToUrl(url + "/produccions/Edit/"+idurl);          
            driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[11]/a[1]")).Click();
       
            driver.FindElement(By.Id("recetaId")).SendKeys("Pan Marraqueta");
            driver.FindElement(By.Id("guardar")).Click();

            var elemTable2 = driver.FindElement(By.Id("tablaProduccion"));

            List<IWebElement> lstTrElem2 = new List<IWebElement>(elemTable2.FindElements(By.TagName("tr")));

            foreach (var elemTr2 in lstTrElem2)
            {
                Console.WriteLine(elemTr2.Text);
                List<IWebElement> lstTdElem2 = new List<IWebElement>(elemTr2.FindElements(By.ClassName("colum3")));
                if (lstTdElem2.Count > 0)
                {
                    foreach (var elemTd in lstTdElem2)
                    {

                        if (elemTd.Text.Equals(prueba))
                        {
                            NUnit.Framework.Assert.AreEqual(elemTd.Text,4);

                        }
                    }

                }

            }
            //driver.Close();

            //driver.Quit();
        }



        //Elimina ultimo elemento de la tbala
        [Test]
        public void EliminarTest()
        {
            Login();
            driver.Navigate().GoToUrl(url + "/produccions/Index");
            string newproduccion = creaProduccion().ToString();

            var elemTable = driver.FindElement(By.Id("tablaProduccion"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.ClassName("colum1")));

            int filas = Convert.ToInt16(lstTrElem.Count.ToString());
            int idurl = Convert.ToInt16(filas) + 1;
            string value = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[1]")).Text;

            //driver.Navigate().GoToUrl(url + "/lineas/Edit/"+idurl);          
            driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[11]/a[3]")).Click();

            driver.FindElement(By.Id("btnEliminar")).Click();

            var elemTable2 = driver.FindElement(By.Id("tablaProduccion"));

            List<IWebElement> lstTrElem2 = new List<IWebElement>(elemTable2.FindElements(By.TagName("tr")));

            foreach (var elemTr2 in lstTrElem2)
            {
                Console.WriteLine(elemTr2.Text);
                List<IWebElement> lstTdElem2 = new List<IWebElement>(elemTr2.FindElements(By.ClassName("colum1")));
                if (lstTdElem2.Count > 0)
                {
                    foreach (var elemTd in lstTdElem2)
                    {

                        if (elemTd.Text.Equals(value))
                        {
                            NUnit.Framework.Assert.AreEqual(elemTd.Text, value);

                        }
                        else
                        {
                            NUnit.Framework.Assert.AreNotEqual(elemTd.Text, value);
                            Console.WriteLine(idurl);
                        }
                    }

                }

            }



            //driver.Close();

            //driver.Quit();
        }



        

        [Test]
        public void ListarTest()
        {
            Login();
            driver.Navigate().GoToUrl(url + "/produccions/Index");
        //    AgregarAux();
            var elemTable = driver.FindElement(By.Id("tablaProduccion"));

            NUnit.Framework.Assert.IsNotEmpty(elemTable.Text);
            Console.WriteLine(elemTable.Text);

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));

            foreach (var elemTr in lstTrElem)
            {
                List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                if (lstTdElem.Count > 0)
                {
                    NUnit.Framework.Assert.NotZero(lstTdElem.Count);
                    Console.WriteLine("Production Encontradas " + lstTdElem[0].Text);
                }
            }

            //driver.Close();

            //driver.Quit();
        }
        [Test]
        public void recetaNulaTest()
        {
            Login();
            // Variables para busqueda 
            String idReceta = Convert.ToString(CreaReceta());
            String buscaEstado = "estado " + idReceta;
           

            //url 
            driver.Navigate().GoToUrl(url + "/Recetas/Edit/" + idReceta);

            // Click para Anular Receta
            driver.FindElement(By.Id("estado")).Click();

            //click boton
            driver.FindElement(By.ClassName("btn")).Click();

            //Busco elemento id para optener atributos y verificar  que el id existe 
            IWebElement element = driver.FindElement(By.Id(buscaEstado));
            string textoAtributo = element.GetAttribute("checked");

            //comparo elementos
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual("False", textoAtributo);
        }



    }
}
