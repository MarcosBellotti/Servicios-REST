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
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Especie { get; set; }
        public DateTime FechaCreacion { get; set; }        
        public DateTime FechaModificacion { get; set; }
        internal bool Eliminado { get; set; }
    }


    [RoutePrefix("api/Animal")]
    public class AnimalController : ApiController
    {

        private static List<Animal> ListaCompleta = new List<Animal>()
        {
            new Animal(){ Id = 1, Nombre ="Pato", Especie="Ave", FechaCreacion = DateTime.Now },
            new Animal(){ Id = 2, Nombre ="Aguila", Especie="Ave", FechaCreacion = DateTime.Now  },
            new Animal(){ Id = 3, Nombre ="Elefante", Especie="Elephantidae", FechaCreacion = DateTime.Now  },
            new Animal(){ Id = 4, Nombre ="Perro", Especie="Canino", FechaCreacion = DateTime.Now  },
            new Animal(){ Id = 5, Nombre ="Gato", Especie="Felino", FechaCreacion = DateTime.Now  },
        };

        [Route("obtenerLista")]
        public IHttpActionResult Get(string especie = "", string nombre = "")
        {
            List<Animal> ListaFiltrada = ListaCompleta.Where(x=>x.Eliminado == false).ToList();

            if (!string.IsNullOrEmpty(especie))
                ListaFiltrada = ListaFiltrada.Where(x => x.Especie.Contains(especie)).ToList();
            if (!string.IsNullOrEmpty(nombre))
                ListaFiltrada = ListaFiltrada.Where(x => x.Nombre.Contains(nombre)).ToList();
            if (ListaCompleta == null)
                return BadRequest();
            return Ok(ListaFiltrada);
        }

        [Route("obtenerAnimal/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (ListaCompleta.FirstOrDefault(x => x.Id == id) != null)
                return Ok(ListaCompleta.FirstOrDefault(x => x.Id == id));
            return NotFound();
        }

        [Route("cargarAnimal/{especie}/{nombre}")]
        public IHttpActionResult Post(string especie, string nombre )
        {
            var animal = new Animal();
            animal.Especie = especie;
            animal.Nombre = nombre;
            animal.Id = ListaCompleta.Last().Id + 1;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            ListaCompleta.Add(animal);

            return Ok(animal);
        }

        [Route("putAnimal/{id}/{nombre}/{especie}")]
        public IHttpActionResult Put(int id, string nombre, string especie)
        {
            var animal = ListaCompleta.FirstOrDefault(x => x.Id == id);
            animal.Especie = especie;
            animal.Nombre = nombre;
            animal.FechaModificacion = DateTime.Now;
            return Ok(animal);
        }

        [Route("deleteAnimal/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var animal = ListaCompleta.FirstOrDefault(x => x.Id == id);
            animal.Eliminado = true;

            return Ok(animal);
        }
    }
}
