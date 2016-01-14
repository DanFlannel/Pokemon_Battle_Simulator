using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GifGlobalColorTable : MonoBehaviour {

	public struct GlobalColorTable
    {
        public string GlobalColorTableString { get; set; }
        public Dictionary<int, GifColor> ColorList { get; private set; }
        public int NumberOfColors { get; private set; }

        public void Set(string stream) {

            //Debug.Log("GLOBAL COLOR TABLE: " + stream);
            ColorList = new Dictionary<int, GifColor>();
            if(stream.Length % 6 != 0)
            {
                Debug.LogError("GLOBAL COLOR TABLE LENGTH DOESNT MOD 6");
            }
            NumberOfColors = stream.Length / 6;
            //Debug.LogError("GLOBAL COLOR TABLE COLORS: " + NumberOfColors);

            GlobalColorTableString = stream;

            //Debug.LogError("GLOBAL COLOR TABLE: " + GlobalColorTableString);

            //create our color table here using a dictionary
            int index = 0;
            bool check = false;

            for(int i = 0; i < GlobalColorTableString.Length; i+=6)
            {
                string colorString = GlobalColorTableString.Substring(i, 6);
                if(colorString == "000000" && !check)
                {
                    Debug.LogError("FIRST 000000 IN GLOBAL COLOR TABLE: " + (i/6) + "/" + (GlobalColorTableString.Length/6));
                    check = true;
                }
                GifColor color = new GifColor();
                color.Set(colorString);
                ColorList.Add(index,color);
                index++;
            }
            //Debug.LogError("ADDED " + index + " COLORS TO OUR COLOR TABLE");

        }

        public void DebugLog()
        {
            Debug.Log("Global Color Table: " + GlobalColorTableString);
            Debug.Log("# of colors: " + NumberOfColors);
        }
    }

    public struct GifColor
    {
        public int R;
        public int G;
        public int B;

        public void Set(string data)
        {
            R = GifHelper.HexToDecimal(data.Substring(0, 2));
            G = GifHelper.HexToDecimal(data.Substring(2, 2));
            B = GifHelper.HexToDecimal(data.Substring(4, 2));

        }
    }
}
