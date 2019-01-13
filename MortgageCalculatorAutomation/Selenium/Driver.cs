using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;

namespace MortgageCalculatorAutomation
{
	public class Driver
	{
		public static EventFiringWebDriver Instance { get; set;}
		

		public static void Initialize()
		{
			Instance = new EventFiringWebDriver(new ChromeDriver());
			
				//log events
				Instance.FindingElement += (sender, e) => Console.WriteLine(e.FindMethod);
				Instance.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
				Instance.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);

				//TO DO: Refactor find a way to capture data entered (helpful if parameterized by fake (random) data generator)
				Instance.ElementValueChanged += (sender, e) => Console.WriteLine("ElementValueChanged: " + e.Element.GetAttribute("textContent"));

				Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
		}

		public static void Close()
		{
			Instance.Close();
		}
	}
}