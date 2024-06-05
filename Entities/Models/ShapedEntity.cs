using System;
namespace Entities.Models;

public class ShapedEntity
{
	public Entity Entity { get; set; }
	public ShapedEntity()
	{
		Entity = [];
	}
}