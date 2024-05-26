using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers;

[Route("api/countries/{countryId}/addresses")]
[ApiController]
public class AddressesController(IRepositoryManager repository, 
							 ILoggerManager logger, 
							 IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public IActionResult GetAddressesForCountry(Guid countryId)
	{
		var country = _repository.Country.GetCountry(countryId, trackChanges: false);
		if (country == null)
		{
			_logger.LogInfo($"Country with id: {countryId} doesn't exist in the database.");
			return NotFound();
		}

		var addressesFromDb = _repository.Address.GetAddresses(countryId, trackChanges: false);

		var addressesDto = _mapper.Map<IEnumerable<AddressDto>>(addressesFromDb);
		return Ok(addressesDto);
	}

	[HttpGet("{id}", Name = "GetAddressById")]
	public IActionResult GetAddress(Guid countryId, Guid id)
	{
		var address = _repository.Address.GetAddress(countryId, id, trackChanges: false);
		if (address == null)
		{
			_logger.LogInfo($"Address with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		else
		{
			var addressDto = _mapper.Map<AddressDto>(address);
			return Ok(addressDto);
		}
	}

	[HttpPost]
	public IActionResult CreateAddress(Guid countryId, [FromBody] AddressForCreationDto address)
	{
		if (address == null)
		{
			_logger.LogError("AddressForCreationDto object sent from client is null.");
			return BadRequest("AddressForCreationDto object is null");
		}

		var country = _repository.Country.GetCountry(countryId, trackChanges: false);
		if (country == null)
		{
			_logger.LogInfo($"Country with id: {countryId} doesn't exist in the database.");
			return NotFound();
		}

		var addressEntity = _mapper.Map<Address>(address);
		_repository.Address.CreateAddress(countryId, addressEntity);
		_repository.Save();
		var addressToReturn = _mapper.Map<AddressDto>(addressEntity);
		return CreatedAtRoute("GetAddressById", new { countryId, id = addressToReturn.Id }, addressToReturn);
	}
}
