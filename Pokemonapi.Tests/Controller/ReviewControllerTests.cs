using AutoMapper;
using FakeItEasy; 
using pokemonapi.Interfaces;

namespace pokemonapi.Tests.Controller
{
  public class ReviewControllerTest
  {
    private readonly IMapper _mapper;
    private readonly IReviewRepository _reviewRepository;
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IReviewerRepository _reviewerRepository;
   
    public ReviewControllerTest()
    {
      _mapper = A.Fake<IMapper>();
      _reviewRepository = A.Fake<IReviewRepository>();
      _reviewerRepository = A.Fake<IReviewerRepository>();
      _reviewRepository = A.Fake<IReviewRepository>();
    }
  }
}