using UnityEngine;
using System.Collections;

public class GifImageDescriptor : MonoBehaviour {

	public struct ImageDescriptor
    {
        public const string ImageSeperator = "2C";
        public int Left { get; private set; }
        public int Top { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool LocalColorTableFlag { get; private set; }
        public bool InterlaceFlag { get; private set; }
        public bool SortFlag { get; private set; }
        public string ResevedForFutureUse { get; private set; }
        public int LocalColorTableSize { get; private set; }
        public int bits { get; set; }

        public void Set(string stream)
        {
            string label = stream.Substring(0, 2);
            Debug.Log("IMAGE DESCRIPTOR: " + stream);
            if(ImageSeperator != label)
            {
                Debug.LogError("NOT AN IMAGE SEPERATOR");
            }

            string redrawLeftHex = stream.Substring(4, 2) + stream.Substring(2, 2);
            Left = GifHelper.HexToDecimal(redrawLeftHex);
            

            string redrawTopHex = stream.Substring(8, 2) + stream.Substring(6, 2);
            Top = GifHelper.HexToDecimal(redrawTopHex);
            

            string redrawWidthHex = stream.Substring(12, 2) + stream.Substring(10, 2);
            Width = GifHelper.HexToDecimal(redrawWidthHex);
            

            string redrawHeightHex = stream.Substring(16, 2) + stream.Substring(14, 2);
            Height = GifHelper.HexToDecimal(redrawHeightHex);
            

            string PacketField = stream.Substring(18, 2);
            //Debug.LogWarning("Resize Left: " + Left);
            //Debug.LogWarning("Resize Top: " + Top);
            //Debug.LogWarning("Resize Width: " + Width);
            //Debug.LogWarning("Resize Height: " + Height);

            //Debug.LogWarning("PacketField: " + PacketField);

            packetFieldDecrypt(PacketField);
        }

        private void packetFieldDecrypt(string input)
        {
            string packetField = GifHelper.HexToBinary(input);
            LocalColorTableFlag = packetField.Substring(0, 1) == "1";
            InterlaceFlag = packetField.Substring(1, 1) == "1";
            SortFlag = packetField.Substring(2, 1) =="1";
            ResevedForFutureUse = packetField.Substring(3, 2);
            LocalColorTableSize = GifHelper.HexToDecimal(packetField.Substring(5, 3));
        }
    }
}
