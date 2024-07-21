using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.RequestModels
{
    public record GeocodeRequestModel
    {
        public string? City { get; init; }

        public string? State { get; init; }

        public string? PostalCode { get; init; }
    }
}