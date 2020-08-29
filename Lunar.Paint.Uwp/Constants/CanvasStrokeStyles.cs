using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunar.Paint.Uwp.Constants
{
    public static class CanvasStrokeStyles
    {
        public static readonly CanvasStrokeStyle GridLineStyle = new CanvasStrokeStyle()
        {
            StartCap = CanvasCapStyle.Flat,
            EndCap = CanvasCapStyle.Flat,
            MiterLimit = 10,
            DashStyle = CanvasDashStyle.Solid,
            DashCap = CanvasCapStyle.Square,
            DashOffset = 0,
            TransformBehavior = CanvasStrokeTransformBehavior.Hairline
        };
    }
}
