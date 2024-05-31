using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pokemonapi.DTO;
using pokemonapi.Interfaces;
using pokemonapi.Models;


namespace pokemonapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IReviewRepository _reviewRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDTO>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpGet("{PokemonId}")]
        [ProducesResponseType(200, Type= typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int PokemonId)
        {
            if(!_pokemonRepository.PokemonExists(PokemonId))
                return NotFound();

            var pokemon = _mapper.Map<PokemonDTO>(_pokemonRepository.GetPokemon(PokemonId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreatePokemon([FromBody] PokemonDTO PokemonParams, [FromQuery] int OwnerId, [FromQuery] int CategoryId)
        {
            if(PokemonParams == null)
                return BadRequest(ModelState);

            var pokemon = _pokemonRepository.GetPokemons()
                .Where(p => p.Name.Trim().ToUpper() == PokemonParams.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon alreay exists");
                return StatusCode(422, ModelState);
            }

            var PokemonMap = _mapper.Map<Pokemon>(PokemonParams);

            if (!_pokemonRepository.CreatePokemon(OwnerId, CategoryId, PokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while trying to create pokemon");
                return StatusCode(500, ModelState);
            }
            return Ok ("Pokemon Successfully created.");
        }

        [HttpPut("{PokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon(
            [FromQuery] int OwnerId,
            [FromQuery] int CategoryId,
            [FromBody] PokemonDTO updatePokemon, int PokemonId)
        {
            if (!_pokemonRepository.PokemonExists(PokemonId)) return NoContent();

            if (updatePokemon.Id != PokemonId) return BadRequest(ModelState);
            if(updatePokemon == null) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest();

            var pokemonMap = _mapper.Map<Pokemon>(updatePokemon);

            if (!_pokemonRepository.UpdatePokemon(OwnerId, CategoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while trying to delete the pokemon");
                StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{PokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int PokemonId)
        {
            if (!_pokemonRepository.PokemonExists(PokemonId))
                return BadRequest(ModelState);

            var pokemonToDelete = _pokemonRepository.GetPokemon(PokemonId);

            if (ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pokemonRepository.DeletePokemon(pokemonToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while tryiny to delete the pokemon");
            }
            return NoContent();
        }
  }
}
