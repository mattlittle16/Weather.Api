namespace Core.RequestModels;

public record GeocodeRequestModel(string? City, string? State, string? PostalCode, string? CountryCode);