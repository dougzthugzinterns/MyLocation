using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Google.Maps;

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
			                                        zoom: 6);
			mapView = MapView.FromCamera (RectangleF.Empty, camera);
			mapView.MyLocationEnabled = true;
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
	}
}

