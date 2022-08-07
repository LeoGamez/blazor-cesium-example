namespace blazorium.Shared.Model
{
    public class WeatherPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public double TemperatureC { get; set; }
        
        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public override string ToString()
        {
            return this.Name + "," + this.Country +","+TemperatureC+"°C";
        }

    }
}
