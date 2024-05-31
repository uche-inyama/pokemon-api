using AutoMapper;
using FakeItEasy;
using pokemonapi.Interfaces;
using pokemonapi.Models;

namespace pokemonapi.Tests.Controller 
{
  public class OwnerController
  {
    private readonly IMapper _mapper;
    private readonly IOwnerRepository _ownerRepository;

    private readonly ICountryRepository _countryRepository;

    public OwnerController()
    {
      _mapper = A.Fake<IMapper>();
      _ownerRepository = A.Fake<IOwnerRepository>();
      _countryRepository = A.Fake<ICountryRepository>();
    }
  }
}