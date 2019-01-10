using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace MortgageCalculatorAutomation
{
	public class MortgageCalculatorPage
	{
		public static void NavigateToUrl()
		{
			Driver.Instance.Navigate().GoToUrl("https://ia.ca/mortgage-payment-calculator");
		}
		
		public static CalculationCommand CalculatePayment(int purchasePrice)
		{
			return new CalculationCommand(purchasePrice);
		}

		public static bool ResultIsDisplayed 
		{ 
		
			get
				{
				var PaymentResults = Driver.Instance.FindElements(By.CssSelector("span#paiement-resultats.calculateur-resultats-total"));
				Console.WriteLine("Payment Results Element count: " + PaymentResults.Count);

				//TO DO: refactor to validate 
				Thread.Sleep(1000);
				var PaymentResultValue = Driver.Instance.FindElement(By.CssSelector("span#paiement-resultats.calculateur-resultats-total")).GetAttribute("textContent");
				Console.WriteLine("Payment Result Value: " + PaymentResultValue);


				if (PaymentResults.Count > 0)
					return true;
				return false;
				} 
		
		} 

	}

	public class CalculationCommand
	{
		private int purchasePrice;
		private int downPayment;
		private double interestRate;
		private int amortizationYears;
		private int paymentFrequency;

		public CalculationCommand(int purchasePrice)
		{
			this.purchasePrice = purchasePrice;
		}

		public CalculationCommand WithDownPayment(int downPayment)
		{
			this.downPayment = downPayment;
			return this;
		}

		public CalculationCommand WithInterestRate(double interestRate)
		{
			this.interestRate = interestRate;
			return this;
		}

		public CalculationCommand WithAmortization(int amortizationYears)
		{
			this.amortizationYears = amortizationYears;
			return this;
		}

		public CalculationCommand WithPaymentFrequency(int paymentFrequency)
		{
			this.paymentFrequency = paymentFrequency;
			return this;
		}
		public void CalculatePayment()
		{

			var PurchasePriceField = Driver.Instance.FindElement(By.Id("PrixPropriete"));
			PurchasePriceField.SendKeys(purchasePrice.ToString());

			var DownPaymentAmountField = Driver.Instance.FindElement(By.Id("MiseDeFond"));
			DownPaymentAmountField.SendKeys(downPayment.ToString());

			var InterestRateFiled = Driver.Instance.FindElement(By.Id("TauxInteret"));
			InterestRateFiled.Clear();
			InterestRateFiled.SendKeys(interestRate.ToString());

			SelectAmortizationValue(amortizationYears);

			SelectPaymentFrequencyValue(paymentFrequency);

			var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
			wait.Until(d => d.FindElement(By.Id("btn_calculer")));

			var CalculateButton = Driver.Instance.FindElement(By.Id("btn_calculer"));
			CalculateButton.Click();

		}

		//temporary method for selecting values from Amortization drop down list,
		//TO DO: refactor to match passed data value with the list value 
		//(currently passed value is not used)
		public void SelectAmortizationValue(int amortizationYears) 
		{
			//expand list		TO DO: Refactor css selector
			var AmortizationPeriodList = Driver.Instance.FindElement(By.CssSelector("div.selectric-wrapper:nth-child(2) > div:nth-child(2) > b:nth-child(2)"));
			AmortizationPeriodList.Click();

			//select 17 years
			var AmortizationPeriodListValue = Driver.Instance.FindElement(By.CssSelector("div.selectric-open li:nth-child(3)"));
			AmortizationPeriodListValue.Click();
		}

		//temporary method for selecting values from Payment Frequency drop down list,
		//TO DO: refactor to match passed data value with the list value 
		//(currently passed value is not used)
		public void SelectPaymentFrequencyValue(int paymentFrequency)
		{
			//expand list	TO DO: Refactor css selector
			var PaymentFrequencyList = Driver.Instance.FindElement(By.CssSelector("div.selectric-wrapper:nth-child(6) > div:nth-child(2) > b:nth-child(2)"));
			PaymentFrequencyList.Click();

			//select BiWeekly TO DO: Refactor match passed value with the element
			var PaymentFrequencyListValue = Driver.Instance.FindElement(By.CssSelector("div.selectric-open li:nth-child(2)"));
			PaymentFrequencyListValue.Click();
		}


	}
}
