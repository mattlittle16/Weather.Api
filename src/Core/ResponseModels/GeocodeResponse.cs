using Core.Models;

namespace Core.ResponseModels;

public class GeocodeResponse
{
    public GeocodeResponse() 
    {

    }

    public GeocodeResponse(Geocode geocode)
    {
        Name = geocode.Name;
        Latitude = geocode.Lat;
        Longitude = geocode.Lon;
        Country = geocode.Country;
        State = geocode.State;
    }

    public string? Name { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
}