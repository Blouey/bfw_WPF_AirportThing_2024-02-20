namespace WPF_AirportThing_2024_02_20;

public interface IAccessible
{
    public int AddAirport(Airport airport);
    public Airport GetAirportById(int id);
    public List<Airport> GetAllAirports();
    public int UpdateAirport(Airport airport);
    public bool DeleteAirportById(int id);
    public List<Airport> GetAirportsByCountry(string country);
    public List<Airport> GetAirportsByType(string type);
    public List<Airport> GetAirportsByScheduledService(string scheduledService);
    public List<Airport> GetAirportsByContinent(string continent);
    public List<Airport> GetAirportsByKeyword(string keyword);
    public List<Airport> GetAirportByIdent(string ident);
    public Airport GetAirportByName(string name);
    
    public List<string> GetAllContinents();
    
    public List<string> GetAllTypes();

}