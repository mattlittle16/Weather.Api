namespace Core.RequestModels
{
    public record GeocodeRequestModel
    {
        public string? City { get; init; }

        public string? State { get; init; }

        public string? PostalCode { get; init; }
    }
}