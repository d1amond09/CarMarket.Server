using Contracts;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;

namespace CarMarket.Server.Utility;

public class CarLinks(LinkGenerator linkGenerator, IDataShaper<CarDto> dataShaper)
{
	private readonly LinkGenerator _linkGenerator = linkGenerator;
	private readonly IDataShaper<CarDto> _dataShaper = dataShaper;

	public LinkResponse TryGenerateLinks(IEnumerable<CarDto> carsDto, string fields, Guid carShopId, HttpContext httpContext)
	{
		var shapedCars = ShapeData(carsDto, fields);
		if (ShouldGenerateLinks(httpContext))
			return ReturnLinkedCars(carsDto, fields, carShopId, httpContext, shapedCars);
		return ReturnShapedCars(shapedCars);
	}

	private List<Entity> ShapeData(IEnumerable<CarDto> carsDto, string fields) =>
		_dataShaper.ShapeData(carsDto, fields)
			.Select(e => e.Entity)
			.ToList();

	private bool ShouldGenerateLinks(HttpContext httpContext)
	{
		var mediaType = (MediaTypeHeaderValue) httpContext.Items["AcceptHeaderMediaType"];

		return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
	}

	private LinkResponse ReturnShapedCars(List<Entity> shapedCars) =>
		new() { ShapedEntities = shapedCars };

	private LinkResponse ReturnLinkedCars(IEnumerable<CarDto> carsDto, string fields, 
		Guid carShopId, HttpContext httpContext, List<Entity> shapedCars)
	{
		var carDTOList = carsDto.ToList();

		for (var index = 0; index < carDTOList.Count; index++)
		{
			var carLinks = CreateLinksForCar(httpContext, carShopId, carDTOList[index].Id, fields);
			shapedCars[index].Add("Links", carLinks);
		}

		var carCollection = new LinkCollectionWrapper<Entity>(shapedCars);
		var linkedCars = CreateLinksForCars(httpContext, carCollection);

		return new LinkResponse { HasLinks = true, LinkedEntities = linkedCars };
	}

	private List<Link> CreateLinksForCar(HttpContext httpContext, Guid carShopId, Guid id, string fields = "")
	{
		var links = new List<Link>
			{
				new(_linkGenerator.GetUriByAction(httpContext, "GetCarForCarShop", values: new { carShopId, id, fields }),
				"self",
				"GET"),
				new(_linkGenerator.GetUriByAction(httpContext, "DeleteCarForCarShop", values: new { carShopId, id }),
				"delete_car",
				"DELETE"),
				new(_linkGenerator.GetUriByAction(httpContext, "UpdateCarForCarShop", values: new { carShopId, id }),
				"update_car",
				"PUT"),
				new(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateCarForCarShop", values: new { carShopId, id }),
				"partially_update_car",
				"PATCH")
			};
		return links;
	}

	private LinkCollectionWrapper<Entity> CreateLinksForCars(HttpContext httpContext,
		LinkCollectionWrapper<Entity> CarsWrapper)
	{
		CarsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetCarsForCarShop", values: new { }),
				"self",
				"GET"));

		return CarsWrapper;
	}
}

