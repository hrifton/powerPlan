using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using powerplant_coding_challenge.Controllers;
using powerplant_coding_challenge.DAO;
using powerplant_coding_challenge.Models;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace powerplant_coding_challenge.Services
{
    public class ServicePowerPlantData
    {
       private readonly ILogger<ServicePowerPlantData> _logger;
       
        public ServicePowerPlantData(ILogger<ServicePowerPlantData>  logger)
        {            
            _logger = logger;
        }


        public List<PowerPlantResponse> CalculateProductionPlan(LoadData payload)
        {

            _logger.LogInformation($"{GetType().Name}: CalculateProductionPlan");

            double load = payload.Load;
            FuelData fuels = payload.Fuels;
            List<PowerPlantData> powerPlants = payload.PowerPlants;

            List<PowerPlantResponse> productionPlan = new List<PowerPlantResponse>();

            foreach (var item in powerPlants)
            {
                _logger.LogInformation($"{JsonConvert.SerializeObject(item)}");
                double power = 0;

                //puissance Central Electric
                switch (item.Type)
                {
                    case "gasfired":
                        // Calculer la puissance en utilisant le prix du gaz
                        power = CalculateGasFiredPower(item,fuels.GasPrice,load) ;
                        break;
                    case "turbojet":
                        // Calculer la puissance en utilisant l'efficacité et la charge
                        power = CalculateTurbojetPower(item, load);
                        break;
                    case "windturbine":
                        // Calculer la puissance en utilisant le pourcentage éolien et la charge
                        power = CalculateWindTurbinePower(item, load, fuels.WindPercentage);
                        break;

                    default:break;
                }
                productionPlan.Add(new PowerPlantResponse
                {
                    Name = item.Name,
                    Power = power
                });
            }

            return productionPlan;
        }

        private double CalculateGasFiredPower(PowerPlantData item, double gasPrice,double load)
        {
            double gasPower = item.Pmax / item.Efficiency;
            double result = Math.Min(gasPower,item.Pmax);
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} => {result}");
            return result;
        }

        private double CalculateTurbojetPower(PowerPlantData item, double load)
        {
             double result = Math.Min(item.Pmax, Math.Max(item.Pmin, load / item.Efficiency));
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} => {result}");
            return result;
        }

        private double CalculateWindTurbinePower(PowerPlantData item, double load, double windPercentage)
        {            
            double result = windPercentage/100*item.Pmax;
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name} => {result}");
            return result;
        }


      
    }

}
