using System;
using AutoMapper;
using FluentAssertions;
using FakeItEasy;
using pokemonapi.Interfaces;


namespace pokemonapi.tests
{
  public class ReviewControllerTests
  {
    private readonly IMapper _mapper;
    private readonly IReviewRepository _reviewRepository;
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IReviewerRepository _reviewerRepository;

    public ReviewControllerTests(){
      _mapper = A.Fake<IMapper>();
      _reviewerRepository = A.Fake<IReviewerRepository>();
      _pokemonRepository = A.Fake<IPokemonRepository>();
    }
    
  }
}