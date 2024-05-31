using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using pokemonapi.Interfaces;
using pokemonapi.DTO;
using pokemonapi.Controllers;
using pokemonapi.Models;

namespace pokemonapi.Tests.Controller {
  public class PokemonControllerTests {
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public PokemonControllerTests() 
    {
      _pokemonRepository = A.Fake<IPokemonRepository>();
      _reviewRepository = A.Fake<IReviewRepository>();
      _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public void PokemonController_GetPokemons_ReturnOk()
    {
      //Arrange
      var pokemons = A.Fake<ICollection<PokemonDTO>>();
      var pokemonList = A.Fake<List<PokemonDTO>>();
      A.CallTo(() => _mapper.Map<List<PokemonDTO>>(pokemons)).Returns(pokemonList);
      var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);

      //Act

      var result = controller.GetPokemons(); 

      //Assert

      result.Should().NotBeNull();
      result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Fact]
    public void PokemonController_CreatePokemon_ReturnOk(){
      // Arrange
      int ownerId = 1;
      int catId = 2;
      var pokemonMap = A.Fake<Pokemon>();
      var pokemon = A.Fake<Pokemon>();
      var pokemonCreate = A.Fake<PokemonDTO>();
      var pokemons = A.Fake<ICollection<PokemonDTO>>();
      var pokemonList = A.Fake<IList<PokemonDTO>>();
      A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);
      A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemon);
      A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap)).Returns(true);
      var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);

      //Act
      // var result = controller.CreatePokemon(ownerId, catId, pokemonCreate);
      // Assert

      // result.Should().NotBeNull();
    }
  }
}