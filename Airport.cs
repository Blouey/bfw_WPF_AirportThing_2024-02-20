using CsvHelper.Configuration;

namespace WPF_AirportThing_2024_02_20;

// "id","ident","type","name","latitude_deg","longitude_deg","elevation_ft","continent","iso_country","iso_region","municipality","scheduled_service","gps_code","iata_code","local_code","home_link","wikipedia_link","keywords"
public class Airport : IComparable<Airport>
{
    public int id { get; set; }
    public string? ident { get; set; }
    public string? type { get; set; }
    public string? name { get; set; }
    public double? latitude_deg { get; set; }
    public double? longitude_deg { get; set; }
    public string? elevation_ft { get; set; }
    public string? continent { get; set; }
    public string? iso_country { get; set; }
    public string? iso_region { get; set; }
    public string? municipality { get; set; }
    public string? scheduled_service { get; set; }
    public string? gps_code { get; set; }
    public string? iata_code { get; set; }
    public string? local_code { get; set; }
    public string? home_link { get; set; }
    public string? wikipedia_link { get; set; }
    public string? keywords { get; set; }
    
    
    public int CompareTo(Airport? other)
    {
        if (other == null) return 1;
        return string.Compare(name, other.name, StringComparison.Ordinal);
    }

}

public class AirportClassMap : ClassMap<Airport>
{
    public AirportClassMap()
    {
        Map(m => m.id).Name("id");
        Map(m => m.ident).Name("ident");
        Map(m => m.type).Name("type");
        Map(m => m.name).Name("name");
        Map(m => m.latitude_deg).Name("latitude_deg");
        Map(m => m.longitude_deg).Name("longitude_deg");
        Map(m => m.elevation_ft).Name("elevation_ft");
        Map(m => m.continent).Name("continent");
        Map(m => m.iso_country).Name("iso_country");
        Map(m => m.iso_region).Name("iso_region");
        Map(m => m.municipality).Name("municipality");
        Map(m => m.scheduled_service).Name("scheduled_service");
        Map(m => m.gps_code).Name("gps_code");
        Map(m => m.iata_code).Name("iata_code");
        Map(m => m.local_code).Name("local_code");
        Map(m => m.home_link).Name("home_link");
        Map(m => m.wikipedia_link).Name("wikipedia_link");
        Map(m => m.keywords).Name("keywords");
    }
}