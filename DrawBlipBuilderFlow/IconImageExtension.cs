using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBlipBuilderFlow
{
    public static class IconImageExtension
    {
        public static Image GetImage(this Icon icon)
        {
            switch (icon)
            {
                
                case Icon.API_IN: return Image.FromFile("assets\\Icon_API In.png");
                case Icon.API_EX: return Image.FromFile("assets\\Icon_API Ex.png");
                case Icon.API: return Image.FromFile("assets\\Icon_API.png");
                case Icon.Transbordo: return Image.FromFile("assets\\Icon_Transbord.png");
                case Icon.NLP: return Image.FromFile("assets\\Icon_NLP.png");
                case Icon.WebView: return Image.FromFile("assets\\Icon_WebView.png");
                case Icon.Regex: return Image.FromFile("assets\\Icon_Regex.png");
                case Icon.Erro: return Image.FromFile("assets\\Icon_Erro.png");
                case Icon.Input: return Image.FromFile("assets\\Icon_Hand.png");
                case Icon.Robo:
                default: return Image.FromFile("assets\\Icon_Robot.png");
            }
        }
    }
}
