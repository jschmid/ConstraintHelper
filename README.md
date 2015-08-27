# ConstraintHelper

## Introduction

This ConstraintHelper library aims to make developers life easier when it comes to creating layout constraints with Xamarin.iOS. 
It makes heavy use of method chaining to make writing the constraints more efficient.

There are lots of example in the example project. Take a closer look at the different ViewControllers.

##  Usage
The primary functionality is provided by the ConstraintHelper object.
Create one for each view you want to add subviews to.

```
var constraintHelper = new ConstraintHelper(View);
```

To add a subview use the ``Attach()`` method. 
Whenever you use the Attach method the given view will be set to be used for all further method calls 
(like Top, Width, Middle etc.) until you call either the ``Attach()`` or ``WorkWith()`` method. The Attach
 method will take care of adding the view as subview and setting autolayout options.

```
var constraintHelper = new ConstraintHelper(View);

constraintHelper
	.Attach(new UIView { BackgroundColor = UIColor.Black })
		.Top() // position to the top of the parent view
		.Left() // position to the left of the parent view
		.Width(50f).Height(50f)
	.Attach(new UIView { BackgroundColor = UIColor.Black })
		.Bottom() // position to the bottom of the parent view
		.Right() // position to the right of the parent view
		.Width(50f).Height(50f);
```

### Positioning

Basic positioning can be done with the methods ``Top()``, ``Right()``, ``Bottom()``, ``Left()``.

These methods create a layout constraint between the subview and the parent view, unless stacking has been used (read more in the stacking section).

All of these methods will take an optional margin argument (float). 
The margin equals the constant attribute of a layout constraint.

Positioning on an axis is done with the ``Center()`` and ``Middle()`` methods.

Center() centers on the X axis, Middle on the Y axis. Both methods take an optional modifier (equals the constant attribute of a layout constraint).

It is also possible to position elements after another element using the methods ``AboveOf()``, ``BelowOf()``, ``LeftOf()`` and ``RightOf()`` you can create a constraint matching one side of an element to the one of the next. Example:

```
var constraintHelper = new ConstraintHelper(View);

var box1 = new UIView();
var box2 = new UIView();
var box3 = new UIView();
var box4 = new UIView();
var box5 = new UIView();

constraintHelper
	.Attach(box1)
		.Top().Left().Width(50f).Height(50f)
	.Attach(box2)
		.BelowOf(box1) 
		.LeftOf(box1)
		.Width(50f).Height(50f)
	.Attach(box3)
		.BelowOf(box2) 
		.LeftOf(box2)
		.Width(50f).Height(50f)
	.Attach(box4)
		.BelowOf(box3) 
		.LeftOf(box3)
		.Width(50f).Height(50f)
```

