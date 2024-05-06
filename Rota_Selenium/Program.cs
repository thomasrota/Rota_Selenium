using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;

namespace Rota_Selenium
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string JsonFilePath = @"billboardGlobal200.json";

			IWebDriver driver = new ChromeDriver(); 
			driver.Navigate().GoToUrl($"https://www.billboard.com/charts/billboard-global-200/");

			// Wait for the page to load
			Thread.Sleep(5000); // Wait for 5 seconds

			//click on the reject button for cookies 
			driver.FindElement(By.Id("onetrust-reject-all-handler")).Click();

			List<Dictionary<string, string>> chartData = new List<Dictionary<string, string>>();

			var chartEntries = driver.FindElements(By.ClassName("o-chart-results-list-row-container"));
			int position = 1;

			foreach (var entry in chartEntries)
			{
				// Extract title, artist, peak position, and weeks on chart information
				string title = entry.FindElement(By.Id("title-of-a-story")).Text;
				string artist = entry.FindElement(By.CssSelector(".c-label.a-no-trucate.a-font-primary-s")).Text;
				string peakPosition = entry.FindElement(By.CssSelector(".o-chart-results-list__item:nth-child(4) .c-label")).Text;
				string weeksOnChart = entry.FindElement(By.CssSelector(".o-chart-results-list__item:nth-child(6) .c-label")).Text;

				// Create a dictionary to store the data
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{ "position", position.ToString() },
					{ "title", title },
					{ "artist", artist },
					{ "peak_position", peakPosition },
					{ "weeks_on_chart", weeksOnChart }
				};

				chartData.Add(data);
				position++;
			}

			// Serialize the data to JSON and save it to a file
			string json = JsonConvert.SerializeObject(chartData, Formatting.Indented);
			File.WriteAllText(JsonFilePath, json);

			// Close the browser
			driver.Quit();
		}
	}
}