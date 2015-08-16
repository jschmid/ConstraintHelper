using System;
using UIKit;

using SR.MonoTouchHelpers;

namespace ConstraintHelperExample.ViewControllers
{
	public class Stacking : Base
	{
    UIScrollView _scrollview;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Stacking";

			int amount = 15;

			int max = amount * amount;
			int color = 255;
			int colorSteps = (int) Math.Floor((double)(255 / max));


      // In this case we work with as scrollview
      _scrollview = new UIScrollView();
      ConstraintHelper.Attach(_scrollview).Top().Right().Bottom().Left();

      // We get us a new constraint helper to work with the scroll view as parent
      var scrollViewConstraintHelper = new ConstraintHelper(_scrollview);

			// Stacking makes it easier to position elements around each other without needing them to know
			// it works exactly the same way as AboveOf, BelowOf, LeftOf and RightOf

			for (int i = 1; i < max + 1; i++) {
				
        scrollViewConstraintHelper
					.Attach(new UIView { BackgroundColor = UIColor.FromRGB(color, color, color) })
					.WidthOfParent((float)1 / amount)
					.HeightFromWidth()
					.Top()
					.Left()
					.StackLeft(); // makes next call of Left behave like LeftOf currrent item
				
				if(i % amount == 0) {
          scrollViewConstraintHelper
						.StackTop() // makes next call of Top behave like LeftOf currrent item
						.ResetLeftStack(); // afterwards Left will position according to parents left
				}

				color -= colorSteps;
			}

      // Since we use a scroll view, it is important to set a bottom constraint 
      // for the last element otherwise scrolling will not work
      scrollViewConstraintHelper.Bottom(); // we still work on the last attached element
		}
	}
}

