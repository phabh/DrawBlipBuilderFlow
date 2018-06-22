using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBlipBuilderFlow.Extension
{
    public static class IconExtensions
    {
        public static Item GetItem(this KeyValuePair<string,JToken> box)
        {
            var contentItemList = new List<Item>();
            var typeBox = TypeBox.Normal;


            var actions = box.Value["$contentActions"];

            var iconList = new List<Icon>();

            var mainItem = new Item();

            int left = (int)float.Parse(box.Value["$position"]["left"].ToString().Replace("px", "")) * 4;
            int top = (int)float.Parse(box.Value["$position"]["top"].ToString().Replace("px", ""))*3;

            mainItem.BuilderPosition = new System.Drawing.Point(left, top);


            foreach (var action in actions)
            {
                if (action["input"] != null)
                {
                    iconList.Add(Icon.Input);
                    continue;
                }

                if (action["action"] == null) continue;

                switch (action["action"]["type"].ToString())
                {
                    case "SendMessage": iconList.Add(Icon.Robo); break;
                }

                var document = action["action"]["$cardContent"]["document"];

                if (document["type"].ToString() == "application/vnd.lime.collection+json" && document["content"]["itemType"].ToString() == "application/vnd.lime.document-select+json")
                {
                    typeBox = TypeBox.Carrosel;

                    var items = document["content"]["items"];

                    var totalItens = items.Count();

                    for (int i = 0; i < totalItens; i++)
                    {
                    
                        var contentItemTemp = new Item();

                        var item = items[i];
                        var titleItem = item["header"]["value"]["title"].ToString();

                        contentItemTemp.Title = titleItem;
                        var options = item["options"];

                        var totalOptions = options.Count();

                        if (totalOptions <= 0) continue;

                        var buttonsList = new List<string>();

                        for (int j = 0; j < totalOptions; j++)
                        {
                            var option = options[j];
                            var titleOption = option["label"]["value"].ToString();
                            buttonsList.Add(titleOption);
                        }

                        contentItemTemp.Buttons = buttonsList.ToArray();

                        contentItemList.Add(contentItemTemp);
                    }
                }
            }

            var outputConditions = box.Value["$conditionOutputs"];

            var connections = new List<string>();

            foreach (var action in outputConditions)
            {
                if (action["conditions"] == null) continue;

                var conditions = action["conditions"];
                connections.Add(action["stateId"].ToString());

                foreach (var condition in conditions)
                {
                    switch (condition["comparison"].ToString())
                    {
                        case "approximateTo":
                        case "contains":
                        case "matches": iconList.Add(Icon.Regex); break;
                    }
                }
            }

            //var defaultOutput = box.Value["$defaultOutput"];

            //connections.Add(defaultOutput["stateId"].ToString());

            mainItem.Icons = iconList.GroupBy( i => i ).ToList().Select( g => g.Key ).ToArray();
            mainItem.Title = box.Value["$title"].ToString();
            mainItem.ContentItems = contentItemList.ToArray();
            mainItem.TypeBox = typeBox;
            mainItem.Id = box.Value["id"].ToString();
            mainItem.Connections = connections.GroupBy( c => c ).ToList().Select( g => g.Key ).ToArray();

            return mainItem;
        }

                    

    }
}
