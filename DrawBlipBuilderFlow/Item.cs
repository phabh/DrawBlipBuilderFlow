using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBlipBuilderFlow
{
    public class Item
    {
        public Point BuilderPosition { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public Icon[] Icons { get; set; }
        public string[] Buttons { get; set; }
        public string[] Connections { get; set; }
        public TypeBox TypeBox { get; set; }
        public Item[] ContentItems { get; set; }
        public Item[] ConnectionItems { get; set; }
    }

    public enum Icon
    {
        Robo,
        API_IN,
        API_EX,
        API,
        Transbordo,
        NLP,
        WebView,
        Regex,
        Erro,
        Input
    }

    public enum TypeBox
    {
        Normal,
        QuickReply,
        Carrosel
    }
}
