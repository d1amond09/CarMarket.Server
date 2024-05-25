using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace CarMarket.Server.Formatters.Output;

public class CsvOutputFormatter : TextOutputFormatter
{
	public CsvOutputFormatter()
	{
		SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
		SupportedEncodings.Add(Encoding.UTF8);
		SupportedEncodings.Add(Encoding.Unicode);
	}

	protected override bool CanWriteType(Type? type)
	{
		if (typeof(CarShopDto).IsAssignableFrom(type) ||
		typeof(IEnumerable<CarShopDto>).IsAssignableFrom(type))
		{
			return base.CanWriteType(type);
		}
		return false;
	}

	public override async Task WriteResponseBodyAsync(
		OutputFormatterWriteContext context, 
		Encoding selectedEncoding)
	{
		var response = context.HttpContext.Response;
		var buffer = new StringBuilder();
		if (context.Object is IEnumerable<CarShopDto> carShops)
		{
			foreach (var carShop in carShops)
			{
				FormatCsv(buffer, carShop);
			}
		}
		else if(context.Object is CarShopDto carShop)
		{
			FormatCsv(buffer, carShop);
		}

		await response.WriteAsync(buffer.ToString());
	}
	private static void FormatCsv(StringBuilder buffer, CarShopDto carShop)
	{
		buffer.AppendLine($"{carShop.Id},\"{carShop.Name},\"{carShop.Phone}\"");
	}
}

