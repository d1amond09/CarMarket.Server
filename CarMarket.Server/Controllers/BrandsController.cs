﻿using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandsController(IRepositoryManager repository, 
							 ILoggerManager logger, 
							 IMapper mapper) : ControllerBase
{
	private readonly IRepositoryManager _repository = repository;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public IActionResult GetBrands()
	{
		var brands = _repository.Brand.GetAllBrands(trackChanges: false);

		var brandsDto = _mapper.Map<IEnumerable<Brand>>(brands);

		return Ok(brandsDto);
	}

	[HttpGet("{id}", Name = "GetBrandById")]
	public IActionResult GetBrand(Guid id)
	{
		var brand = _repository.Brand.GetBrand(id, trackChanges: false);
		if (brand == null)
		{
			_logger.LogInfo($"Brand with id: {id} doesn't exist in the database.");
			return NotFound();
		}
		else
		{
			var brandDto = _mapper.Map<BrandDto>(brand);
			return Ok(brandDto);
		}
	}

	[HttpPost]
	public IActionResult CreateCarShop([FromBody] BrandForCreationDto brand)
	{
		var brandEntity = _mapper.Map<Brand>(brand);
		_repository.Brand.CreateBrand(brandEntity);
		_repository.Save();
		var brandToReturn = _mapper.Map<BrandDto>(brandEntity);
		return CreatedAtRoute("GetBrandById", new { id = brandToReturn.Id }, brandToReturn);
	}
}