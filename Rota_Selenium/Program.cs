using System;
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
            // User Input
            string artista; Console.Write("Inserisci nome dell'artista: "); 
            artista = Console.ReadLine();

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl($"https://open.spotify.com/search/{artista}/artists");

            Thread.Sleep(2000);

            // Get the first artist element link
            IWebElement div = driver.FindElement(By.ClassName("main-view-container"));
            IWebElement firstArtistLink = div.FindElement(By.CssSelector("div[data-encore-id='card']"));

            // Click on the first artist element link
            firstArtistLink.Click();

            // Get current URL
            string artistURL = driver.Url;
            artistURL += "/discography/album";

            // Go to new URL
            driver.Navigate().GoToUrl(artistURL);

            // Get Albums, Tracklist exc.
            Thread.Sleep(1000);
            IWebElement discog = driver.FindElement(By.ClassName("main-view-container")); 
            IWebElement albumEl = discog.FindElement(By.TagName("span"));
            Thread.Sleep(1000);
            IWebElement albumTitle = albumEl.FindElement(By.TagName("a"));
            Console.Write(albumTitle.GetAttribute("outerHTML"));

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