using System.Windows;
using System.Windows.Media.TextFormatting;
using Microsoft.Win32;
using Shopify2Bring;

namespace GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Order>? loadedOrders;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Grid_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                HandleFile(file);
            }
        }
    }

    private void OpenFile_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (openFileDialog.ShowDialog() == true)
        {
            string filename = openFileDialog.FileName;
            HandleFile(filename);
        }
    }

    private void HandleFile(string filename)
    {
        var orders = new OrdersCsvReader().Read(filename);
        var loadedOrdersWindow = new LoadedOrdersWindow(orders);
        loadedOrdersWindow.Show();
        Close();

    }
}
