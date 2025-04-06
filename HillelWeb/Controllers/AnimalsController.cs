using Microsoft.AspNetCore.Mvc;

namespace HillelWeb.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private static List<Animal> animals = new List<Animal>();

        [HttpGet]
        public ActionResult<IEnumerable<Animal>> GetAllAnimals() => Ok(animals);

        [HttpGet("{id}")]
        public ActionResult<Animal> GetAnimalById([FromRoute] int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            return animal != null ? Ok(animal) : NotFound("Животное не найдено");
        }

        [HttpPost]
        public ActionResult<Animal> AddAnimal([FromQuery] int Id, [FromQuery] string name, [FromQuery] string owner)
        {
            var animal = new Animal { Id = Id, animalName = name, ownerName = owner };
            animals.Add(animal);

            return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAnimal([FromRoute] int id, [FromQuery] string name, [FromQuery] string owner)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound("Животное не найдено");

            animal.animalName = name;
            animal.ownerName = owner;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAnimal([FromRoute] int id)
        {
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return NotFound("Животное не найдено");

            animals.Remove(animal);
            return NoContent();
        }
    }
}
