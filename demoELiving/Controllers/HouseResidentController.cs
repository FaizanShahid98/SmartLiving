﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using demoELiving.Repositires;
using Newtonsoft.Json;
using demoELiving.Models;

namespace demoELiving.Controllers
{
    [Route("api/HouseResident")]
    [ApiController]
    public class HouseResidentController : ControllerBase
    {
        private HouseResidentRepositry context = null;
        public HouseResidentController(HouseResidentRepositry residentRepositry) {
            context = residentRepositry;

        }
        [HttpGet(Name = "AllResidentsData")]
        public async Task<string> getAllResidentsData()//all residents of each housing society
        {
            var residentData = await context.retriveAllData();
            return JsonConvert.SerializeObject(residentData);            
        }

        [Route("[action]/{societyId}")]
        [HttpGet("{societyId}", Name = "AllHouseResidentData")]
        public async Task<string> getAllHouseResidentData(string societyId)
        {
            var adminData = await context.retrieveAll(societyId);
            return JsonConvert.SerializeObject(adminData);
        }

        [Route("[action]/{email}")]
        [HttpGet("{email}/{societyId}", Name = "houseResidentProfile")]
        public async Task<string> getHouseResidentProfile(string email,string societyId)
        {

            var houseResidentData = await context.retrieve(email);
            if (houseResidentData == null)
                return null;
            return JsonConvert.SerializeObject(houseResidentData);
        }

        [HttpPost(Name = "registerHouseResident")]
        public async Task <bool> registerHouseResident([FromBody]HouseResident houseResident)
        {                                      
        var residentData = await context.retrieve(houseResident.email);                                
            residentData= JsonConvert.SerializeObject(residentData);
            if (residentData.ToString() == "[]")
            {
                 await context.insert(houseResident);                 
                 
                 return true;
            }
            
            return false;

        }
        [HttpPut( Name = "updateProfilehouseResident") ]
        public async Task <ActionResult> updateAdminProfile([FromBody] HouseResident houseResident)
        {
            
            await context.update(houseResident.email, houseResident);
            return NoContent();        
        }
    }
}