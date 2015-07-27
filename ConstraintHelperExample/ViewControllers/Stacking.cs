using System;
using UIKit;

namespace ConstraintHelperExample.ViewControllers
{
	public class Stacking : Base
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Stacking";

			int amount = 15;

			int max = amount * amount;
			int color = 255;
			int colorSteps = (int) Math.Floor((double)(255 / max));


			// Stacking makes it easier to position elements around each other without needing them to know
			// it works exactly the same way as AboveOf, BelowOf, LeftOf and RightOf

			for (int i = 1; i < max + 1; i++) {
				
				ConstraintHelper
					.Attach(new UIView { BackgroundColor = UIColor.FromRGB(color, color, color) })
					.WidthOfParent((float)1 / amount)
					.HeightFromWidth()
					.Top()
					.Left()
					.StackLeft(); // makes next call of Left behave like LeftOf currrent item
				
				if(i % amount == 0) {
					ConstraintHelper
						.StackTop() // makes next call of Top behave like LeftOf currrent item
						.ResetLeftStack(); // afterwards Left will position according to parents left
				}

				color -= colorSteps;
			}
		}
	}
}

