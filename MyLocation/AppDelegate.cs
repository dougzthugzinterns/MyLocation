using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Google.Maps;

namespace MyLocation
{
	
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		MyLocationViewController viewController;
		const string MapsApiKey = "AIzaSyCpAwJUqBgUwTp7kDVzCxX28ODHWg_wHkM";

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			MapServices.ProvideAPIKey (MapsApiKey);
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new MyLocationViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

