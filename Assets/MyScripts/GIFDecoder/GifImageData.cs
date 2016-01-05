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

        public void Set(string stream)
        {
            int index = 0;
            int subBlockLength;
            int total = 0;
            string subBlock;
            ImageDataString = stream;
            LZWCompressionSize = GifHelper.HexToDecimal(stream.Substring(index, 2));
            index += 2;

            for (int i = 0; i < stream.Length; i++)
            {
                subBlockLength = GifHelper.HexToDecimal(stream.Substring(index, 2)) * 2;
                index += 2;

                if (subBlockLength == 0)
                {
                    Debug.LogWarning("byte index in image data: " + index + " /" + stream.Length.ToString());
                    Debug.Log("number of sub blocks:" + SubBlocks.Count);
                    return;
                }

                total += subBlockLength + 2;

                subBlock = stream.Substring(index, subBlockLength);
                index += subBlockLength;
                SubBlocks.Add(subBlock);
            }
        }
    }
}
