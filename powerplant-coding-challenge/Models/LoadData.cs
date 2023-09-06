namespace powerplant_coding_challenge.Models
{
    public class LoadData
    {
        public double Load { get; set; }
        public FuelData Fuels { get; set; }
        public List<PowerPlantData> PowerPlants { get; set; }
    }
}
