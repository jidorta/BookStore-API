using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore_API.Controllers
{

   
    
 

    /// <summary>
    /// Gets Values
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
       

        public HomeController()
        {
           
        }

        ///<summary>
        /// Get a Value
        ///</summary>
        ///<return></return>
        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        ///<summary>
        /// Gets Values
        ///</summary>
        ///<param name="id"></param>
        ///<return></return>
        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
           
            return "value";
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
