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
            string label = stream.Substring(2, 2);
            if(label != ExtensionLabel)
            {
                Debug.LogError("NOT AN APPLICATION EXTENSION");
            }
        }
    }
}
