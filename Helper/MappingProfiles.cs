using AutoMapper;
using pokemonapi.DTO;
using pokemonapi.Models;

namespace pokemonapi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            // Mapping for retriving an object.
            CreateMap<Pokemon, PokemonDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Reviewer, ReviewerDTO>();
            //Mapping for creating an object
            CreateMap<CategoryDTO, Category>();
            CreateMap<CountryDTO, Country>();
            CreateMap<OwnerDTO, Owner>();
            CreateMap<PokemonDTO, Pokemon>();
            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewerDTO, Reviewer>();
        }
    }
}
