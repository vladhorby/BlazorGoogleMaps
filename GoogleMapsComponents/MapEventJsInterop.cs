using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoogleMapsComponents
{
	public class MapEventJsInterop
	{
		private static readonly IDictionary<Guid, Delegate> registeredEvents
				= new ConcurrentDictionary<Guid, Delegate>();

		private IJSRuntime _jsRuntime;

		public MapEventJsInterop(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public async Task<Guid> SubscribeMapEvent(string mapId, string eventName, Action<JObject> action)
		{
			var guid = Guid.NewGuid();

			await _jsRuntime.InvokeAsync<bool>(
					"googleMapEventJsFunctions.addListener",
					guid,
					mapId,
					eventName);

			registeredEvents.Add(guid, action);

			return guid;
		}

		public async Task<Guid> SubscribeMapEventOnce(string mapId, string eventName, Action<JObject> action)
		{
			var guid = Guid.NewGuid();

			await _jsRuntime.InvokeAsync<bool>(
					"googleMapEventJsFunctions.addListenerOnce",
					guid,
					mapId,
					eventName);

			registeredEvents.Add(guid, action);

			return guid;
		}

		public async Task UnsubscribeMapEvent(string guid)
		{
			await _jsRuntime.JsonNetInvokeAsync<bool>(
					"googleMapEventJsFunctions.removeListener",
					guid);
		}

		[JSInvokable]
		public static Task NotifyMapEvent(string guidString, string eventArgs)
		{
			var guid = new Guid(guidString);

			if (eventArgs == null)
			{
				registeredEvents[guid].DynamicInvoke(null);
			}
			else
			{
				registeredEvents[guid].DynamicInvoke(JObject.Parse(eventArgs));
			}

			return Task.FromResult(true);
		}

		public async Task<Guid> SubscribeMarkerEvent(
				string markerGuid,
				string eventName,
				Action<JObject> action)
		{
			var eventGuid = Guid.NewGuid();

			await _jsRuntime.InvokeAsync<bool>(
					"googleMapEventJsFunctions.addMarkerListener",
					eventGuid,
					markerGuid,
					eventName);

			registeredEvents.Add(eventGuid, action);

			return eventGuid;
		}
		public async Task<Guid> SubscribePolygonEvent<TEventArgs>(
			string markerGuid,
			string eventName,
			Action<TEventArgs> action)
		{
			var eventGuid = Guid.NewGuid();

			await _jsRuntime.InvokeAsync<bool>(
																				"googleMapEventJsFunctions.addPolygonListener",
																				eventGuid,
																				markerGuid,
																				eventName);

			registeredEvents.Add(eventGuid, action);

			return eventGuid;
		}
		//public static async Task UnsubscribeMarkerEvent(string guid)
		//{
		//    await Helper.MyInvokeAsync<bool>(
		//        "googleMapEventJsFunctions.removeMarkerListener",
		//        guid);
		//}

		[JSInvokable]
		public static Task NotifyMarkerEvent(string guidString, string eventArgs)
		{
			var guid = new Guid(guidString);
			if (!registeredEvents.ContainsKey(guid))
				return Task.FromResult(false);

			var registeredEvent = registeredEvents[guid];
			var handlerType = registeredEvent.GetType();
			if (handlerType.IsGenericType && handlerType.GenericTypeArguments.Length == 1)
			{
				var genericType = handlerType.GenericTypeArguments[0];
				var typedArgs = JsonConvert.DeserializeObject(eventArgs, genericType);
				registeredEvent.DynamicInvoke(typedArgs);
			}
			else
			{
				registeredEvent.DynamicInvoke(JObject.Parse(eventArgs));
			}

			return Task.FromResult(true);
		}
	}
}
