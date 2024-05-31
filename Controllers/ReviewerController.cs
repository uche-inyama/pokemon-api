using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pokemonapi.DTO;
using pokemonapi.Interfaces;
using pokemonapi.Models;
using pokemonapi.Repositories;

namespace pokemonapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewerController : Controller
    {
        public readonly IMapper _mapper;
        public readonly IReviewerRepository _reviewerRepository;

        public ReviewerController(IMapper mapper, IReviewerRepository reviewerRepository)
        {
            _mapper = mapper;
            _reviewerRepository = reviewerRepository;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody]ReviewerDTO ReviewerParams)
        {
            if (ReviewerParams == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers().Where(r => r.FirstName == ReviewerParams.FirstName).FirstOrDefault();

            if (reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            var ReviewerMap = _mapper.Map<Reviewer>(ReviewerParams);

            if (!_reviewerRepository.CreateReviewer(ReviewerMap))
            {
                ModelState.AddModelError("", "Object already exist");
                return StatusCode(500, ModelState);
            }

            return Ok("Reviewer added successfully");
        }

        [HttpGet("{ReviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewer(int ReviewerId)
        {
            var reviewer = _mapper.Map<ReviewerDTO>(_reviewerRepository.GetReviewer(ReviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reviewer);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDTO>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewers);
        }

        [HttpGet("/reviewer/reviews{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var reviews = _mapper.Map<List<Review>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateReviewer([FromBody] ReviewerDTO updatedReviewer, int reviewerId)
        {
            if (updatedReviewer == null || reviewerId != updatedReviewer.Id)
                return BadRequest(ModelState);

            if (!_reviewerRepository.ReviewerExists(reviewerId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong trying to update the reviewer");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if(!_reviewerRepository.ReviewerExists(reviewerId)) return NotFound();

            var ReviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reviewerRepository.DeleteReviwer(ReviewerToDelete))
                ModelState.AddModelError("", "Something went wrong while trying to delete the Reviewer.");

            return NoContent();
        }
    }
}
