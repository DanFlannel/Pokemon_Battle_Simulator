using UnityEngine;
using System.Collections;
using System;

public class GifGraphicsControlExtension : MonoBehaviour
{

    public struct GraphicsControlExtension
    {
        public const string ExtensionIntroducer = "21";
        public const string Label = "F9";
        public int FutureUse { get; private set; }
        public int BlockSize { get; private set; }
        public int DisposalMethod { get; private set; }
        public bool UserInput { get; private set; }
        public bool HasTransparency { get; private set; }
        public int Delay { get; private set; }
        public int TransparencyIndex { get; private set; }
        public int bits { get; set; }

        public void Set(string stream)
        {
            Debug.Log("GRAPHICS CONTROL EXTENSION: " + stream);
            ErrorCheck(stream);

            string size = stream.Substring(4, 2);
            size = GifHelper.HexToBinary(size);
            BlockSize = (int)Convert.ToInt64(size, 2);

            string packetField = stream.Substring(6, 2);
            PacketField(packetField);

            string delay = stream.Substring(8, 4);
            Delay = GifHelper.HexToDecimal(delay);

            string transparentIndex = stream.Substring(12, 2);
            TransparencyIndex = GifHelper.HexToDecimal(transparentIndex);
            //Debug.LogWarning("GCE TRANSPARENCY INDEX: " + TransparencyIndex);        

            

        }

        private void ErrorCheck(string stream)
        {
            string streamIntroducer = stream.Substring(0, 2);
            string streamLabel = stream.Substring(2, 2);
            string terminator = stream.Substring(14, 2);

            if (streamIntroducer != ExtensionIntroducer)
            {
                Debug.LogError("NOT A GRAPHICS CONTROL EXTENSION");
            }
            if (streamLabel != Label)
            {
                Debug.LogError("NOT A GRAPHICS CONTROL EXTENSION");
            }
            if (terminator != "00")
            {
                Debug.LogError("TERMINATOR MISSING");
            }
        }

        private void PacketField(string packetField)
        {
            packetField = GifHelper.HexToBinary(packetField);
            //Debug.LogWarning("GCE PACKET FIELD BINARY: " + packetField);

            FutureUse = GifHelper.HexToDecimal(packetField.Substring(0, 3));
            DisposalMethod = GifHelper.HexToDecimal(packetField.Substring(3, 3));

            UserInput = packetField.Substring(6, 1) == "1";
            HasTransparency  = packetField.Substring(7, 1) == "1";
        }

        public void DebugLog()
        {
            Debug.Log("Future Use: " + FutureUse);
            Debug.Log("Block Size: " + BlockSize);
            Debug.Log("Disposal Method: " + DisposalMethod);
            Debug.Log("Use Input: " + UserInput);
            Debug.Log("Has Transparency: " + HasTransparency);
            Debug.Log("Delay: " + Delay);
            Debug.Log("Transparency Color Index: " + TransparencyIndex);
        }

    }
    
}
