using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

            LZWCompressionSize = GifHelper.HexToDecimal(stream.Substring(0, 2));
            Debug.Log("LZW minimum code size: " + LZWCompressionSize);
            

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
                    break;
                }
                Debug.Log("sub block length: " + subBlockLength);
                total += subBlockLength + 2;

                temp = stream.Substring(index, subBlockLength);
                index += subBlockLength;
                SubBlocks.Add(temp);
                //Debug.LogWarning("byte index in image data: " + index + " /" + stream.Length.ToString());
            }

            DecryptImageData(stream);



        }
        //GIF files use LSB-First packing order so I have to account for this.
        private void DecryptImageData(string stream)
        {
            string builder3 = "";
            string builder = "";
            string number = "";
            string binary = GifHelper.HexToBinary(stream);
            string binary2 = "";
            int pixel = 0;

            for (int i = 0; i < binary.Length - LZWCompressionSize; i+= LZWCompressionSize)
            {
                number = Convert.ToInt32(binary.Substring(i, LZWCompressionSize), 2).ToString();
                binary2 += binary.Substring(i, 8) + ", ";
                builder += number + ", ";
                pixel++;
            }

            for(int i = 0; i < stream.Length-2; i += 2)
            {
                builder3 += stream.Substring(i, 2) + " ";
            }

            Debug.Log("EST PIXELS: " + pixel);
            Debug.Log("IMAGE DATA SUBBLOCK 1 DATA: " + builder3);
            Debug.Log("IMAGE BLOCK BINARY: " + binary2);
            Debug.Log("IMAGE DATA DECRYPTED: " + builder);
        }
    }
}
