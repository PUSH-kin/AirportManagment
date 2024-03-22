namespace AirportService.Domain.Contracts
{
    /// <summary>
    /// Описание воздушного судна
    /// </summary>
    public record Plane
    {
        public Guid PlaneId { get; set; }

        public string? FlightNumber { get; set; }

        /// <summary>
        /// Код самолёта согласно ICAO
        /// </summary>
        public string? AircraftICAO { get; set; }


        /// <summary>
        /// Конструтор для десериализатора
        /// </summary>
        public Plane()
        {
            
        }



        public Plane(string flightNumber, string aircraftIATA)
        {
            PlaneId = Guid.NewGuid();
            FlightNumber = flightNumber;
            AircraftICAO = aircraftIATA;
        }

        public Plane(Guid planeId, string flightNumber, string aircraftIATA)
        {
            PlaneId = planeId;
            FlightNumber = flightNumber;
            AircraftICAO = aircraftIATA;
        }

        public override string ToString()
        {
            return $"{this.FlightNumber} (ICAO: {this.AircraftICAO})";
        }
    }
}
