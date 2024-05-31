using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pokemonapi.Interfaces;
using pokemonapi.Models;
using pokemonapi.DTO;
using pokemonapi.Repositories;

namespace pokemonapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        public readonly IMapper _mapper;
        public readonly IReviewRepository _reviewRepository;
        public readonly IPokemonRepository _pokemonRepository;
        public readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IMapper mapper, IReviewRepository reviewRepository, 
            IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId)) return NotFound();

            if(reviewId == null) return BadRequest(ModelState);

            var MappedReview = _mapper.Map<Review>(_reviewRepository.GetReview(reviewId));
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(MappedReview);
        }

        [HttpGet("/pokemon/{PokemonId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForAPokemon(int PokemonId)
        {
            if (!_reviewRepository.ReviewExists(PokemonId)) return NotFound();

            if(PokemonId == null) return BadRequest(ModelState);

            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewsForAPokemon(PokemonId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDTO reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            //if (reviews != null)
            //{
               // ModelState.AddModelError("", "Review already exists");
               // return StatusCode(422, ModelState);
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);


            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{ReviewId}")]
        [ProducesResponseType(200)]
        public IActionResult UpdateReview([FromBody] ReviewDTO reviewToUpdate, int ReviewId)
        {
            if (reviewToUpdate == null || reviewToUpdate.Id != ReviewId)
                return BadRequest(ModelState);  
            if(!_reviewRepository.ReviewExists(ReviewId)) return NoContent();

            if (!ModelState.IsValid) return BadRequest();

            var ReviewToUpdate = _mapper.Map<Review>(reviewToUpdate);

            if (!_reviewRepository.UpdateReview(ReviewToUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while trying to update the review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ReviewId}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteReview(int ReviewId)
        {
            if (!_reviewRepository.ReviewExists(ReviewId))
                return NoContent();

            if (ReviewId == 0) return BadRequest();

            var ReviewToDelete = _reviewRepository.GetReview(ReviewId);

            if (!_reviewRepository.DeleteReview(ReviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while trying to update the review");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