As you probably reckoned, this will create 4 boxes, 50 by 50 in size, that are aligned diagonally down from the top left (ok, ok , it's not the most real life scenario).

These methods will take into account the margins of the elements. 
If there is a bottom margin of the first and a top margin of the second element,
the second element below will be positioned taking both margins into account - margins do not collapse.


## Sizing

The main methods for sizing are ``Width()``, ``WidthOf()``, ``WidthOfParent()``, ``Height()``, ``HeightOf()`` and ``HeightOfParent()``.
While Width() and Height() are good for setting fixed values, all HeighOf and WidthOf methods are good for percental sizes in relation to another view.

As with most of the methods creating constraints between two view, there are optional multiplier and modifier arguments.

In addition there is ``WidthFromHeight()`` and ``HeightFromWidth``. These create a constraint from one to the other value on the same view. It's an easy way to create flexible squares etc.


```
var constraintHelper = new ConstraintHelper(View);

var box1 = new UIView ();
var box2 = new UIView ();
var box3 = new UIView ();

constraintHelper
	.Attach(box1)
		.Width(50f).Width(50f)// setting size by fixed values
		.Top().Left()

	.Attach(box2)
		.WidthOf(box1, 2f).HeightOf(box1, 0.5f) // Sets the width to 100 and the height to 25
		.Top().LeftOf(box1)
		
	.Attach(box3)
		.WidthOfParent(0.25f) // setting sizes of parent containers with modifier
		.HeightFromWidth() // use views height as width
		.BelowOf(box1).Left();
```


## Stacking

``BelowOf()``, ``RightOf()``, ``AboveOf()`` and ``LeftOf()`` are great methods to place a view after another, but you have to know each of the views.


Another way is to use stacks. There are 4 stacks, one for each side. 
Using ``StackTop()``, ``StackRight()``, ``StackBottom()`` or ``StackLeft()`` will set the current view as a new reference point.
After that, the methods ``Top()``, ``Right()``, ``Bottom()`` and ``Left()`` will behave as ``BelowOf()``, ``RightOf()``, ``AboveOf()`` and ``LeftOf()``.


For example, when you use ``StackTop()`` on a view, the next time you call ``Top()`` on the next view, 
``Top()`` will not create a constraint to the top of the parent view, but a constraint to the bottom of the stacked view.


In this example, 10 rows of 10 squares will be placed:
```
int amount = 10;

int max = amount * amount;
int color = 255;
int colorSteps = (int) Math.Floor((double)(color / max)); // used to create a different color for each view

constraintHelper = new ConstraintHelper(View);

for (int i = 1; i < max + 1; i++) {
	
	constraintHelper
		.Attach(new UIView { BackgroundColor = UIColor.FromRGB(color, color, color) })
		.WidthOfParent((float)1 / amount)
		.HeightFromWidth()
		.Top()
		.Left()
		.StackLeft(); // makes next call of Left behave like LeftOf current item
	
	if(i % amount == 0) {
		constraintHelper
			.StackTop() // makes next call of Top behave like BelowOf current item
			.ResetLeftStack(); // afterwards Left will position according to parents left
	}
}			
```

## Margin

In a lot of layouting cases, margins are a necessity. The ConstraintHelper allows you to define margins in a similar way as in css.

Use the ``SetMargin()`` to set individuals margins or define a ConstraintMargin object representing all the margins.
Furthermore you can implement ``IConstraintHelpedView``, it defines the margin attribute. The margins of views implementing the interface will automatically be taken into account.

It is important to note, that a margin will only take effect if you position the element accordingly. A left margin will only be made if you position it to that side.

Margins will not collapse if you stack views:
If there is a bottom margin of the first view and a top margin of the second view,
the second element below will be positioned taking both margins into account.

```
constraintHelper = new ConstraintHelper(View);

var box1 = new UIView() { BackgroundColor = UIColor.FromRGB(200, 200, 200) };
var box2 = new UIView() { BackgroundColor = UIColor.FromRGB(210, 210, 210) };
var box3 = new UIView() { BackgroundColor = UIColor.FromRGB(220, 220, 220) };

var box4 = new UIView() { BackgroundColor = UIColor.FromRGB(230, 230, 230) };
var box5 = new UIView() { BackgroundColor = UIColor.FromRGB(230, 230, 230) };

constraintHelper
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
		// margin don't collapse, the bottom margin of the previous element takes effect, so no margin top
		.SetMargin(0, 10f) // there is multiple signatures (N, E, S, W), (N&S, E&W), (All the same)
		.Left() // the element still has to be positioned
		.Right()
		.Top()
		.Height(40f)
		.StackTop() 

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
		.StackTop();
```

Margins for the moment are done manually in the library and do not take advantage of iOS 8 constraint margins, but they do work on iOS 7.

## Animations

The ConstraintHelper helps with animations.  With methods to remove constraints and forcing rendering, animations are easily done.

```
constraintHelper = new ConstraintHelper(View);

var box1 = new UIView { BackgroundColor = UIColor.Blue };
var box2 = new UIView { BackgroundColor = UIColor.Black };


// set constraints as usual

constraintHelper
	.Attach(box1)
		.Top().Left().Right().Height(1f)
	.Attach(box2)
		.Width(10f).Height(10f).Center().Middle()

	// layout constraints (equal to LayoutIfNeeded)
	.UpdateConstraints();


UIView.Animate(1, 0, UIViewAnimationOptions.CurveEaseOut, 
	() => {
		box2.Layer.CornerRadius = 75f; // some other changes for fanciness

		// change the constraints

		ConstraintHelper
			.WorkWith(box1)
				.RemoveHeightConstraint().HeightOfParent(0.5f)
			.WorkWith(box2)
				.RemoveWidthConstraint().Width(150f)
				.RemoveHeightConstraint().Height(150f)

			// layout constraints again (equal to LayoutIfNeeded)
			.UpdateConstraints();
	},
	null
);
```

In the above example, the first View will change its height from 0 to 50% of its parent view; and the second view gets scaled up and changed from a square to a circle.