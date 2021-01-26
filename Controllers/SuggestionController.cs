using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Suggester.APIv2.Controllers{

    [Route("[controller]")]
    public class SuggestionController : ControllerBase{
        private readonly DataContext _dtx;

        public SuggestionController(DataContext dtx){
            _dtx = dtx;
        }

        [HttpGet]
        public IActionResult Get(){
            var data = _dtx.Suggestions.Last();
            if(data.IsAnswered){
                return Ok(null);
            }
            return Ok(data);
        }

        // GET suggestion/searchText
        [HttpGet("{id}", Name = "GetSuggestion")]
        public IActionResult Get(int id){
            var data = _dtx.Suggestions.Find(id);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] bool value){
            var data = _dtx.Suggestions.Find(id);
            data.IsAnswered = true;
            data.Like = value;
            _dtx.Suggestions.Update(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Suggestion suggestion){
            
            if(suggestion == null){
                return BadRequest();
            }

            _dtx.Suggestions.Add(suggestion);
            _dtx.SaveChanges();

            return CreatedAtRoute("GetSuggestion", new {id = suggestion.Id}, suggestion);
        }
    }
}