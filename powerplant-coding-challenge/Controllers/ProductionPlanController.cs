using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using powerplant_coding_challenge.DAO;
using powerplant_coding_challenge.Models;
using powerplant_coding_challenge.Services;

namespace powerplant_coding_challenge.Controllers
{
    public class ProductionPlanController : Controller
    {

        private readonly ILogger<ProductionPlanController> _logger;
        private ServicePowerPlantData _service;

        public ProductionPlanController(ILogger<ProductionPlanController> logger, ServicePowerPlantData service)
        {
            _logger = logger;
            _service = service;
        }



        [HttpPost("/productionplan")]
        public IActionResult CalculatevProductionPlan([FromBody] string payloadJson)
        {
            try
            {
                _logger.LogInformation("Début du calcul du plan de production d'énergie.");
                // Désérialiser la charge utile (payload) en objets C#
                LoadData payload = JsonConvert.DeserializeObject<LoadData>(payloadJson);
                _logger.LogInformation($"Charge reçue : {JsonConvert.SerializeObject(payload)}");
                //Calcul De production
                List<PowerPlantResponse> powerPlantResponses = _service.CalculateProductionPlan(payload);

                // Sérialiser la réponse en JSON
                string jsonResponse = JsonConvert.SerializeObject(powerPlantResponses);
                return Ok(jsonResponse);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erreur lors de la désérialisation du payload JSON.");
                return BadRequest("Erreur lors de la désérialisation du payload JSON.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors du calcul du plan de production d'énergie.");
                return StatusCode(500, $"Une erreur s'est produite lors du calcul du plan de production d'énergie : {ex.Message}");
            }
        }
    }
}
