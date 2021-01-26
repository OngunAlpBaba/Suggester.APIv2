using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Suggester.APIv2.Controllers{

    [Route("[controller]")]
    public class SessionController : ControllerBase{
        private readonly DataContext _dtx;

        public SessionController(DataContext dtx){
            _dtx = dtx;
        }

        [HttpGet]
        public IActionResult Get(){
            var data = _dtx.Products.OrderBy(c => c.Id);
            _dtx.Sessions.RemoveRange(_dtx.Sessions.OrderBy(c => c.Id));
            return Ok(data);
        }
/*
        [HttpGet("{searchText}", Name = "GetProduct")]
        public IActionResult Get(string searchText){
            
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product){
            
        }
        */
    }
}