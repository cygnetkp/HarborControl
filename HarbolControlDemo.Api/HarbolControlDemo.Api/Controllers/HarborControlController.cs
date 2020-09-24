using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarbolControlDemo.DataModels.Models;
using HarbolControlDemo.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HarbolControlDemo.Api.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class HarborControlController : ControllerBase
    {
        #region Variables
        private readonly IHarborControlRepository _harborControlRepository;
        private readonly ILogger _logger;
        #endregion

        public HarborControlController(IHarborControlRepository harborControlRepository,
            ILogger<HarborControlController> logger)
        {
           _harborControlRepository = harborControlRepository;
            _logger = logger;
        }

        #region Public Methods
        /// <summary>
        /// Description: Get all random boat related informations
        /// Created by : Kishan Prajapati
        /// Created On :23/09/2020
        /// </summary>
        /// <param name="boatCount"></param>
        /// <returns>Boat Information List</returns>
        [HttpGet]
        [Route("get-boat-information-details/{boatCount:int}")]
        public async Task<IActionResult> GetBoatInformationDetails(int boatCount)
        {

            List<BoatInformation> boatInformations = null;
            try
            {

                boatInformations = await _harborControlRepository.GetBoatInformationDetails(boatCount);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetBoatInformationDetails method give error in HarborControlController class " + ex.Message);
                return StatusCode(500, "500 Internal Error");
            }
            return Ok(boatInformations);
        }
        #endregion

    }
}
