using UnityEngine;
using System.Collections;

public class GifApplicationExtension : MonoBehaviour {

    public struct ApplicationExtension
    {
        public const string ExtensionLabel = "FF";

        public int BlockSize { get; private set; }
        public string ApplicationIdentifier { get; private set; }
        public string AuthenticationCode { get; private set; }
        public byte[] Data { get; private set; }
        public int bits {get; set;}

        public void Set(string stream)
        {
            Debug.Log("APPLICATION EXTENSION: " + stream);
            string extension = stream.Substring(0, 2);
            string label = stream.Substring(2, 2);
            if(label != ExtensionLabel)
            {
                Debug.LogError("NOT AN APPLICATION EXTENSION");
            }
            BlockSize = GifHelper.HexToDecimal(stream.Substring(4, 2));
            ApplicationIdentifier = GifHelper.HexToASCII(stream.Substring(6, 16));
            AuthenticationCode = GifHelper.HexToASCII(stream.Substring(22, 6));
        }

        public void DebugLog()
        {
            Debug.Log("Block Size: " + BlockSize);
            Debug.Log("Application Identifier: " + ApplicationIdentifier);
            Debug.Log("Authentification Code: "+ AuthenticationCode);
        }
    }
}
