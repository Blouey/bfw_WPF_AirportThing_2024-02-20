using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using CsvHelper;
using Microsoft.Win32;

namespace WPF_AirportThing_2024_02_20;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static List<Airport> _airportList = new ();
    
    public static List<Airport> temp = new ();
    
    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        using (var reader = new StreamReader(@"..\..\..\airports.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<AirportClassMap>();
            var records = csv.GetRecords<Airport>();
            _airportList = records.ToList();
        }
        
        InitCmbContinentBox();
    }
    
    public MainWindow()
    {
        InitializeComponent();
    
    }
    
    private void InitCmbContinentBox()
    {
        IAccessible dal = new CsvDal(_airportList);
        var airportTypes = dal.GetAllContinents();
        CmbContinent.ItemsSource = airportTypes;
        CmbContinent.IsEditable = true;
        CmbContinent.IsReadOnly = true;
        CmbContinent.Text = "-- Select Type --";
    }


    private void CmbContinent_OnSelectionChanged_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!CmbType.IsEnabled)
        {
            CsvDal dal = new CsvDal(_airportList);
            temp = dal.GetAirportsByContinent(CmbContinent.SelectedValue.ToString()!);
            CmbType.IsEnabled = true;
            CmbType.ItemsSource = temp.Select(a => a.type).Distinct();
            CmbType.IsEditable = true;
            CmbType.IsReadOnly = true;
            CmbType.Text = "-- Select Name --";
            return;
        }
        CmbType.IsEnabled = false;
        CmbType.ItemsSource = null;
        CmbName.IsEnabled = false;
        CmbName.ItemsSource = null;
        CmbContinent_OnSelectionChanged_OnSelectionChanged(sender, e);
    }
    
    private void CmbType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(CmbType.SelectedValue == null) return;
        if (!CmbName.IsEnabled)
        {
            CsvDal dal = new CsvDal(temp);
            var airports = dal.GetAirportsByType(CmbType.SelectedValue.ToString()!);
            CmbName.IsEnabled = true;
            CmbName.ItemsSource = airports.Select(a => a.name).Distinct();
            CmbName.IsEditable = true;
            CmbName.IsReadOnly = true;
            CmbName.Text = "-- Select Name --";
            return;
        }
        CmbName.IsEnabled = false;
        CmbName.ItemsSource = null;
        
        CmbType_OnSelectionChanged(sender, e);
    }

    private void CmbName_OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
    {
        if(CmbName.SelectedValue == null) return;
        CsvDal dal = new CsvDal(_airportList);
        var airport = dal.GetAirportByName(CmbName.SelectedValue.ToString()!);
        
        
        LblId.Content = "ID: " +  airport.id;
        LblName.Content = "Name: " +  airport.name;
        LblIdent.Content = "Ident: " +  airport.ident;
        LblContinent.Content = "Continent: " +  airport.continent;
        LblIsoCountry.Content = "Country: " +  airport.iso_country;
        LblIsoRegion.Content = "Region: " +  airport.iso_region;
        LblMunicipality.Content = "Municipality: " +  airport.municipality;
        LblServices.Content = "Scheduled Services: " +  airport.scheduled_service;
        LblGpsCode.Content = "GPS-Code: " +  airport.gps_code;
        LblIataCode.Content = "Iata-Code: " +  airport.iata_code;
        LblLocalCode.Content = "Local-Code: " +  airport.local_code;
        LblHomeLink.Content = "Home-Link: " +  airport.home_link;
        LblWikiLink.Content = "Wiki-Link: " +  airport.wikipedia_link;
        LblLat.Content = "Latitude: " +  airport.latitude_deg;
        LblLong.Content = "Longitude: " +  airport.longitude_deg;
        LblElevation.Content = "Elevation: "  +  airport.elevation_ft;
        LblType.Content = "Type: "  +  airport.type;
        LblKeywords.Content = "Tags: " +  airport.keywords;
        Load_IFrame((double)airport.longitude_deg!, (double)airport.latitude_deg!);
    }
    
    private void Load_IFrame(double longitude, double latitude)
    {
        string htmlCode = @$"
                            <!DOCTYPE html>
                                <html lang=""de"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                        <meta name=""viewport"" content=""width=device-width,initial-scale=1"">
                                        <title></title>
                                    </head>
                                    <body>
                                        <div style=""width: 100%"">
                                            <iframe width=""100%"" height=""{(WvPanel.ActualHeight - 20).ToString()}"" 
                                                frameborder=""0"" scrolling=""no"" marginheight=""0"" marginwidth=""0"" 
                                                src=""https://maps.google.com/maps?width=100%25&height={(WvPanel.ActualHeight - 20).ToString()}&hl=de&q={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}&t=k&z=17&ie=UTF8&iwloc=B&output=embed"" />
                                        </div>
                                    </body>
                                </html>";
        Wv2.NavigateToString(htmlCode);
    }
    
    private void MnuExit_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void MnuImport_OnClick(object sender, RoutedEventArgs e)
    {
        // ask user if he want to import from new file
        var result = MessageBox.Show("Do you want to import from file?", "Import", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            importFileDialog();
        } 
    }

    private void importFileDialog()
    {
        var ofd = new OpenFileDialog();
        if (ofd.ShowDialog() == true)
        {
            var filePath = ofd.FileName;
            var records = new List<Airport>();
            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<AirportClassMap>();
                records = csv.GetRecords<Airport>().ToList();
            }

            _airportList = records.ToList();
            MessageBox.Show("Import successful", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}