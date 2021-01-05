using System;

namespace Cqrs.Domain.Models
{
    public class AlertPrice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal OdlPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
