namespace AirportService.Domain.Contracts
{
    /// <summary>
    /// Сервис информации о воздушных судах
    /// </summary>
    public class PlaneService
    {
        private Random random;
        
        private List<Plane> Planes { get; set;} = new List<Plane>();

        


        public PlaneService()
        {
            random = new Random();
            var planes = new List<Plane>()
            {
                new ("SU1702", "A359"),
                new ("SU1249", "A320"),
                new ("SU1611", "A321"),
                new ("SU1445", "B738"),
                new ("QR337", "B788"),
                new ("D2157", "CRJ2"),
                new ("4G252", "SU95"),
                new ("UT180", "B735"),
                new ("SU220", "B77W"),
                new ("U6105", "A21N"),
                new ("S75075", "E170")

            };
            Planes.AddRange(planes);
        }


        public static Plane GetNull()
        {
            return new Plane(Guid.Empty, "None", "None");
        }


        public Plane GetNext()
        {
            var index = random.Next(Planes.Count);
            return Planes[index];
        }
    }
}
