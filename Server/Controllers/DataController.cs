using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    using Core;

    using TransportModels;

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public DataController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Data>> Get()
        {
            return Ok(_dataRepository.GetDataSet());
        }

        [HttpPost]
        public void Post([FromBody] Data data)
        {
            _dataRepository.Save(data);
        }
    }
}
