using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GifImageData : MonoBehaviour
{

    public struct ImageData
    {
        public int LZWCompressionSize;
        public string ImageDataString { get; private set; }
        public List<string> SubBlocks { get; private set; }
        public int bits { get; set; }

        public void Set(string stream)
        {
            SubBlocks = new List<string>();
            Debug.Log("IMAGE DATA: " + stream);
            int index, total, subBlockLength;
            string temp;

            Debug.LogWarning("LZW minimum code size: " + stream.Substring(0, 2));
            index = 2;
            total = 2;

            for (int i = 0; i < stream.Length; i++)
            {
                subBlockLength = GifHelper.HexToDecimal(stream.Substring(index, 2)) * 2;
                index += 2;
                if (subBlockLength == 0)
                {
                    Debug.LogWarning("byte index in image data: " + index + " /" + stream.Length.ToString());
                    //Debug.Log("number of sub blocks:" + SubBlocks.Count);
                    return;
                }
                Debug.LogWarning("sub block length: " + subBlockLength);
                total += subBlockLength + 2;

                temp = stream.Substring(index, subBlockLength);
                index += subBlockLength;
                SubBlocks.Add(temp);
                //Debug.LogWarning("byte index in image data: " + index + " /" + stream.Length.ToString());
            }
        }
    }
}
