using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record PaymentDto
    {
        public DateTime ReservationStartDate { get; init; }
        public int Duration { get; init; }
        public string ChairId { get; init; }

        public required string CardName { get; init; }
        public required string CardNumber { get; init; }
        public required string ExpirationMonth { get; init; }
        public required string ExpirationYear { get; init; }
        public required string Cvv { get; init; }
    }
}
