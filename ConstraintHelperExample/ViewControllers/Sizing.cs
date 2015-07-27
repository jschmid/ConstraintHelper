using System;
using UIKit;

namespace ConstraintHelperExample.ViewControllers
{
	public class Sizing : Base
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Sizing";

			var box1 = new UIView { BackgroundColor = UIColor.FromRGB(100, 180, 100) };
			var box2 = new UIView { BackgroundColor = UIColor.FromRGB(100, 180, 150) };
			var box3 = new UIView { BackgroundColor = UIColor.FromRGB(100, 180, 200) };
			var box4 = new UIView { BackgroundColor = UIColor.FromRGB(140, 140, 140) };
			var box5 = new UIView { BackgroundColor = UIColor.FromRGB(160, 160, 160) };
			var box6 = new UIView { BackgroundColor = UIColor.FromRGB(180, 180, 180) };

			ConstraintHelper
				.Attach(box1)
					.Width(80f).Height(100f) // setting size by fixed values
					.Top().Left()

				.Attach(box2)
					.WidthOf(box1).HeightOf(box1) // setting size to the same of another element
					.Top().LeftOf(box1)
					
				.Attach(box3)
					.WidthOf(box2, 2f).HeightOf(box2) // setting size to the same of another element with modifier
					.Top().LeftOf(box2).StackTop()

				.Attach(box4)
					.WidthOfParent(0.25f) // setting sizes of parent containers with modifier
					.HeightFromWidth() // use views height as width
					.Top().Left()

				.Attach(box5)
					.WidthOfParent(0.5f)
					.HeightFromWidth() // use views width as height
					.Top().LeftOf(box4)

				.Attach(box6)
					.HeightOf(box4)
					.WidthFromHeight()
					.Top().Right();
		}
	}
}

