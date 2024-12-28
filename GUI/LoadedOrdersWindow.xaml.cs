using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using GUI.Helpers;
using Shopify2Bring;
using Shopify2Bring.Types;

namespace GUI;

public partial class LoadedOrdersWindow : Window
{
    private readonly List<Order> _orders;
    private readonly BringConfig _bringConfig;
    

    public LoadedOrdersWindow(List<Order> orders)
    {
        InitializeComponent();

        
        var appSettingsJson = File.ReadAllText(   Path.Join( AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        var bringConfig = JsonSerializer.Deserialize<BringConfig>(appSettingsJson);

        _bringConfig = bringConfig ?? throw new InvalidDataException("appsettings.json is not valid");
        
        OrdersDataGrid.ItemsSource = orders;
        OrdersDataGrid.IsReadOnly = true;
        _orders = orders;
    }

    private async void BookButton_OnClick(object sender, RoutedEventArgs e)
    {
        BookButton.IsEnabled = false;
        OrdersDataGrid.Visibility = Visibility.Collapsed;
        StatusTextBox.Visibility = Visibility.Visible;
        var pdfLocations = new List<string>();
        
        await Task.Run(() =>
        {
            var index = 1;
            _orders.ForEach(order =>
            {
                var response = new BringSdk(_bringConfig).Book(order);

                var errors = response.Consignments.FirstOrDefault()?.Errors;
                pdfLocations.Add(response.Consignments.First().Confirmation.Links.Labels);
                
                var responseMessage = "";

                if (errors == null)
                {
                    responseMessage = $"{index++}/{_orders.Count} Order for {order.ShippingName} booked successfully\n";
                }
                else
                {
                    responseMessage = $"{index++}/{_orders.Count} Order for {order.ShippingName} returned errors: " + string.Join(", ", errors) + "\n";

                }


                Console.WriteLine(responseMessage);

                Dispatcher.Invoke(() => { StatusTextBox.Text += responseMessage; });

                Thread.Sleep(3000);
            });
            
            Dispatcher.Invoke(() => { StatusTextBox.Text += "Done"; });

            var filename = Path.GetTempFileName().Replace(".tmp", ".pdf");
            
            Console.WriteLine(filename);
            var pdfTask = new PdfHelper().DownloadAndMergePdf(pdfLocations, filename);
            pdfTask.Wait();
            
            
            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });

        });


    }
}
