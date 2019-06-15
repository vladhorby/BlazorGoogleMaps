using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleMapsComponents.Maps
{
	public enum PolygonEditOperation
	{
		Move,
		Insert, 
		Remove
	}
	public class PolygonEditEventArgs : MapEventArgs
	{
		public int? Index { get; set; }
		public PolygonEditOperation? Operation { get; set; }
		public int? PathIndex { get; set; }
		public LatLngLiteral LatLng { get; set; }
	}
}
