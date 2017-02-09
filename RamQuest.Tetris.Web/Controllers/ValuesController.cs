using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RamQuest.Tetris.Web.Controllers
{
    [Authorize("Execute")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values        
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("foo")]
        public IActionResult Foo([FromBody] CommandBase payload)
        {
            

            var foo = JsonConvert.DeserializeObject(payload.Contract.ToString(), Type.GetType("RamQuest.Tetris.Web.Controllers"));

            //TOD0: get command and lookup factory
            //var factory = new FooFactory();
            //return Json(factory.Deserialize(payload.Contract.ToString()));

            return Json(foo);
            
        }
    }

    public class CommandBase
    {
        public string Name { get; set; }
        public object Contract { get; set; }
    }

    public class Foo
    {
        public string Bar { get; set; }
    }

    public abstract class Factory<T> where T : class
    {
        public abstract T Deserialize(string payload);
    }

    public class FooFactory : Factory<Foo>
    {
        public override Foo Deserialize(string payload)
        {
            var foo = JsonConvert.DeserializeObject<Foo>(payload);
            return foo;
        }
    }
}
