
window.googleMapPolygonJsFunctions = {
	init: function (jsonArgs) {
		let args = JSON.parse(jsonArgs);
		let guid = args[0];
		let options = args[1];

		console.log("Init polygon" + guid);
		//console.dir(options);

		if (options.map !== null && typeof options.map !== 'undefined')
			options.map = window._blazorGoogleMaps[options.map];

		window._blazorGoogleMapsMarkers = window._blazorGoogleMapsMarkers || [];
		window._blazorGoogleMapsMarkers[guid] = new google.maps.Polygon(options);

		return true;
	},

	dispose: function (guid) {
		let polygon = window._blazorGoogleMapsMarkers[guid];
		polygon.setMap(null);
		delete window._blazorGoogleMapsMarkers[guid];

		return true;
	},

	invoke: function (guid, methodName, jsonArgs) {
		let args = JSON.parse(jsonArgs);
		let polygon = window._blazorGoogleMapsMarkers[guid];

		//console.log("Invoke " + methodName);
		//console.dir(polygon);

		if (typeof args === 'undefined') {
			return polygon[methodName]();
		} else {
			return polygon[methodName](...args);
		}
	},

	setMap: function (jsonArgs) {
		let args = JSON.parse(jsonArgs);
		let guid = args[0];
		let mapId = args[1];

		let marker = window._blazorGoogleMapsMarkers[guid];
		let map = null;

		if (mapId !== null && typeof mapIdp !== 'undefined')
			map = window._blazorGoogleMaps[mapId];

		marker.setMap(map);
		return true;
	}
};