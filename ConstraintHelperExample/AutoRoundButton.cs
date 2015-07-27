using System;

using UIKit;

namespace ConstraintHelperExample
{
	public class AutoRoundButton : UIButton
	{
		public AutoRoundButton(string title)
		{
			SetTitle(title, UIControlState.Normal);
			ContentEdgeInsets = new UIEdgeInsets(10f, 10f, 10f, 10f);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			Layer.CornerRadius = Frame.Size.Width / 2;
		}
	}
}

