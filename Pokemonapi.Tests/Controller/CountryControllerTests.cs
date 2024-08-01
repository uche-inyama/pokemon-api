using System;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using pokemonapi.Data;
using pokemonapi.Controllers;
using pokemonapi.Models;
using pokemonapi.DTO;
using pokemonapi.Interfaces;
using pokemonapi.Repositories;



namespace pokemonapi.tests 
{
  public class CountryControllerTest
  {
    private readonly ICountryRepository _countryRepository;    
    private readonly IMapper _mapper;

    public CountryControllerTest()
    {
      _mapper = A.Fake<IMapper>();
      _countryRepository = A.Fake<ICountryRepository>();
    }
  }
}