# ConstraintHelper

## Introduction

This ConstraintHelper library aims to make developers life easier when it comes to creating layout constraints with Xamarin.iOS. 
It makes heavy use of method chaining to make writing the constraints more efficient.

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
var box5 = new UIView ();

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

As you probably reckoned, this will create 4 boxes, 50 by 50 in size, that are aligned diagonaly down from the top left (ok, ok , it's not the most real life scenario).

These methods will take into account the margins of the elements. 
If there is a bottom margin of the first and a top margin of the second element,
the second element below will be positioned taking both margins into account - margins do not collapse.


## Sizing

The main methods for sizing are ``Width()``, ``WidthOf()``, ``WidthOfParent()``, ``Height()``, ``HeightOf()`` and ``HeightOfParent()``.
While Width() and Height() are good for setting fixed values, all HeighOf and WidthOf methods are good for percental sizes.

