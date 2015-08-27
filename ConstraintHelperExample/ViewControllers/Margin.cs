using System;
using UIKit;

using SR.MonoTouchHelpers;

namespace ConstraintHelperExample.ViewControllers
{
	public class Margin : Base
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Margin";

			var box1 = new UIView() { BackgroundColor = UIColor.FromRGB(200, 200, 200) };
			var box2 = new UIView() { BackgroundColor = UIColor.FromRGB(210, 210, 210) };
			var box3 = new UIView() { BackgroundColor = UIColor.FromRGB(220, 220, 220) };

			var box4 = new UIView() { BackgroundColor = UIColor.FromRGB(230, 230, 230) };
			var box5 = new UIView() { BackgroundColor = UIColor.FromRGB(230, 230, 230) };

			var box6 = new UIView() { BackgroundColor = UIColor.FromRGB(240, 240, 240) };
			var box7 = new UIView() { BackgroundColor = UIColor.FromRGB(240, 240, 240) };
			var box8 = new UIView() { BackgroundColor = UIColor.FromRGB(240, 240, 240) };

			var helper = new UIView();

			// Margins are handeld similarly as they are in CSS. Methods for them also follow the same rules.
			// Margins will not collapse and will have affect when you position a an element around another
			ConstraintHelper

				.Attach(box1)
					.Left(10f) // add left margin
					.Right(10f) // add right margin
					.Top(10f) // add top margin
					.Height(40f)
					.StackTop() 

				.Attach(box2)
					.SetMargin(10f) // setting all margin at once
					.Left() // the element still has to be positioned
					.Right()
					.Top()
					.Height(40f)
					.StackTop() 

				.Attach(box3)
				// margin don't collaps, the bottom margin of the previous element takes effect, so no margin top
					.SetMargin(0, 10f) // there is multiple signatures (N, E, S, W), (N&S, E&W), (All the same)
					.Left() // the element still has to be positioned
					.Right()
					.Top()
					.Height(40f)
					.StackTop() 

				// you can modify width and height you inherit of another view
				.Attach(box4)
					.SetMargin(10f, 5f, 0, 10f)
					.Top()
					.Left()
					.WidthOfParent(0.5f, -15f) // left and right margin as modifier
					.Height(45f)

				.Attach(box5)
					.SetMargin(10f, 10f, 0, 5f)
					.Top()
					.Right()
					.WidthOfParent(0.5f, -15f) // left and right margin as modifier
					.Height(45f)
					.StackTop()

				// don't be afraid to use helper views
				.Attach(helper)
					.WidthOfParent(1f, -10f)
					.Top(10f)
					.Left();
			
			(new ConstraintHelper(helper))
				.Attach(box6)
					.SetMargin(0, 0, 0, 10f)
					.Top()
					.Left().StackLeft()
					.WidthOfParent(0.3333f, -10f)
					.Height(45f)

				.Attach(box7)
					.SetMargin(0, 0, 0, 10f)
					.Top()
					.Left().StackLeft()
					.WidthOfParent(0.3333f, -10f)
					.Height(45f)

				.Attach(box8)
					.SetMargin(0, 0, 0, 10f)
					.Top()
					.Left().StackLeft()
					.WidthOfParent(0.3333f, -10f)
					.Height(45f)
					// make it a habit of pinning the last element to the bottom
					// if the parents height is not predefined (specialy if you use scroll views!)
					.Bottom();
		}
	}
}

