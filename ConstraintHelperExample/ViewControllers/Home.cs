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

			_buttonSizing = new AutoRoundButton("Sizing") { 
				BackgroundColor = UIColor.FromRGB(210, 210, 210) 
			};
			_buttonPositioning = new AutoRoundButton("Positioning") { 
				BackgroundColor = UIColor.FromRGB(180, 180, 180) 
			};
			_buttonStacking = new AutoRoundButton("Stacking") { 
				BackgroundColor = UIColor.FromRGB(150, 150, 150) 
			};
			_buttonMargin = new AutoRoundButton("Margin") { 
				BackgroundColor = UIColor.FromRGB(120, 120, 120) 
			};
			_buttonAnimation = new AutoRoundButton("Animation") { 
				BackgroundColor = UIColor.FromRGB(90, 90, 90) 
			};

			ConstraintHelper

				.Attach(_buttonSizing)
					.Top(5f).Center(-50f).HeightFromWidth()

				.Attach(_buttonPositioning)
					.Top(5f).Center(50f).HeightFromWidth().StackTop()

				.Attach(_buttonStacking)
					.Top(-32).Center(-42).HeightFromWidth()

				.Attach(_buttonMargin)
					.Top(10f).Center(42f).HeightFromWidth().StackTop()

				.Attach(_buttonAnimation)
					.Top(-18).Center(-25f).HeightFromWidth().StackTop();
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
			NavigationController.PushViewController(new Sizing(), true);
		}

		void HandleButtonPositioning(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Positioning(), true);
		}

		void HandleButtonStacking(object sender, EventArgs e)
		{
			NavigationController.PushViewController(new Stacking(), true);
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

