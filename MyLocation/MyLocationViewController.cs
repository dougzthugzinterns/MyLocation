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
			//double latitude = mapView.MyLocation.Coordinate.Latitude;
			//double longitude = mapView.MyLocation.Coordinate.Longitude;

			View = mapView;

			for(int i = 0; i <10; i++){

			addPinToCurrentLocation();

			View = mapView;
			}
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
		public void addPinToCurrentLocation ()
		{
			CLLocation myPosition;;
			myPosition = mapView.MyLocation;
			if (myPosition == null) {
				Console.WriteLine ("Cant resolve location.");
			}else{
				Console.WriteLine ("I got you bitch");
			}
			double mylat = myPosition.Coordinate.Latitude;
			double mylong = myPosition.Coordinate.Longitude;
			CLLocationCoordinate2D myPos = new CLLocationCoordinate2D (mylat, mylong);

			var myPostionMarker = new Marker () {
				Title = "My location",
				Snippet = "",
				Position = myPos,
				Map = mapView
			};

			mapView.SelectedMarker = myPostionMarker;
			Thread.Sleep (10000);
	}
}
}
