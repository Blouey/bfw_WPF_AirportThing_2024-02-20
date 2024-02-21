using System.Globalization;
using System.IO;
using CsvHelper;

namespace WPF_AirportThing_2024_02_20;

public class CsvDal(List<Airport> airportList) : IAccessible
{
    private List<Airport> _list = airportList;

    public int AddAirport(Airport airport)
    {
        throw new NotImplementedException();
    }
    
    public Airport GetAirportById(int getId)
    {
        throw new NotImplementedException(); 
    }
    
    public List<Airport> GetAllAirports()
    {
        throw new NotImplementedException();
    }
    
    public int UpdateAirport(Airport airport)
    {
        throw new NotImplementedException();
    }
    
    public bool DeleteAirportById(int delId)
    {
        throw new NotImplementedException();
    }
    
    public List<Airport> GetAirportsByCountry(string country)
    {
        throw new NotImplementedException();
    }
    
    public List<Airport> GetAirportsByType(string searchType)
    {
        List<Airport> airports = new List<Airport>();
        foreach (var airport in _list)
        {
            if(airport.type == searchType) airports.Add(airport);
        }
        airports.Sort();
        return airports;
    }
    
    public List<Airport> GetAirportsByScheduledService(string scheduledService)
    {
        throw new NotImplementedException();
    }
    
    public List<Airport> GetAirportsByContinent(string searchContinent)
    {
        var airports = new List<Airport>();
        
        foreach (var a in _list)
        {
            if(a.continent == searchContinent) airports.Add(a);
        }

        return airports;
    }
    
    public List<Airport> GetAirportsByKeyword(string keyword)
    {
        throw new NotImplementedException();
    }
    
    public List<Airport> GetAirportByIdent(string searchIdent)
    {
        throw new NotImplementedException();
    }
    
    public Airport GetAirportByName(string searchName)
    {
        
        foreach (var a in _list)
        {
            if(a.name == searchName) return a;
        }

        return new Airport();
    }
    
    public List<string> GetAllContinents()
    {
        List<string> airportContinents = new List<string>();
        foreach (var airport in _list)
        {
            if(!airportContinents.Contains(airport.continent!)) airportContinents.Add(airport.continent!);
        }
        return airportContinents;
    }
    
    public List<string> GetAllTypes()
    {
        List<string> airportTypes = new List<string>();
        foreach (var airport in _list)
        {
            if(!airportTypes.Contains(airport.type!)) airportTypes.Add(airport.type!);
        }
        airportTypes.Sort();
        return airportTypes;
    }
    
    public List<string> GetAllNames()
    {
            List<string> airportNames = new List<string>();
            foreach (var airport in _list)
            {
                airportNames.Add(airport.name!);
            }
            return airportNames;
    }
}