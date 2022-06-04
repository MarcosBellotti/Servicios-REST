using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REST.Controllers
{
    public class Animal
    {
        [Required(ErrorMessage ="Campo obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Especie { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaModificacion { get; set; }
        public bool Eliminado { get; set; }
    }


    [RoutePrefix("api/Animal")]
    public class AnimalController : ApiController
    {
        private static List<Animal> ListaCompleta = new List<Animal>()
        {
            new Animal(){ Id = 1, Nombre ="Pato", FechaCreacion = DateTime.Now, Especie="Ave" },
            new Animal(){ Id = 2, Nombre ="Aguila", FechaCreacion = DateTime.Now, Especie="Ave" },
            new Animal(){ Id = 3, Nombre ="Elefante", FechaCreacion = DateTime.Now, Especie="Elephantidae" },
            new Animal(){ Id = 4, Nombre ="Perro", FechaCreacion = DateTime.Now, Especie="Canino" },
            new Animal(){ Id = 5, Nombre ="Gato", FechaCreacion = DateTime.Now, Especie="Felino" },
        };
        // GET api/values
        [Route("filtro")]
        public IHttpActionResult Get(string especie = "", string nombre = "")
        {
            List<Animal> ListaFiltrada = ListaCompleta;
            if (!string.IsNullOrEmpty(especie))
                ListaFiltrada = ListaFiltrada.Where(x => x.Especie.Contains(especie)).ToList();
            if (!string.IsNullOrEmpty(nombre))
                ListaFiltrada = ListaFiltrada.Where(x => x.Nombre.Contains(nombre)).ToList();
            if (ListaCompleta == null)
                return BadRequest();
            return Ok(ListaFiltrada);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
