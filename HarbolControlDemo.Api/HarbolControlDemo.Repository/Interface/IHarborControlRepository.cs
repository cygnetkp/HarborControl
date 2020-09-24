using HarbolControlDemo.DataModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HarbolControlDemo.Repository.Interface
{
    public interface IHarborControlRepository
    {
        public Task<List<BoatInformation>> GetBoatInformationDetails(int boatCount);
    }
}
