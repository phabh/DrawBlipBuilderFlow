using DrawBlipBuilderFlow.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBlipBuilderFlow
{
    public class DrawUtil
    {
        private readonly SolidBrush _brush = new SolidBrush(Color.FromArgb(232,232,232));
        private readonly SolidBrush _brushText = new SolidBrush(Color.Black);
        private readonly SolidBrush _brushItem = new SolidBrush(Color.FromArgb(185,185,185));
        private readonly Pen _penLine = new Pen(Color.Black, 2);
        private readonly Font _font = new Font("Roboto", 24);
        private readonly Font  _fontMenu = new Font("Roboto", 16);

        private readonly Size _cardSize = new Size(255, 211);
        private readonly Size _cardButtonSize = new Size(255, 56);

        private readonly Graphics _graphics;
        public DrawUtil(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void Draw(Rectangle rectangle, Item item)
        {
            switch (item.TypeBox)
            {
                case TypeBox.Normal: DrawNormal(rectangle, item); break;
                case TypeBox.QuickReply: DrawQuickReply(rectangle, item); break;
                case TypeBox.Carrosel: DrawCarrosel(rectangle, item); break;
            }
        }

        private void DrawCarrosel(Rectangle rectangle, Item item)
        {
            var sizeTitle = _graphics.MeasureString(item.Title, _font);

            _graphics.DrawString(item.Title, _font, _brushText, rectangle.Location);

            var left = rectangle.Left + 10;
            var top = (int)(rectangle.Top + sizeTitle.Height + 10);

            foreach (var card in item.ContentItems)
            {
                var point = new Point(left, top);

                DrawCard(point, card);

                left += _cardSize.Width + 10;
            }

            //_graphics.DrawRoundedRectangle( new Pen(_brushItem),  )


            //DrawNormal(rectangle, item);
        }

        private void DrawCard(Point point, Item item)
        {
            var rectCard = new Rectangle(point, _cardSize);
            _graphics.FillRoundedRectangle(_brush, rectCard, 8);

            var bottom = rectCard.Bottom;

            foreach (var button in item.Buttons)
            {
                var rectButton = new Rectangle(new Point(rectCard.Left, bottom), _cardButtonSize);
                _graphics.FillRoundedRectangle(_brushItem, rectButton, 8);
                bottom += _cardButtonSize.Height;

                var buttonStringRect = rectButton.AllignCenter(_graphics, button, _fontMenu);

                _graphics.DrawString(button, _fontMenu, _brushText, buttonStringRect);
            }
        }

        private void DrawQuickReply(Rectangle rectangle, Item item)
        {
            DrawNormal(rectangle, item);
        }

        private void DrawNormal(Rectangle rectangle, Item item)
        {
            _graphics.FillRoundedRectangle(_brush, rectangle, 8);

            var sizeIcon = DrawUtils.Inflate(Icon.Robo.GetImage().Size, 0.5f);

            for( int i = 0; i < item.Icons.Length; i++)
            {
                var tempRect = new Rectangle(new Point((rectangle.Left + (i * (sizeIcon.Width + 3)) + 4), rectangle.Top + 4), sizeIcon);
                var icon = item.Icons[i];
                _graphics.DrawImage(icon.GetImage(), tempRect);
            }

            var centerString = rectangle.AllignCenter(_graphics, " " + item.Title+" ", _font);

            _graphics.DrawString(" " + item.Title + " ", _font, _brushText, centerString);

            //var leftAccumulate = 0f;


            if (item.ConnectionItems.Length > 5) return;

            for (int i = 0; i < item.ConnectionItems.Length; i++)
            {
                //if (item.ConnectionItems[i].Title.ToLower() == "inicio" || item.ConnectionItems[i].Title.ToLower() == "exceções") continue;

                _graphics.DrawLine(_penLine, item.BuilderPosition, item.ConnectionItems[i].BuilderPosition);



                //var connString = " "+item.ConnectionItems[i].Title+" ";

                //var stringSizeF = _graphics.MeasureString(connString, _fontMenu);

                //var tempRect = new RectangleF(new PointF(rectangle.Left + leftAccumulate + 4f, rectangle.Bottom + 4f), stringSizeF);
                //_graphics.DrawString(connString, _fontMenu, _brushItem, tempRect);

                //leftAccumulate += stringSizeF.Width + 10f;
            }
        }
    }
}
