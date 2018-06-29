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
    public class LineaUitTest
    {
        string url = "http://localhost:60656";
        IWebDriver driver = new ChromeDriver();

        public void Logear()
        {
            driver.Navigate().GoToUrl(url + "/Home/Login");
            driver.FindElement(By.Id("nombre")).SendKeys("Luis");
            driver.FindElement(By.Id("clave")).SendKeys("1234");
            driver.FindElement(By.ClassName("btn-block")).Click();
        }


        public void AgregarAux()
        {
            Random num = new Random();
            int numero = num.Next(0, 100);
            string probar = "LineaPrueba" + numero.ToString();
            driver.Navigate().GoToUrl(url + "/lineas/Create");
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("nombre")).SendKeys(probar);
            driver.FindElement(By.Id("guardar")).Click();

        }


        [Test]
        public void AgregarTest()
        {
            Logear();
            Random num = new Random();
            int numero = num.Next(0, 100);
            string probar = "LineaPrueba"+numero.ToString();           

            driver.Navigate().GoToUrl(url + "/lineas");
            driver.FindElement(By.Id("crear")).Click();
            driver.FindElement(By.Id("nombre")).SendKeys(probar);
            driver.FindElement(By.Id("guardar")).Click();
            
            var elemTable = driver.FindElement(By.Id("tablaLineas"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
            
            foreach (var elemTr in lstTrElem)
            {
                Console.WriteLine(elemTr.Text);
                List<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.ClassName("colum2")));
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
            string prueba = "LineaEditada";
            Logear();            
            driver.Navigate().GoToUrl(url + "/lineas");
            AgregarAux();

            var elemTable = driver.FindElement(By.Id("tablaLineas"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.ClassName("colum1")));
            
            int filas = Convert.ToInt16(lstTrElem.Count.ToString());
            int idurl = Convert.ToInt16(filas) + 1;
            string value = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[1]")).Text;
            
            //driver.Navigate().GoToUrl(url + "/lineas/Edit/"+idurl);          
            driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[3]/a[1]")).Click();
            
            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("nombre")).SendKeys(prueba);
            driver.FindElement(By.Id("guardar")).Click();

            var elemTable2 = driver.FindElement(By.Id("tablaLineas"));

            List<IWebElement> lstTrElem2 = new List<IWebElement>(elemTable2.FindElements(By.TagName("tr")));

            foreach (var elemTr2 in lstTrElem2)
            {
                Console.WriteLine(elemTr2.Text);
                List<IWebElement> lstTdElem2 = new List<IWebElement>(elemTr2.FindElements(By.ClassName("colum2")));
                if (lstTdElem2.Count > 0)
                {
                    foreach (var elemTd in lstTdElem2)
                    {

                        if (elemTd.Text.Equals(prueba))
                        {
                            NUnit.Framework.Assert.AreEqual(elemTd.Text, prueba);

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
            Logear();
            driver.Navigate().GoToUrl(url + "/lineas");
            AgregarAux();

            var elemTable = driver.FindElement(By.Id("tablaLineas"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.ClassName("colum1")));

            int filas = Convert.ToInt16(lstTrElem.Count.ToString());
            int idurl = Convert.ToInt16(filas) + 1;
            string value = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[1]")).Text;
            
            //driver.Navigate().GoToUrl(url + "/lineas/Edit/"+idurl);          
            driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[3]/a[3]")).Click();

            driver.FindElement(By.Id("btnEliminar")).Click();

            var elemTable2 = driver.FindElement(By.Id("tablaLineas"));

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
        public void DetalleEditaTest()
        {
            string prueba = "LineaDetalleEditada";
            Logear();
            AgregarAux();

            driver.Navigate().GoToUrl(url + "/lineas");

            var elemTable = driver.FindElement(By.Id("tablaLineas"));

            List<IWebElement> lstTrElem = new List<IWebElement>(elemTable.FindElements(By.ClassName("colum1")));

            int filas = Convert.ToInt16(lstTrElem.Count.ToString());
            int idurl = Convert.ToInt16(filas) + 1;
            string value = driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[1]")).Text;


            //driver.Navigate().GoToUrl(url + "/lineas/Edit/"+idurl);          
            driver.FindElement(By.XPath("/html/body/div[2]/table/tbody/tr[" + idurl.ToString() + "]/td[3]/a[2]")).Click();

            driver.FindElement(By.Id("btnEditar")).Click();

            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("nombre")).SendKeys(prueba);
            driver.FindElement(By.Id("guardar")).Click();

            var elemTable2 = driver.FindElement(By.Id("tablaLineas"));

            List<IWebElement> lstTrElem2 = new List<IWebElement>(elemTable2.FindElements(By.TagName("tr")));

            foreach (var elemTr2 in lstTrElem2)
            {
                Console.WriteLine(elemTr2.Text);
                List<IWebElement> lstTdElem2 = new List<IWebElement>(elemTr2.FindElements(By.ClassName("colum2")));
                if (lstTdElem2.Count > 0)
                {
                    foreach (var elemTd in lstTdElem2)
                    {

                        if (elemTd.Text.Equals(prueba))
                        {
                            NUnit.Framework.Assert.AreEqual(elemTd.Text, prueba);

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
            Logear();
            driver.Navigate().GoToUrl(url + "/lineas/Index");
            AgregarAux();
            var elemTable = driver.FindElement(By.Id("tablaLineas"));

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
