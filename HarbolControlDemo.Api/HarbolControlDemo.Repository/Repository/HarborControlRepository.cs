using HarbolControlDemo.DataModels.Enum;
using HarbolControlDemo.DataModels.Models;
using HarbolControlDemo.Repository.Interface;
using HarbolControlDemo.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HarbolControlDemo.Repository.Repository
{
    public class HarborControlRepository : IHarborControlRepository
    {
        #region Variables
        private readonly ILogger<HarborControlRepository> _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly AppSettingConfiguration _appSettingCongifuration;
        private List<BoatInformation> boatInformationList = null;
        #endregion

        public HarborControlRepository(ILogger<HarborControlRepository> logger,
            IOptions<AppSettingConfiguration> appSettingCongifuration,
            IHttpClientFactory httpFactory)
        {
            boatInformationList = new List<BoatInformation>()
            {
                new BoatInformation(){ Id =1,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =2,BoatType = BoatType.SailBoat,BoatSpeed="15 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =3,BoatType = BoatType.CargoShip,BoatSpeed="5 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =4,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =5,BoatType = BoatType.SailBoat,BoatSpeed="15 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =6,BoatType = BoatType.CargoShip,BoatSpeed="5 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =7,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =8,BoatType = BoatType.SailBoat,BoatSpeed="15 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =9,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =10,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =11,BoatType = BoatType.SailBoat,BoatSpeed="15 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =12,BoatType = BoatType.CargoShip,BoatSpeed="5 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =13,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =14,BoatType = BoatType.SailBoat,BoatSpeed="15 Km/h", BoatStatus=BoatStatusType.None},
                new BoatInformation(){ Id =15,BoatType = BoatType.SpeedBoat,BoatSpeed="30 Km/h", BoatStatus=BoatStatusType.None},
            };
            _logger = logger;
            _httpFactory = httpFactory;
            _appSettingCongifuration = appSettingCongifuration.Value;
        }

        #region Public Methods
        /// <summary>
        /// Description: Get all random boat related informations
        /// Created by : Kishan Prajapati
        /// Created On :23/09/2020
        /// </summary>
        /// <param name="boatCount"></param>
        /// <returns>Boat Information List</returns>

        public async Task<List<BoatInformation>> GetBoatInformationDetails(int boatCount)
        {
            try
            {
                List<BoatInformation> boatInformations = new List<BoatInformation>();
                BoatInformation boatInformationObj = new BoatInformation();
                if (boatInformationList != null && boatInformationList.Count > 0)
                {
                    boatInformations = Common.PickRandom<BoatInformation>(boatInformationList, boatCount);
                    int incrementCount = 0;
                    int boatTimeDurationTemp = 0;
                    var url = Common.BuildOpenWeatherUrl("durban", _appSettingCongifuration.OpenWeatherApiKey);
                    var client = _httpFactory.CreateClient();
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var openWeatherResponse = JsonSerializer.Deserialize<OpenWeatherResponse>(json);
                    var _windSpeed = openWeatherResponse.wind.speed;
                    var _isCheckedWindSpeed = false;
                    var _isInProgress = false;
                    foreach (var item in boatInformations)
                    {
                        switch (item.BoatType)
                        {
                            case BoatType.SailBoat:
                                if (_windSpeed < 10 || _windSpeed > 30)
                                {
                                    item.BoatStatus = BoatStatusType.None;
                                    _isCheckedWindSpeed = true;
                                }
                                else
                                {
                                    if (incrementCount == 0)
                                    {
                                        item.BoatStatus = BoatStatusType.InProgress;
                                        _isInProgress = true;
                                        _isCheckedWindSpeed = false;
                                    }
                                    else
                                        item.BoatStatus = BoatStatusType.Queue;
                                }
                                break;
                            default:
                                if (incrementCount == 0)
                                {
                                    item.BoatStatus = BoatStatusType.InProgress;
                                    _isInProgress = true;
                                }
                                else
                                {
                                    if (_isCheckedWindSpeed && !_isInProgress)
                                    {
                                        item.BoatStatus = BoatStatusType.InProgress;
                                        _isCheckedWindSpeed = false;
                                        _isInProgress = true;
                                    }
                                    else
                                    {
                                        item.BoatStatus = BoatStatusType.Queue;
                                    }
                                }
                                break;
                        }

                        item.BoatReachTimeDuration = boatTimeDurationTemp;
                        string[] splitSpeed = item.BoatSpeed.Split(' ');
                        int boatReachTimeDuration = Common.CalculateSpeed(Convert.ToInt16(splitSpeed[0]));
                        //Note: consider 10 min is equal to 1 second
                        switch (boatReachTimeDuration)
                        {
                            case 120:
                                item.BoatActualTimeDuration = 12;
                                if (boatTimeDurationTemp == 0)
                                    item.BoatReachTimeDuration = 12;
                                else
                                    item.BoatReachTimeDuration = boatTimeDurationTemp + 12;
                                break;
                            case 40:
                                item.BoatActualTimeDuration = 4;
                                if (_windSpeed < 10 || _windSpeed > 30)
                                {
                                    item.BoatReachTimeDuration = 0;
                                }
                                else
                                {
                                    if (boatTimeDurationTemp == 0)
                                        item.BoatReachTimeDuration = 4;
                                    else
                                        item.BoatReachTimeDuration = boatTimeDurationTemp + 4;
                                }

                                break;
                            case 20:
                                item.BoatActualTimeDuration = 2;
                                if (boatTimeDurationTemp == 0)
                                    item.BoatReachTimeDuration = 2;
                                else
                                    item.BoatReachTimeDuration = boatTimeDurationTemp + 2;
                                break;
                            default:
                                break;
                        }
                        if (item.BoatReachTimeDuration != 0)
                            boatTimeDurationTemp = item.BoatReachTimeDuration;
                        incrementCount++;
                    }
                }
                return boatInformations;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetBoatInformationDetails method give error in HarborControlRepository class" + ex.Message);
                throw ex;
            }
        }
        #endregion

    }
}
