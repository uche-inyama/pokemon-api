using System;
using pokemonapi.Interfaces;
using FluentAssertions;
using FakeItEasy;
using AutoMapper;

namespace pokemonapi.tests
{
  public class ReviewerControllerTests
  {
    private readonly IMapper _mapper;
    private readonly IReviewerRepository _reviewerRepository;
    public ReviewerControllerTests()
    {
      _mapper = A.Fake<IMapper>();
      _reviewerRepository = A.Fake<IReviewerRepository>();
    }
  }
}