using System;

namespace SR.MonoTouchHelpers
{
  public class ConstraintMargin
  {
    public float Top { get; set; }
    public float Right { get; set; }
    public float Bottom { get; set; }
    public float Left { get; set; }

    public ConstraintMargin(float top, float right, float bottom, float left)
    {
      Top = top;
      Right = right;
      Bottom = bottom;
      Left = left;
    }

    public ConstraintMargin(float topAndBottom, float rightAndLeft)
    {
      Top = Bottom = topAndBottom;
      Right = Left = rightAndLeft;
    }

    public ConstraintMargin(float allSides)
    {
      Top = Right = Bottom = Left = allSides;
    }

    public ConstraintMargin() {}
  }
}

