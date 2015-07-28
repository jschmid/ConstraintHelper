using System;
using UIKit;

namespace ConstraintHelperExample.ViewControllers
{
	public class Animation : Base
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Animation";

			var box1 = new UIView { BackgroundColor = UIColor.FromRGB(250, 250, 250) };
			var box2 = new UIView { BackgroundColor = UIColor.FromRGB(10, 10, 10) };
			var box3 = new UIView { BackgroundColor = UIColor.FromRGB(240, 240, 240) };
			var box4 = new UIView { BackgroundColor = UIColor.FromRGB(240, 240, 240) };


			// set constraints as usual
			ConstraintHelper

				.Attach(box1)
					.Top().Left().Right().Height(1f)
				.Attach(box2)
					.Width(10f).Height(10f).Center().Middle()
				.Attach(box3)
					.Top(20f).Left().Height(40f).Width(0f)
				.Attach(box4)
					.Bottom(20f).Right().Height(40f).Width(0f)

				// layout constraints (equal to LayoutIfNeeded)
				.UpdateConstraints();


			UIView.Animate(0.8, 0, UIViewAnimationOptions.CurveEaseOut, 
				() => {

					box1.BackgroundColor = UIColor.FromRGB(10, 10, 10);
					box2.BackgroundColor = UIColor.FromRGB(128, 128, 128);
					box2.Layer.CornerRadius = 75f;

					// change the constraints
					ConstraintHelper

						.WorkWith(box1)
							.RemoveHeightConstraint().HeightOfParent(0.5f)
						.WorkWith(box2)
							.RemoveWidthConstraint().Width(150f)
							.RemoveHeightConstraint().Height(150f)
						.WorkWith(box3)
							.RemoveWidthConstraint().WidthOfParent(0.8f)
						.WorkWith(box4)
							.RemoveWidthConstraint().WidthOfParent(0.8f)

						// layout constraints again (equal to LayoutIfNeeded)
						.UpdateConstraints();
					
				}, null
			);
		}
	}
}

