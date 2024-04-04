using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Calculator
{
	public sealed partial class CurrencyCalculator : Page
	{
		public CurrencyCalculator()
		{
			this.InitializeComponent();
		}

		// When currency conversion button is clicked
		private void CurrencyConversion_Click(object sender, RoutedEventArgs e)
		{
			// Hardcoded conversion rates
			Dictionary<string, Dictionary<string, double>> conversionRates = new Dictionary<string, Dictionary<string, double>>
	{
		{ "US Dollar", new Dictionary<string, double>
			{
				{ "Euro", 0.85189982 },
				{ "British Pound", 0.72872436 },
				{ "Indian Rupee", 74.257327 }
			}
		},
		{ "Euro", new Dictionary<string, double>
			{
				{ "US Dollar", 1.1739732 },
				{ "British Pound", 0.8556672 },
				{ "Indian Rupee", 87.00755 }
			}
		},
		{ "British Pound", new Dictionary<string, double>
			{
				{ "US Dollar", 1.371907 },
				{ "Euro", 1.1686692 },
				{ "Indian Rupee", 101.68635 }
			}
		},
		{ "Indian Rupee", new Dictionary<string, double>
			{
				{ "US Dollar", 0.011492628 },
				{ "Euro", 0.013492774 },
				{ "British Pound", 0.0098339397 }
			}
		}
	};

			// Get the amount to convert
			double amount;
			if (!double.TryParse(amountTextBox.Text, out amount))
			{
				resultTextBlock.Text = "Invalid amount";
				return;
			}

			// Get the selected currencies
			string fromCurrency = ((ComboBoxItem)fromCurrencyComboBox.SelectedItem)?.Content.ToString();
			string toCurrency = ((ComboBoxItem)toCurrencyComboBox.SelectedItem)?.Content.ToString();

			// Check if currencies are selected
			if (string.IsNullOrWhiteSpace(fromCurrency) || string.IsNullOrWhiteSpace(toCurrency))
			{
				resultTextBlock.Text = "Select currencies";
				return;
			}

			// Check if conversion rates are available
			if (!conversionRates.ContainsKey(fromCurrency) || !conversionRates[fromCurrency].ContainsKey(toCurrency))
			{
				resultTextBlock.Text = "Select both currencies";
				return;
			}
			else
			{
				double fromToRate = conversionRates[fromCurrency][toCurrency];
				double toFromRate = conversionRates[toCurrency][fromCurrency];
				fromTextBlock.Text =  "1 " + fromCurrency + " = " + fromToRate + " " + toCurrency;
				toTextBlock.Text = "1 " + toCurrency + " = " + toFromRate + " " + fromCurrency;
			}
			// Perform conversion
			double conversionRate = conversionRates[fromCurrency][toCurrency];
			double result = amount * conversionRate;

			// Display result
			resultTextBlock.Text = $"{amount} {fromCurrency} = {result} {toCurrency}";
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			MainMenu page = new	MainMenu();
			this.Content = page;
		}
	}
}
