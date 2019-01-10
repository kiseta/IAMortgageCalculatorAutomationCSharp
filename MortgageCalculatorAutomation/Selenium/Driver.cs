using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MortgageCalculatorAutomation
{
	public class Driver
	{
		public static IWebDriver Instance { get; set;}

		public static void Initialize()
		{
			Instance = new ChromeDriver();
			Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
		}

		public static void Close()
		{
			Instance.Close();
		}
	}
}