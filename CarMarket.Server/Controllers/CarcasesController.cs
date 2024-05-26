using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers;

[Route("api/carcases")]
[ApiController]
public class CarcasesController(IRepositoryManager repository, 
							 ILoggerManager logger, 
							 IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public IActionResult Getcarcases()
	{
		var carcases = _repository.Carcase.GetAllCarcases(trackChanges: false);

		var carcasesDto = _mapper.Map<IEnumerable<Carcase>>(carcases);

		return Ok(carcasesDto);
	}

	[HttpGet("{id}", Name = "GetCarcaseById")]
	public IActionResult GetCarcase(Guid id)
	{
		var carcase = _repository.Carcase.GetCarcase(id, trackChanges: false);
		if (carcase == null)
		{
			_logger.LogInfo($"Carcase with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		else
		{
			var carcaseDto = _mapper.Map<CarcaseDto>(carcase);
			return Ok(carcaseDto);
		}
	}

	[HttpPost]
	public IActionResult CreateCarShop([FromBody] CarcaseForUpdateDto Carcase)
	{
		var carcaseEntity = _mapper.Map<Carcase>(Carcase);
		_repository.Carcase.CreateCarcase(carcaseEntity);
		_repository.Save();
		var carcaseToReturn = _mapper.Map<CarcaseDto>(carcaseEntity);
		return CreatedAtRoute("GetCarcaseById", new { id = carcaseToReturn.Id }, carcaseToReturn);
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteCarcase(Guid id)
	{
		var carcase = _repository.Carcase.GetCarcase(id, trackChanges: false);
		if (carcase == null)
		{
			_logger.LogInfo($"Carcase with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_repository.Carcase.DeleteCarcase(carcase);
		_repository.Save();
		return NoContent();
	}

	[HttpPut("{id}")]
	public IActionResult UpdateCarcase(Guid id, [FromBody] CarcaseForUpdateDto carcase)
	{
		if (carcase == null)
		{
			_logger.LogError("CarcaseForUpdateDto object sent from client is null.");
			return BadRequest("CarcaseForUpdateDto object is null");
		}
		var carcaseEntity = _repository.Carcase.GetCarcase(id, trackChanges: true);
		if (carcaseEntity == null)
		{
			_logger.LogInfo($"Carcase with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		_mapper.Map(carcase, carcaseEntity);
		_repository.Save();
		return NoContent();
	}

}
