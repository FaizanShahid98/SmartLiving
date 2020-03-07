﻿
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using demoELiving.Repositires;
using demoELiving.Models;
using Newtonsoft.Json;

namespace demoELiving.Controllers
{
    
    [Route("api/Admin")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly AdminRepositry context;

        public AdminController(AdminRepositry adminRepositry)
        {
            context = adminRepositry;
        }
        [HttpGet]
        public async Task<string> getAllAdminsData()
        {

            var adminData = await context.retriveAllData();
            return JsonConvert.SerializeObject(adminData);
            
        }
         //http://localhost:5000/api/Admin/1       
        [HttpGet("{id}", Name = "AdminProfile")]
        public async Task<string> getAdminData(string id)
        {
            var adminData = await context.retrieve(id);
            if (adminData == null)
                return null;
            return JsonConvert.SerializeObject(adminData);        
        }

        [HttpPost(Name = "AdminRegister")]
        public async Task <bool > registerAdmin([FromBody]Admin admin)//ActionResult<Admin>

        {            
            var adminData = await context.retrieve(admin.adminEmail);
                                

            adminData= JsonConvert.SerializeObject(adminData);
            if (adminData.ToString() == "[]")
            {
                 await context.insert(admin);                 
                 
                 return true;
            }
            
            return false;
        }
         

        //[HttpPut("{adminId},{societyId},{admin}", Name = "UpdateProfileAdmin")]
        [HttpPut("{id}")]
        public async Task <ActionResult> updateAdminProfile( string id ,[FromBody]Admin admin)
         {
            
            if (id != admin.adminEmail )
            {
                return BadRequest();
            }

           await context.update(id, admin);
            return Ok(admin);
        }
    }
}

        //[HttpGet]
        ////http://localhost:5000/api/Admin
        //public ActionResult<List<Admin>> Get()
        //{
        //        return   _bookService.Get();
        //}

        //[HttpGet("{id:length(24)}", Name = "GetAdmin")]
        //public ActionResult<Admin> Get(string id)
        //{
        //    var book = _bookService.GetAdmin(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        //[HttpPost]
        //public ActionResult<Admin> Create([FromBody]Admin book)
        //{
        //    _bookService.Create(book);


        //    return CreatedAtRoute( new { id = book.Id }, book);
        //}

    