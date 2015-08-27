using System;

namespace SR.MonoTouchHelpers
{
  public class ConstraintMargin
  {
    public float Top { get; set; }
    public float Right { get; set; }
    public float Bottom { get; set; }
    public float Left { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SR.MonoTouchHelpers.ConstraintMargin"/> class with individual values for each sides.
    /// </summary>
    /// <param name="top">Top.</param>
    /// <param name="right">Right.</param>
    /// <param name="bottom">Bottom.</param>
    /// <param name="left">Left.</param>
    public ConstraintMargin(float top, float right, float bottom, float left)
    {
      Top = top;
      Right = right;
      Bottom = bottom;
      Left = left;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SR.MonoTouchHelpers.ConstraintMargin"/> class with equal margin values for top & bottom, and left & right.
    /// </summary>
    /// <param name="topAndBottom">Top and bottom.</param>
    /// <param name="rightAndLeft">Right and left.</param>
    public ConstraintMargin(float topAndBottom, float rightAndLeft)
    {
      Top = Bottom = topAndBottom;
      Right = Left = rightAndLeft;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SR.MonoTouchHelpers.ConstraintMargin"/> class with equal margin values.
    /// </summary>
    /// <param name="allSides">All sides.</param>
    public ConstraintMargin(float allSides)
    {
      Top = Right = Bottom = Left = allSides;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SR.MonoTouchHelpers.ConstraintMargin"/> class.
    /// </summary>
    public ConstraintMargin() {}
  }
}

