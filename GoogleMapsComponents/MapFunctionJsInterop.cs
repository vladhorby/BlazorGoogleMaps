using Microsoft.JSInterop;
using GoogleMapsComponents.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMapsComponents
{
    public class MapFunctionJsInterop
    {
        private IJSRuntime _jsRuntime;

        public MapFunctionJsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public Task Init(string id, MapOptions options)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.init",
                id,
                options);
        }

        public Task Dispose(string id)
        {
            return _jsRuntime.InvokeAsync<bool>(
                "googleMapJsFunctions.dispose",
                id);
        }

        public Task FitBounds(string id, LatLngBoundsLiteral bounds)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.fitBounds",
                id,
                bounds);
        }

        public Task PanBy(string id, int x, int y)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.panBy",
                id,
                x,
                y);
        }

        public Task PanTo(string id, LatLngLiteral latLng)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.panTo",
                id,
                latLng);
        }

        public Task PanToBounds(string id, LatLngBoundsLiteral latLngBounds)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.panToBounds",
                id,
                latLngBounds);
        }

        public Task<LatLngBoundsLiteral> GetBounds(string id)
        {
            return _jsRuntime.JsonNetInvokeAsync<LatLngBoundsLiteral>(
                "googleMapJsFunctions.getBounds",
                id);
        }

        public Task<LatLngLiteral> GetCenter(string id)
        {
            return _jsRuntime.JsonNetInvokeAsync<LatLngLiteral>(
                "googleMapJsFunctions.getCenter",
                id);
        }

        public Task SetCenter(string id, LatLngLiteral latLng)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.setCenter",
                id,
                latLng);
        }

        public Task<bool> GetClickableIcons(string id)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.getClickableIcons",
                id);
        }

        public Task SetClickableIcons(string id, bool value)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.setClickableIcons",
                id,
                value);
        }

        public Task<int> GetHeading(string id)
        {
            return _jsRuntime.JsonNetInvokeAsync<int>(
                "googleMapJsFunctions.getHeading",
                id);
        }

        public Task SetHeading(string id, int heading)
        {
            return _jsRuntime.JsonNetInvokeAsync<int>(
                "googleMapJsFunctions.setHeading",
                id,
                heading);
        }

        public async Task<MapTypeId> GetMapTypeId(string id)
        {
            var str = await _jsRuntime.JsonNetInvokeAsync<string>(
                "googleMapJsFunctions.getMapTypeId",
                id);

            Debug.WriteLine($"Get map type {str}");

            return Helper.ToEnum<MapTypeId>(str);
        }

        public Task SetMapTypeId(string id, MapTypeId mapTypeId)
        {
            return _jsRuntime.JsonNetInvokeAsync<int>(
                "googleMapJsFunctions.setMapTypeId",
                id,
                mapTypeId);
        }
        
        public Task<int> GetTilt(string id)
        {
            return _jsRuntime.JsonNetInvokeAsync<int>(
                "googleMapJsFunctions.getTilt",
                id);
        }

        public Task SetTilt(string id, int tilt)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.setTilt",
                id,
                tilt);
        }

        public Task<int> GetZoom(string id)
        {
            return _jsRuntime.JsonNetInvokeAsync<int>(
                "googleMapJsFunctions.getZoom",
                id);
        }

        public Task SetZoom(string id, int zoom)
        {
            return _jsRuntime.JsonNetInvokeAsync<bool>(
                "googleMapJsFunctions.setZoom",
                id,
                zoom);
        }
    }
}
