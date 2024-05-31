using AutoMapper;
using FakeItEasy;
using pokemonapi.Interfaces;

namespace pokemonapi.Tests.Controllers {
  public class CountryControllerTest {
    private readonly IMapper _mapper;
    private readonly ICountryRepository _countryRepository;
    
    public CountryControllerTest(IMapper mapper, ICountryRepository countryRepository)
    {
      _mapper = A.Fake<IMapper>();
      _countryRepository = A.Fake<ICountryRepository>();
    }
  }
}