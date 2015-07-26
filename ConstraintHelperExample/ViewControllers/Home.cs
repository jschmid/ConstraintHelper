using System;
using UIKit;

namespace ConstraintHelperExample.ViewControllers
{
	public class Home : Base
	{
		UIButton _buttonSizing;
		UIButton _buttonPositioning;
		UIButton _buttonStacking;
		UIButton _buttonMargin;
		UIButton _buttonAnimation;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Examples";

			_buttonSizing = new UIButton() { BackgroundColor = UIColor.FromRGB(210, 210, 210) };
			_buttonSizing.SetTitle("Sizing", UIControlState.Normal);

			_buttonPositioning = new UIButton() { BackgroundColor = UIColor.FromRGB(180, 180, 180) };
			_buttonPositioning.SetTitle("Positioning", UIControlState.Normal);

			_buttonStacking = new UIButton() { BackgroundColor = UIColor.FromRGB(150, 150, 150) };
			_buttonStacking.SetTitle("Stacking", UIControlState.Normal);

			_buttonMargin = new UIButton() { BackgroundColor = UIColor.FromRGB(120, 120, 120) };
			_buttonMargin.SetTitle("Margin", UIControlState.Normal);

			_buttonAnimation = new UIButton() { BackgroundColor = UIColor.FromRGB(90, 90, 90) };
			_buttonAnimation.SetTitle("Animation", UIControlState.Normal);

			ConstraintHelper
				.Attach(_buttonSizing)
					.Top().Left().Height(60).Width(120)
				.Attach(_buttonPositioning)
					.Top().LeftOf(_buttonSizing).Right().Height(60).StackTop()
				.Attach(_buttonStacking)
					.Top().Left().WidthOfParent(0.3333f).HeightFromWidth().StackLeft()
				.Attach(_buttonMargin)
					.Top().Left().WidthOf(_buttonStacking).HeightFromWidth().StackLeft()
				.Attach(_buttonAnimation)
					.Top().LeftOf(_buttonMargin).Right().HeightOf(_buttonMargin).StackTop().ResetLeftStack();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			_buttonSizing.TouchUpInside += HandleButtonSizing;
			_buttonPositioning.TouchUpInside += HandleButtonPositioning;
			_buttonStacking.TouchUpInside += HandleButtonStacking;
			_buttonMargin.TouchUpInside += HandleButtonMargin;
			_buttonAnimation.TouchUpInside += HandleButtonAnimation;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			_buttonSizing.TouchUpInside -= HandleButtonSizing;
			_buttonPositioning.TouchUpInside -= HandleButtonPositioning;
			_buttonStacking.TouchUpInside -= HandleButtonStacking;
			_buttonMargin.TouchUpInside -= HandleButtonMargin;
			_buttonAnimation.TouchUpInside -= HandleButtonAnimation;
		}


		void HandleButtonSizing(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Base(), true);
		}

		void HandleButtonPositioning(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Base(), true);
		}

		void HandleButtonStacking(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Base(), true);
		}

		void HandleButtonMargin(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Base(), true);
		}

		void HandleButtonAnimation(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Base(), true);
		}
	}
}

