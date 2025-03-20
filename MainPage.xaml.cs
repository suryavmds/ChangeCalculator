using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace ChangeCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            // Clear previous results
            ChangeDenominationsView.ItemsSource = null;

            // Validate inputs
            if (!decimal.TryParse(PriceEntry.Text, out decimal price) ||
                !decimal.TryParse(AmountPaidEntry.Text, out decimal amountPaid))
            {
                DisplayAlert("Error", "Please enter valid numbers.", "OK");
                return;
            }

            if (amountPaid < price)
            {
                DisplayAlert("Error", "The amount paid is less than the price.", "OK");
                return;
            }

            // Calculate change
            decimal change = amountPaid - price;

            // Round to the nearest 5 cents (Canadian rule)
            change = Math.Round(change * 20) / 20;

            // Calculate denominations
            var denominations = new Dictionary<string, decimal>
            {
                { "100 Dollar Bill", 100m },
                { "50 Dollar Bill", 50m },
                { "20 Dollar Bill", 20m },
                { "10 Dollar Bill", 10m },
                { "5 Dollar Bill", 5m },
                { "2 Dollar Coin", 2m },
                { "1 Dollar Coin", 1m },
                { "25 Cent Coin", 0.25m },
                { "10 Cent Coin", 0.10m },
                { "5 Cent Coin", 0.05m },
                { "1 Cent Coin", 0.01m }
            };

            var changeDenominations = new List<string>();

            foreach (var denom in denominations)
            {
                if (change >= denom.Value)
                {
                    int count = (int)(change / denom.Value);
                    change -= count * denom.Value;
                    changeDenominations.Add($"{count} x {denom.Key}");
                }
            }

            // Display results
            ChangeDenominationsView.ItemsSource = changeDenominations;
        }
    }
}