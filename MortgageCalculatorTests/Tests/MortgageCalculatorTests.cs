using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculatorAutomation;

namespace MortgageCalculatorTests
{
	[TestClass]
	public class PurchasePriceTests
	{
		[TestInitialize]
		public void Init()
		{
			Driver.Initialize();
		}

		[TestMethod]
		public void CalculatePaymentUsingFields()
		{
			
		//TO DO: refactor: Parameterize with external parameter/Json file 
			int purchasePrice = 300000;
			int downPayment = 50000;
			double interestRate = 3.33;
			int amortizationYears = 17;
			int paymentFrequency = 26;

			MortgageCalculatorPage.NavigateToUrl();

			MortgageCalculatorPage.CalculatePayment(purchasePrice)
					.WithDownPayment(downPayment)
					.WithInterestRate(interestRate)
					.WithAmortization(amortizationYears)
					.WithPaymentFrequency(paymentFrequency)
					.CalculatePayment();

			//TO-DO: Refactor - element detected displayed even if not visible to the user
			Assert.IsTrue(MortgageCalculatorPage.ResultIsDisplayed, "Result is not displayed");
		}

		public void CalculatePaymentUsingButtons()
		{
		//placeholder for testing entering values using Plus/Minus buttons
		}

		public void CalculatePaymentUsingSliders()
		{
			//placeholder for testing entering values using sliders
		}

		[TestCleanup]
		public void Cleanup()
		{
			Driver.Close();
		}

	}

	public class MortgageAmountTests
		{
		//placeholder for Mortgage Amount Tests
		}
	}
