using APBD5.Model;
using Microsoft.AspNetCore.Mvc;


namespace APBD5.Controller
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController(IAnimals animalRepo, ILogger<AnimalsController> logger) : ControllerBase
    {
        // GET: /api/animals
        [HttpGet]
        public IActionResult GetAllAnimals(string orderBy = "Name")
        {
            logger.LogInformation("Fetching all animals.");

            if (!IsValidOrderBy(orderBy))
            {
                logger.LogWarning($"Invalid orderBy value: {orderBy}. Defaulting to 'Name'.");
                orderBy = "Name"; // Default to sorting by Name if orderBy is invalid
            }

            var animals = animalRepo.FindAll(orderBy);
            return Ok(animals);
        }

        // POST: /api/animals
        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            if (animal == null)
            {
                return BadRequest("Animal data is missing.");
            }

            logger.LogInformation($"Adding new animal: {animal}");

            animalRepo.Add(animal);
            return Ok("Animal successfully added.");
        }

        // PUT: /api/animals/{idAnimal}
        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal(int idAnimal, [FromBody] Animal animal)
        {
            if (animal == null || idAnimal <= 0)
            {
                return BadRequest("Invalid animal data or ID.");
            }

            logger.LogInformation($"Updating animal with ID: {idAnimal}");

            var updated = animalRepo.Update(idAnimal, animal);

            if (updated)
            {
                return Ok("Animal successfully updated.");
            }
            else
            {
                return NotFound("Animal not found.");
            }
        }

        // DELETE: /api/animals/{idAnimal}
        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            if (idAnimal <= 0)
            {
                return BadRequest("Invalid animal ID.");
            }

            logger.LogInformation($"Deleting animal with ID: {idAnimal}");

            var deleted = animalRepo.Delete(idAnimal);

            if (deleted)
            {
                return Ok("Animal successfully deleted.");
            }
            else
            {
                return NotFound("Animal not found.");
            }
        }

        private bool IsValidOrderBy(string orderBy)
        {

            var validOrders = new List<string> { "Name", "Description", "Category", "Area" };
            return validOrders.Contains(orderBy);
        }
    }
}
