using AutoMapper;
using CarMarket.Server.ModelBinders;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CarMarket.Server.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController(IRepositoryManager repository, 
							   ILoggerManager logger, 
							   IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public IActionResult GetCountries()
	{
		var countries = _repository.Country.GetAllCountries(trackChanges: false);

		var countrysDto = _mapper.Map<IEnumerable<Country>>(countries);

		return Ok(countrysDto);
	}

	[HttpGet("{id}", Name = "CountryById")]
	public IActionResult GetCountry(Guid id)
	{
		var country = _repository.Country.GetCountry(id, trackChanges: false);
		if (country == null)
		{
			_logger.LogInfo($"Country with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		else
		{
			var countryDto = _mapper.Map<CountryDto>(country);
			return Ok(countryDto);
		}
	}

	[HttpGet("collection/({ids})", Name = "CountryCollection")]
	public IActionResult GetCountryCollection(
		[ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
	{
		if (ids == null)
		{
			_logger.LogError("Parameter ids is null");
			return BadRequest("Parameter ids is null");
		}
		var countryEntities = _repository.Country.GetByIds(ids, trackChanges: false);
		if (ids.Count() != countryEntities.Count())
		{
			_logger.LogError("Some ids are not valid in a collection");
			return NotFound();
		}
		var countriesToReturn = _mapper.Map<IEnumerable<CountryDto>>(countryEntities);
		return Ok(countriesToReturn);
	}


	[HttpPost]
	public IActionResult CreateCarShop([FromBody] CountryForCreationDto country)
	{
		var countryEntity = _mapper.Map<Country>(country);
		_repository.Country.CreateCountry(countryEntity);
		_repository.Save();
		var countryToReturn = _mapper.Map<CountryDto>(countryEntity);
		return CreatedAtRoute("CountryById", new { id = countryToReturn.Id }, countryToReturn);
	}

	[HttpPost("collection")]
	public IActionResult CreateCountryCollection([FromBody] IEnumerable<CountryForCreationDto> countryCollection)
	{
		if (countryCollection == null)
		{
			_logger.LogError("Country collection sent from client is null.");
			return BadRequest("Country collection is null");
		}
		var countryEntities = _mapper.Map<IEnumerable<Country>>(countryCollection);
		foreach (var Country in countryEntities)
		{
			_repository.Country.CreateCountry(Country);
		}
		_repository.Save();
		var countryCollectionToReturn = _mapper.Map<IEnumerable<CountryDto>>(countryEntities);
		var ids = string.Join(",", countryCollectionToReturn.Select(c => c.Id));
		return CreatedAtRoute("CountryCollection", new { ids }, countryCollectionToReturn);
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteCountry(Guid id)
	{
		var country = _repository.Country.GetCountry(id, trackChanges: false);
		if (country == null)
		{
			_logger.LogInfo($"Country with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_repository.Country.DeleteCountry(country);
		_repository.Save();
		return NoContent();
	}

}
