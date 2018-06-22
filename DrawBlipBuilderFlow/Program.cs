using DrawBlipBuilderFlow.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBlipBuilderFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bmp;
            Graphics objGraphics;
            Rectangle rt;
            Point pnt;

            bmp = new Bitmap(20000, 7000, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            objGraphics = Graphics.FromImage(bmp);
            objGraphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(0, 0), new Size(20000, 7000)));

            var drawUtil = new DrawUtil(objGraphics);

            var builderFlowJson = GetBuilderFlow();

            var listItems = new Dictionary<string, Item>();

            foreach (var box in builderFlowJson)
            {
                var item = box.GetItem();

                listItems.Add(item.Id, item);
            }

            foreach(var item in listItems)
            {
                var itemContent = item.Value;

                var connectionItems = new List<Item>();

                foreach(var conn in itemContent.Connections)
                {
                    connectionItems.Add(listItems[conn]);
                }

                itemContent.ConnectionItems = connectionItems.ToArray();
            }

            int count = 0;

            foreach (var item in listItems)
            {
                var itemContent = item.Value;

                //var tempRect = new Rectangle(new Point((bmp.Size.Width - 464) / 2, count * 320), new Size(464, 160));
                var tempRect = new Rectangle(itemContent.BuilderPosition, new Size(464, 160));

                drawUtil.Draw(tempRect, itemContent);

                count++;
            }



            //Bitmap bmp;
            //Graphics objGraphics;
            //Rectangle rt;
            //Point pnt;

            //var rectList = new List<Rectangle>();

            //bmp = new Bitmap(10000, 10000, System.Drawing.Imaging.PixelFormat.Format24bppRgb);


            //var brush = new SolidBrush(Color.FromArgb(232,232,232));
            //var brushText = new SolidBrush(Color.Black);
            //var brushItem = new SolidBrush(Color.FromArgb(185,185,185));

            //objGraphics = Graphics.FromImage(bmp);


            //Image roboIcon = Image.FromFile("Icon_Robot.png");
            //Image transbordIcon = Image.FromFile("Icon_Transbord.png");
            //Image webviewIcon = Image.FromFile("Icon_WebView.png");
            //Image regexIcon = Image.FromFile("Icon_Regex.png");
            //Image nlpIcon = Image.FromFile("Icon_NLP.png");
            //Image handIcon = Image.FromFile("Icon_Hand.png");
            //Image erroIcon = Image.FromFile("Icon_Erro.png");
            //Image apiIcon = Image.FromFile("Icon_API.png");
            //Image apiInIcon = Image.FromFile("Icon_API In.png");
            //Image apiExIcon = Image.FromFile("Icon_API Ex.png");

            //var font = new Font("Roboto", 24);
            //var fontMenu = new Font("Roboto", 16);



            //foreach (var box in builderFlowJson)
            //{
            //    var title = box.Value["$title"].ToString();
            //    int left = (int)float.Parse(box.Value["$position"]["left"].ToString().Replace("px", ""))*3;
            //    int top = (int)float.Parse(box.Value["$position"]["top"].ToString().Replace("px", ""))*3;

            //    rt = new Rectangle(left, top, 464, 160);
            //    var tempRect = new Rectangle( new Point(rt.X + 1, rt.Y + 1), DrawUtils.Inflate(roboIcon.Size, 0.8f));

            //    objGraphics.FillRoundedRectangle(brush, rt, 4);
            //    objGraphics.DrawImage(roboIcon, tempRect);

            //    var centerString = rt.AllignCenter(objGraphics, title, font);

            //    objGraphics.DrawString(title, font, brushText, centerString);

            //    var actions = box.Value["$contentActions"];

            //    foreach (var action in actions)
            //    {
            //        if (action["action"] == null) continue;

            //        var document = action["action"]["$cardContent"]["document"];

            //        if (document["type"].ToString() == "application/vnd.lime.collection+json" && document["content"]["itemType"].ToString() == "application/vnd.lime.document-select+json")
            //        {
            //            var items = document["content"]["items"];

            //            var totalItens = items.Count();

            //            var widthCarousel = rt.Width / totalItens;
            //            var heightCarousel = rt.Height / 2; 

            //            for( int i = 0; i < totalItens; i++)
            //            {
            //                var item = items[i];
            //                var titleItem = item["header"]["value"]["title"].ToString();

            //                var options = item["options"];

            //                var sizeCarosel = new Size(255, 210);

            //                var rectCarosel = new Rectangle( new Point( rt.X + (i * (sizeCarosel.Width + 3)), rt.Y), sizeCarosel);

            //                objGraphics.FillRoundedRectangle(brush, rectCarosel, 4);

            //                var centerStringRt = rectCarosel.AllignCenter(objGraphics, titleItem, fontMenu);

            //                objGraphics.DrawString(titleItem, fontMenu, brushText, centerStringRt);
            //                var totalOptions = options.Count();

            //                if (totalOptions <= 0) continue;

            //                var optionHeight = heightCarousel / totalOptions;

            //                for(int j = 0; j <  totalOptions; j++)
            //                {
            //                    var option = options[j];
            //                    var titleOption = option["label"]["value"].ToString();

            //                    var itemRect = new Rectangle(rt.X+(i * widthCarousel)+1, rt.Y + heightCarousel + (j * optionHeight)+1, widthCarousel-2, optionHeight-2);

            //                    objGraphics.FillRoundedRectangle(brushItem, itemRect, 4);

            //                    var centerStringItemRt = itemRect.AllignCenter(objGraphics, titleOption, fontMenu);

            //                    objGraphics.DrawString(titleOption, fontMenu, brushText, centerStringItemRt);
            //                }
            //            }
            //        }
            //    }


            //}


            objGraphics.Dispose();

            bmp.Save("test.bmp");

            bmp.Dispose();
        }

        public static JObject GetBuilderFlow()
        {
            return JsonConvert.DeserializeObject<JObject>(File.ReadAllText("builderflow.json"));
        }

    }
}
