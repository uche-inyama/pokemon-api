using pokemonapi.Interfaces;
using AutoMapper;
using FakeItEasy;

namespace pokemonapi.Tests.Controller{
  public class CategoryControllerTest {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryControllerTest() 
    {
      _mapper = A.Fake<IMapper>();
      _categoryRepository = A.Fake<ICategoryRepository>();
    }
  }
}