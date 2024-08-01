using System;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using pokemonapi.Controllers;
using pokemonapi.DTO;
using pokemonapi.Interfaces;
using pokemonapi.Models;

namespace pokemonapi.tests;

public class PokemonControllerTests
{
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
  public void PokemonController_Getpokemons_returnOk()
  {
    // Arrange
     var pokemons = A.Fake<ICollection<PokemonDTO>>();
     var PokemonList = A.Fake<List<PokemonDTO>>(); 
     A.CallTo(() => _mapper.Map<List<PokemonDTO>>(pokemons)).Returns(PokemonList);
     var pokemonController = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);
    // Act
    var result = pokemonController.GetPokemons();
    // Assert
    result.Should().NotBeNull();
    result.Should().BeOfType(typeof(OkObjectResult));
  }

  [Fact]
  public void PokemonController_CreatePokemon_ReturnOk()
  {
    int ownerId = 1;
    int categoryId = 2;
    var pokemonMap = A.Fake<Pokemon>();
    var pokemon = A.Fake<Pokemon>();
    var pokemonCreate = A.Fake<PokemonDTO>();
    var pokemons = A.Fake<ICollection<PokemonDTO>>();
    var pokemonList = A.Fake<IList<PokemonDTO>>();
    A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);
    A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemon);
    A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap)).Returns(true);
    var pokemonController = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);
      //Act
    var result = pokemonController;
      //Assert
    result.Should().NotBeNull();
  }

  [Fact]
  public void PokemonController_UpdatePokemon_ReturnNoContent()
  {
    //Arrange
    int ownerId = 1;
    int categoryId = 2;
    int pokemonId = 2;
    var pokemonDTO = A.Fake<PokemonDTO>();
    var pokemon = A.Fake<Pokemon>();
    var updatePokemon = A.Fake<PokemonDTO>();
    A.CallTo(() => _mapper.Map<Pokemon>(updatePokemon)).Returns(pokemon);
    A.CallTo(() => _pokemonRepository.UpdatePokemon(ownerId, categoryId, pokemon)).Returns(true);
    var pokemonController = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);
    // Act
    var result = pokemonController.UpdatePokemon(ownerId, categoryId, pokemonDTO, pokemonId);
    // Assert
    result.Should().NotBeNull();
  }

  [Fact]
  public void PokemonController_DeletePokemon_ReturnNoContent()
  {
    // Arrange
    var pokemonId = 1;
    var pokemon = A.Fake<Pokemon>();
    var pokemonDTO = A.Fake<PokemonDTO>();
    A.CallTo(() => _mapper.Map<PokemonDTO>(pokemon)).Returns(pokemonDTO);
    A.CallTo(() => _pokemonRepository.PokemonExists(pokemonId));
    A.CallTo(() => _pokemonRepository.DeletePokemon(pokemon)).Returns(true);
    // Act
    var pokemonController = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);
    var result = pokemonController.DeletePokemon(pokemonId);
    // Assert
  }
}