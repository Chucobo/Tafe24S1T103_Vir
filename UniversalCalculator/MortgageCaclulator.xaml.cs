using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCaclulator : Page
	{
		public MortgageCaclulator()
		{
			this.InitializeComponent();
		}

		private void pageLoaded(object sender, RoutedEventArgs e)
		{
			// window minimum size
			ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(1024, 1024));
		}

		private async void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			double principalBorrowAmount,yearlyInterestRate, monthlyRepayments, monthlyInterestRate, baseValue;
			int month, year, totalMonths;

			//validate principal amount
			try
			{
				principalBorrowAmount = double.Parse(principalBorrowTextBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid  number for Principal Borrow");
				await dialogMessage.ShowAsync();
				principalBorrowTextBox.Focus(FocusState.Programmatic);
				return;
			}
			//validate year
			try
			{
				year = int.Parse(yearTextBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid  number for Years");
				await dialogMessage.ShowAsync();
				yearTextBox.Focus(FocusState.Programmatic);
				return;
			}
			//validate month
			try
			{
				month = int.Parse(monthTextBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid  number for Months");
				await dialogMessage.ShowAsync();
				monthTextBox.Focus(FocusState.Programmatic);
				return;
			}
			//Validate month number
			if (month < 0 || month > 12  )
			{
				var dialogMessage = new MessageDialog("Error! Enter a number between 0-12 for Months");
				await dialogMessage.ShowAsync();
				monthTextBox.Focus(FocusState.Programmatic);
				return;
			}
			//validate Yearly Interest Rate
			try
			{
				yearlyInterestRate = double.Parse(yearlyInterestRateTextBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid number for Yearly Interest Rate");
				await dialogMessage.ShowAsync();
				yearlyInterestRateTextBox.Focus(FocusState.Programmatic);
				return;
			}

			totalMonths = (year * 12) + month;
			monthlyInterestRate = yearlyInterestRate / 100 / 12;
			monthlyRepayments = principalBorrowAmount * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths)) / (Math.Pow(1 + monthlyInterestRate, totalMonths) - 1);
			monthlyInterestRateTextBox.Text = monthlyInterestRate.ToString();
			monthlyRepaymentsTextBox.Text = monthlyRepayments.ToString("C");
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			MainMenu page = new MainMenu();
			this.Content = page;
		}
	}
}
