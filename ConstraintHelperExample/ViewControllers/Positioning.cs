using System;
using UIKit;

namespace ConstraintHelperExample.ViewControllers
{
	public class Positioning : Base
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Positioning";

			var box1 = new UIView { BackgroundColor = UIColor.FromRGB(100, 100, 100) };
			var box2 = new UIView { BackgroundColor = UIColor.FromRGB(100, 100, 100) };
			var box3 = new UIView { BackgroundColor = UIColor.FromRGB(100, 100, 100) };
			var box4 = new UIView { BackgroundColor = UIColor.FromRGB(100, 100, 100) };

			var box5 = new UIView { BackgroundColor = UIColor.FromRGB(100, 100, 100) };

			var box6 = new UIView { BackgroundColor = UIColor.FromRGB(130, 190, 80) };
			var box7 = new UIView { BackgroundColor = UIColor.FromRGB(130, 190, 80) };
			var box8 = new UIView { BackgroundColor = UIColor.FromRGB(130, 190, 80) };
			var box9 = new UIView { BackgroundColor = UIColor.FromRGB(130, 190, 80) };

			var box10 = new UIView { BackgroundColor = UIColor.FromRGB(0, 100, 250) };
			var box11 = new UIView { BackgroundColor = UIColor.FromRGB(0, 100, 250) };
			var box12 = new UIView { BackgroundColor = UIColor.FromRGB(0, 100, 250) };
			var box13 = new UIView { BackgroundColor = UIColor.FromRGB(0, 100, 250) };


			ConstraintHelper

				.Attach(box1)
					.Top() // position to the top of the parent view
					.Left() // position to the left of the parent view
					.Width(50f).Height(50f)

				.Attach(box2)
					.Bottom() // position to the bottom of the parent view
					.Left() // position to the left of the parent view
					.Width(50f).Height(50f)

				.Attach(box3)
					.Top() // position to the top of the parent view
					.Right() // position to the right of the parent view
					.Width(50f).Height(50f)

				.Attach(box4)
					.Bottom() // position to the bottom of the parent view
					.Right() // position to the right of the parent view
					.Width(50f).Height(50f)


				.Attach(box5)
					.Center() // position to the vertical center of the parent view
					.Middle() // position to the horizontal center of the parent view
					.Width(50f).Height(50f)


				.Attach(box6)
					.AboveOf(box5) // position above another view
					.Center()
					.Width(50f).Height(50f)

				.Attach(box7)
					.BelowOf(box5) // position below another view
					.Center()
					.Width(50f).Height(50f)

				.Attach(box8)
					.RightOf(box5) // position on the right of another view
					.Middle()
					.Width(50f).Height(50f)

				.Attach(box9)
					.LeftOf(box5) // position on the left of another view
					.Middle()
					.Width(50f).Height(50f)


				.Attach(box10)
					.AboveOf(box6) 
					.RightOf(box8)
					.Width(50f).Height(50f)

				.Attach(box11)
					.AboveOf(box6) 
					.LeftOf(box9)
					.Width(50f).Height(50f)

				.Attach(box12)
					.BelowOf(box7) 
					.RightOf(box8)
					.Width(50f).Height(50f)

				.Attach(box13)
					.BelowOf(box7) 
					.LeftOf(box9)
					.Width(50f).Height(50f);
		}
	}
}

