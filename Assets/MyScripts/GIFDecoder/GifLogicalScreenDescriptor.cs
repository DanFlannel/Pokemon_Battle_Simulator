using UnityEngine;
using System.Collections;
using System;

public class GifLogicalScreenDescriptor : MonoBehaviour
{



    public struct LogicalScreenDescriptor
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool HasGlobalColorTable { get; private set; }
        public int ColorResolution { get; private set; }
        public bool IsGlobalColorTableSorted { get; private set; }
        public int GlobalColorTableSize { get; private set; }
        public int BackgroundColorIndex { get; private set; }
        public int PixelAspectRatio { get; private set; }
        public int bits { get; set; }

        public void Set(string stream)
        {
            Debug.Log("LOGICAL DESCRIPTOR: " + stream);
            string widthHex = stream.Substring(2, 2) + stream.Substring(0, 2);
            string heightHex = stream.Substring(6, 2) + stream.Substring(4, 2);

            Width = GifHelper.HexToDecimal(widthHex);
            Height = GifHelper.HexToDecimal(heightHex);

            string BgColor = stream.Substring(10, 2);
            BackgroundColorIndex = GifHelper.HexToDecimal(BgColor);

            string AspectRatio = stream.Substring(12, 2);
            PixelAspectRatio = GifHelper.HexToDecimal(AspectRatio);

            DecodePacketField(stream.Substring(8, 2));


        }

        private void DecodePacketField(string packetField)
        {
            //Debug.LogError("LOGICAL DESCRIPTOR PACKETFIELD: " + packetField);
            string binary = GifHelper.HexToBinary(packetField);

            HasGlobalColorTable = binary.Substring(0, 1) == "1";
            IsGlobalColorTableSorted = binary.Substring(4, 1) == "1";

            long resolution = Convert.ToInt64(binary.Substring(1, 3), 2);
            ColorResolution = (int)resolution + 1;

            long size = Convert.ToInt64(binary.Substring(5, 3), 2);
            GlobalColorTableSize = (int)(Mathf.Pow(2, size + 1) * 3);
            //Debug.LogError("Global Color Table Size: " + GlobalColorTableSize);

        }
    }



}
