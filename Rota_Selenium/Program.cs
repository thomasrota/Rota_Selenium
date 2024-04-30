﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Rota_Selenium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl($"https://www.billboard.com/charts/billboard-global-200/");

            Thread.Sleep(2000);

            // Get the first artist element link
            IWebElement div = driver.FindElement(By.ClassName("chart-results-list"));

            Console.Write(div.GetAttribute("outerHTML"));

            /*var uls = div.FindElements(By.TagName("ul"));
            List<string> autori = new List<string>();
            //variabile foreach
            foreach (IWebElement ele in uls)
            {
                //Console.WriteLine(uls[0].Text);
                var lis = ele.FindElements(By.TagName("li"));
                //inserisco gli autori nell'array
                foreach (IWebElement autore in lis)
                {
                    autori.Add(autore.Text);
                }
            }*/
            //serializzo l'array autori
            //string JSON = System.Text.Json.JsonSerializer.Serialize(autori);

            //scrivo il JSON su file
            //File.WriteAllText("autore.json", JSON);
            Thread.Sleep(10000);
            driver.Quit();
        }
    }
}