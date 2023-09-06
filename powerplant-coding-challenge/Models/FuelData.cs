using Newtonsoft.Json;

namespace powerplant_coding_challenge.Models
{
    public class FuelData
    {
        [JsonProperty("gas(euro/MWh)")]
        public double GasPrice { get; set; }

        [JsonProperty("kerosine(euro/MWh)")]
        public double KerosinePrice { get; set; }

        [JsonProperty("co2(euro/ton)")]
        public double CO2Price { get; set; }

        [JsonProperty("wind(%)")]
        public int WindPercentage { get; set; }
    }
}
