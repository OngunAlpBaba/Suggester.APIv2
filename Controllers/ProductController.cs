using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Suggester.APIv2.Controllers{

    [Route("[controller]")]
    public class ProductController : ControllerBase{
        private readonly DataContext _dtx;

        public ProductController(DataContext dtx){
            _dtx = dtx;

            var data = _dtx.Suggestions.ToList();
            var size = data.Count;
            for(int i=0; i<size; ++i)
                _dtx.Suggestions.Remove(data[i]);

            var data2 = _dtx.Sessions.ToList();
            size = data2.Count;
            for(int i=0; i<size; ++i)
                _dtx.Sessions.Remove(data2[i]);
        }

        [HttpGet]
        public IActionResult Get(){
            var data = _dtx.Products.OrderBy(c => c.Id);

            return Ok(data);
        }
/*
        // GET product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult Get(int id){
            var data = _dtx.Products.Find(id);

            return Ok(data);
        }
*/
        // GET product/searchText
        [HttpGet("{searchText}", Name = "GetProduct")]
        public IActionResult Get(string searchText){
            try{
                var data = _dtx.Products.Find(Int32.Parse(searchText));
                return Ok(data);
            }
            catch{
                int a = 0;
                int[] indexes = new int[4]; 
                var data = _dtx.Products.OrderBy(c => c.Id).ToList();
                var ses = new Session();
                try{
                    ses.Id = _dtx.Sessions.OrderBy(c => c.Id).Last().Id + 1;
                }
                catch{
                    ses.Id = 0;
                }
                ses.Sid = "0";
                ses.Text  = searchText.ToString();
                ses.Image = null;
                _dtx.Sessions.Add(ses);
                _dtx.SaveChanges();
                for(int i=1; i<17; ++i){
                    if(data[i].Name.ToLower().Contains(searchText.ToLower())){
                        indexes[a] = i;
                        ++a;
                        if(a >= 4){
                            break;
                        }
                    }
                }
                if(a == 0){
                    return Ok(_dtx.Products.Find(0));
                }
                Product[] ret = new Product[a];
                for(int i=0; i<a; ++i){
                    ret[i] = data[indexes[i]];
                }
                return Ok(ret);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product){
            
            if(product == null){
                return BadRequest();
            }

            var ses = new Session();
            try{
                ses.Id = _dtx.Sessions.OrderBy(c => c.Id).Last().Id + 1;
            }
            catch{
                ses.Id = 0;
            }
            ses.Text  = product.Name.ToString();
            ses.Sid = "0";
            ses.Image = product.Image.ToString();
            _dtx.Sessions.Add(ses);
            _dtx.SaveChanges();

            return CreatedAtRoute("GetProduct", new {id = ses.Id}, ses);
        }
    }
}