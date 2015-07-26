using System;

using UIKit;

using SemiRoot.MonoTouchHelpers;

namespace ConstraintHelperExample.ViewControllers
{
	public class Base : UIViewController
	{
		public ConstraintHelper ConstraintHelper { get; protected set; }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationController.NavigationBar.Translucent = false;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.White;
			ConstraintHelper = new ConstraintHelper(View);
		}
	}
}

