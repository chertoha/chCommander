using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace chCommander {
    class xmlConfig {

        public string title { get; set; }
        public int backGroundColorIndex { get; set; }
        public int foreGroundColorIndex { get; set; }
        public string leftWindowStartPath { get; set; }
        public string rightWindowStartPath { get; set; }
        public bool leftWindowActive { get; set; }

        public xmlConfig() {


            

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNodeList nodes = doc.GetElementsByTagName("properties");

            
            string backgroundColor = "";
            string foregroundColor = "";
            string active="";

            foreach (XmlNode node in nodes) {
                title = node["title"].InnerText;
                backgroundColor = node["backgroundColor"].InnerText;
                foregroundColor = node["foregroundColor"].InnerText;
                leftWindowStartPath = node["leftWindowStartPath"].InnerText;
                rightWindowStartPath = node["rightWindowStartPath"].InnerText;
                active = node["leftWindowActive"].InnerText;

                if (active.ToLower() == "true") {
                    leftWindowActive = true;
                }
                else {
                    leftWindowActive = false;
                }                
            }//foreach

            //default colors
            backGroundColorIndex = 7;
            foreGroundColorIndex = 0;

            string[] colors = { "black", "darkBlue", "darkGreen", "DarkCyan", "DarkRed", "DarkMagenta", "DarkYellow", "Gray", "DarkGray", "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", "White" };
            for (int i = 0; i < colors.Length; i++) {
                if (backgroundColor.ToLower() == colors[i].ToLower()) {
                    backGroundColorIndex = i;
                }
                if (foregroundColor.ToLower() == colors[i].ToLower()) {
                    foreGroundColorIndex = i;
                }
            }//for


        }//c-tor

        



    }//class xmlConfig
}//namespace chCommander 
