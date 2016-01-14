using UnityEngine;
using System.Collections;


public class GifHeader : MonoBehaviour
{

    public struct Header
    {
        public string Signature { get; private set; }
        public string Version { get; private set; }
        public int bits { get;  set; }

        public void Set(string stream)
        {
            Debug.Log("HEADER: " + stream);
            //Debug.LogError("HEADER: " + stream);
            Signature = stream.Substring(0,6);
            Version = stream.Substring(6, 6);

            if (GifHelper.HexToASCII(Signature) != "GIF")
            {
                Debug.LogError("NOT A GIF");
            }
            
            string ASCIIVersion = GifHelper.HexToASCII(Version);
            if (ASCIIVersion != "89a" && ASCIIVersion != "87a")
            {
                Debug.LogError("NOT A COMPATIBLE VERSION " + ASCIIVersion);
            }
        }
    }


}
