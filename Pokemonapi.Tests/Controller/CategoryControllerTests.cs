using System;
using pokemonapi.Repositories;
using pokemonapi.Models;
using FluentAssertions;
using FakeItEasy;
using AutoMapper;
using pokemonapi.Interfaces;
using pokemonapi.Controllers;
using pokemonapi.DTO;

namespace pokemonapi.tests
{
  public class CategoryControllerTests
  {
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    public CategoryControllerTests()
    {
      _mapper = A.Fake<IMapper>();
      _categoryRepository = A.Fake<ICategoryRepository>();
    }

    [Fact]
    public void CategoryController_GetCategories_returnOK()
    {
      // arrange
      var categoryDTO = A.Fake<CategoryDTO>();
      var categories = A.Fake<List<CategoryDTO>>();
      var category = A.Fake<ICollection<Category>>();

      A.CallTo(() => _mapper.Map<List<CategoryDTO>>(category)).Returns(categories);
      A.CallTo(() => _categoryRepository.GetCategories()).Returns(category);
      var categoryController = new CategoryController(_categoryRepository, _mapper);
      var result = categoryController.GetCategories();
    
      //Act
      result.Should().NotBeNull();
      //assert
    }

    [Fact]
    public void CategoryController_GetCategory_returnOK()
    {
      // arrange
      var categoryId = 1;
      var category = A.Fake<Category>();
      var categoryDTO = A.Fake<CategoryDTO>();
      var categoryController = new CategoryController(_categoryRepository, _mapper);
      A.CallTo(() => _mapper.Map<CategoryDTO>(category)).Returns(categoryDTO);
      A.CallTo(() => _categoryRepository.GetCategory(categoryId)).Returns(category);
      // Act
      var result = categoryController.GetCategory(categoryId);
      // Assert
      result.Should().NotBeNull();
    }

    [Fact]

    public void CategoryController_CreateCategory_returnOk()
    {
      // arrange
        var categoryDTO = A.Fake<CategoryDTO>();
        var category = A.Fake<Category>();
        A.CallTo(() => _mapper.Map<Category>(categoryDTO)).Returns(category);
        // A.CallTo(() => _categoryRepository.CreateCategory(categoryDTO)).Returns(category); 
      // act
        var categoryController = new CategoryController(_categoryRepository, _mapper);
        var result = categoryController.CreateCategory(categoryDTO);
      // assert
      result.Should().NotBeNull();
    }
  }
}