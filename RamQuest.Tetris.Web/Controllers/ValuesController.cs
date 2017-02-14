using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace RamQuest.Tetris.Web.Controllers
{
    [Authorize("Execute")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ICommandFactory _commandFactory;
        public ValuesController(ICommandFactory factory)
        {
            _commandFactory = factory;
        }

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
        //[ServiceFilter(typeof(CommandValidationFilter))]
        [ValidateModel]
        public IActionResult Foo([FromBody] CommandBase payload)
        {

            if (ModelState.IsValid)
            {

            }

            var foo = JsonConvert.DeserializeObject(payload.Contract.ToString(), Type.GetType("RamQuest.Tetris.Web.Controllers.Foo"));

            //TOD0: get command and lookup factory
            //var factory = new FooFactory();
            //return Json(factory.Deserialize(payload.Contract.ToString()));

            return new CommandExecutionResult(new {name = "greg", contract = foo});

        }
    }

    public class CommandBase : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Name2 { get; set; }
        [Required]
        public object Contract { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            var service = validationContext.GetService(typeof(ICommandFactory)) as ICommandFactory;

            var contractType = service.GetCommand();
            var foo = JsonConvert.DeserializeObject(Contract.ToString(), Type.GetType(contractType));

            if (foo is IValidatableObject)
            {
                var v = foo as IValidatableObject;
                var errors = v.Validate(validationContext);
                results.AddRange(errors);
            }
            return results;
        }
    }

    public class Foo : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            foreach (var property in this.GetType().GetProperties())
            {
                Validator.TryValidateProperty(property.GetValue(this), new ValidationContext(this, null, null) { MemberName = property.Name }, results);
                if (results.Count > 0)
                {
                    foreach (var err in results)
                    {
                        yield return new ValidationResult(err.ErrorMessage);
                    }
                    results.Clear();
                }
            }

            results.Add(new ValidationResult("Prop1 must be larger than Prop2"));            
        }
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

    public class CommandValidationFilter : ActionFilterAttribute
    {
        private ICommandFactory _commandFactory;
        public CommandValidationFilter(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string s = "";
            base.OnActionExecuting(context);
        }
    }

    public interface ICommandFactory
    {
        string GetCommand();
    }

    public class CommandFactory : ICommandFactory
    {
        public string GetCommand()
        {
            return "RamQuest.Tetris.Web.Controllers.Foo";
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public class ValidationResultModel
    {
        public string Message { get; }

        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status406NotAcceptable;
        }
    }

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            string s = "";
        }
    }

    public class CommandExecutionResult : ObjectResult
    {
        public CommandExecutionResult(object value) : base(value)
        {
            Name = "greg";
        }

        public string Name { get; set; }
    }
}
