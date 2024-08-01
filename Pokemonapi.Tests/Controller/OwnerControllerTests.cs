using System;
using pokemonapi.Repositories;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using pokemonapi.Interfaces;

namespace pokemonapi.tests
{
  public class OwnerControllerTests 
  {
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public OwnerControllerTests()
    {
      _ownerRepository = A.Fake<IOwnerRepository>();
      _countryRepository = A.Fake<ICountryRepository>();
      _mapper = A.Fake<IMapper>();
    }
  }
}