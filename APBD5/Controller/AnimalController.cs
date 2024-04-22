using Microsoft.AspNetCore.Mvc;


namespace APBD5.Controller
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimals _animalRepo;
        private readonly ILogger<AnimalsController> _logger;

        public AnimalsController(IAnimals animalRepo, ILogger<AnimalsController> logger)
        {
            _animalRepo = animalRepo;
            _logger = logger;
        }

        // GET: /api/animals
        [HttpGet]
        public IActionResult GetAllAnimals(string orderBy = "Name")
        {
            _logger.LogInformation("Fetching all animals.");

            if (!IsValidOrderBy(orderBy))
            {
                _logger.LogWarning($"Invalid orderBy value: {orderBy}. Defaulting to 'Name'.");
                orderBy = "Name"; // Default to sorting by Name if orderBy is invalid
            }

            var animals = _animalRepo.FindAll(orderBy);
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

            _logger.LogInformation($"Adding new animal: {animal}");

            _animalRepo.Add(animal);
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

            _logger.LogInformation($"Updating animal with ID: {idAnimal}");

            var updated = _animalRepo.Update(idAnimal, animal);

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

            _logger.LogInformation($"Deleting animal with ID: {idAnimal}");

            var deleted = _animalRepo.Delete(idAnimal);

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
