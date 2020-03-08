
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using demoELiving.Repositires;
using demoELiving.Models;
using Newtonsoft.Json;

namespace demoELiving.Controllers
{
    
    [Route("api/Property")]
    [ApiController]

    public class PropertyController : ControllerBase
    {
        private readonly PropertyRepositry context;

        public PropertyController(PropertyRepositry propertyRepositry)
        {
            context = propertyRepositry;
        }
        [HttpGet("{societyId}", Name = "allPropertyProfiles")]
        public async Task<string> getAllPropertiesData(string societyId)
        {
            var propertyData = await context.retrieveAll(societyId);
            return JsonConvert.SerializeObject(propertyData);
            
        }
              
        [HttpGet("{propertyId}/{all}", Name = "propertyProfile")]
        public async Task<string> getPropertyData(string propertyId,string all)
        {
            var propertyData = await context.retrieve(propertyId);
            if (propertyData == null)
                return null;
            return JsonConvert.SerializeObject(propertyData);        
        }

        [HttpPost(Name = "propertyRegister")]
        public async Task <bool > registerProperty([FromBody]Property property)//ActionResult<Admin>

        {            
            var propertyData = await context.retrieve(property.propertyId);                                
            propertyData= JsonConvert.SerializeObject(propertyData);
            if (propertyData.ToString() == "[]")
            {
                 await context.insert(property);                                  
                 return true;
            }            
            return false;
        }                 
        [HttpPut("{id}")]
        public async Task <ActionResult> updateAdminProfile([FromBody]Property property)
         {                        
           await context.update(property.propertyId, property);
            return Ok(property);
        }
    }
}