using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using Google.Maps;
using System.Threading;

namespace MyLocation
{
	public partial class MyLocationViewController : UIViewController
	{
		MapView mapView;
		double myLat;
		double myLong;
		double tempLat;
		double tempLong;
		bool firstLocationUpdate;

		public MyLocationViewController () : base ("MyLocationViewController", null)
		{

		}

		public override void LoadView ()
		{
			base.LoadView ();

			var camera = CameraPosition.FromCamera (latitude: 37.797865, 
			                                        longitude: -122.402526, 
			                                        zoom: 2);
	
			mapView = MapView.FromCamera (RectangleF.Empty, camera);
			
			mapView.MyLocationEnabled = true;

			if(mapView.MyLocation != null){
				myLat = mapView.MyLocation.Coordinate.Latitude;
				myLong = mapView.MyLocation.Coordinate.Longitude;
				moveCameraToLocation (myLat, myLong, 4, mapView);
			}else{
				tempLat = 25.7216;
				tempLong = -80.2793;
				moveCameraToLocation (tempLat, tempLong, 4, mapView);
			}
			// Listen to the myLocation property of GMSMapView.
			mapView.AddObserver (this, new NSString ("myLocation"), NSKeyValueObservingOptions.New, IntPtr.Zero);                                          
			View = mapView;
		
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			mapView.StartRendering ();
		}

		public override void ViewWillDisappear (bool animated)
		{	
			mapView.StopRendering ();
			base.ViewWillDisappear (animated);
		}
		public override void ObserveValue (NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			//base.ObserveValue (keyPath, ofObject, change, context);

			if (!firstLocationUpdate) {
				// If the first location update has not yet been recieved, then jump to that
				// location.
				firstLocationUpdate = true;
				var location = change.ObjectForKey (NSValue.ChangeNewKey) as CLLocation;
				mapView.Camera = CameraPosition.FromCamera (location.Coordinate, 14);
			}
		}

		//adds a pin to a location by specifying the latitude and longitude
		public void addPinToLocation (double latitude, double longitude, String title, String snippet, MapView map){
			CLLocationCoordinate2D coord = new CLLocationCoordinate2D (latitude, longitude);
			var marker = new Marker () {
				Title = title,
				Snippet = snippet,
				Position = coord,
				Map = map
			};
		}
		//moves the camera of a map to a location by specifying the latitude and longitude
		public void moveCameraToLocation (double latitude, double longitude, float zoom, MapView map){
			map.MoveCamera (CameraUpdate.SetCamera(CameraPosition.FromCamera( latitude, longitude, zoom)));
		}
	}
}
