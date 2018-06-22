using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBlipBuilderFlow.Extension
{
    public static class RetangleExtension
    {
        public static RectangleF AllignCenter( this RectangleF thisRectangle, Graphics graphics, string text, Font font )
        {
            var stringSize = graphics.MeasureString(text, font);

            var leftString =  thisRectangle.X + (thisRectangle.Width - stringSize.Width) / 2;
            var topString = thisRectangle.Y + (thisRectangle.Height - stringSize.Height) / 2;

            return new RectangleF(new PointF(leftString, topString), stringSize);
        }

        public static Rectangle AllignCenter(this Rectangle thisRectangle, Graphics graphics, string text, Font font)
        {
            var stringSizeF = graphics.MeasureString(text, font);
            var stringSize = new Size((int)stringSizeF.Width+1, (int)stringSizeF.Height+1);

            var leftString = (int) (thisRectangle.X + (thisRectangle.Width - stringSize.Width) / 2);
            var topString = (int) (thisRectangle.Y + (thisRectangle.Height - stringSize.Height) / 2);

            return new Rectangle(new Point(leftString, topString), stringSize);
        }
    }
}
