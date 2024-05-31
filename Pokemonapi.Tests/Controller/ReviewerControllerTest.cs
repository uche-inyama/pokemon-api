using AutoMapper;
using FakeItEasy;
using pokemonapi.Interfaces;

namespace pokemonapi.Tests.Controller {
  public class ReviewerControllerTest 
  {
    private readonly IMapper _mapper;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IPokemonRepository _pokemonRepository;

    public ReviewerControllerTest()
    {
      _mapper = A.Fake<IMapper>();
      _reviewerRepository = A.Fake<IReviewerRepository>();
      _reviewRepository = A.Fake<IReviewRepository>();
      _pokemonRepository = A.Fake<IPokemonRepository>();
    }
  }
}