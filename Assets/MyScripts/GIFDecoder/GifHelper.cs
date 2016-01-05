using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;

public class GifHelper : MonoBehaviour {


    //for our own hex to binary conversion
    private static Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'a', "1010" },
    { 'b', "1011" },
    { 'c', "1100" },
    { 'd', "1101" },
    { 'e', "1110" },
    { 'f', "1111" }
};


    /// <summary>
    /// Hex to decimal
    /// </summary>
    /// <param name="hex">the hex you want to convert to decimal</param>
    /// <returns>decimal form of the hex input</returns>
    public static int HexToDecimal(string hex)
    {
        int n = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        return n;
    }

    /// <summary>
    /// Converts Hex to Binary
    /// Source: http://stackoverflow.com/questions/6617284/c-sharp-how-convert-large-hex-string-to-binary
    /// because why would i write my own..... 
    /// wait why am I writing my own gif reader.... oh right Unity hates GIFs andI love them... only for the purpose of this game do I love them
    /// </summary>
    /// <param name="hex">a hexidecimal string</param>
    /// <returns>the binary value of the hexidecimal string</returns>
    public static string HexToBinary(string hex)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in hex)
        {
            // This will crash for non-hex characters. You might want to handle that differently.
            result.Append(hexCharacterToBinary[char.ToLower(c)]);
        }
        return result.ToString();
    }

    /// <summary>
    /// http://stackoverflow.com/questions/5613279/c-sharp-hex-to-ascii
    /// </summary>
    /// <param name="hexString"></param>
    /// <returns></returns>
    public static string HexToASCII(string hexString)
    {
        try
        {
            string ascii = string.Empty;

            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hs = string.Empty;

                hs = hexString.Substring(i, 2);
                uint decval = System.Convert.ToUInt32(hs, 16);
                char character = System.Convert.ToChar(decval);
                ascii += character;

            }

            return ascii;
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

        return string.Empty;
    }

    /// <summary>
    /// Converts little endian hex to hex
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public  static string LittleEndian(string num)
    {
        int number = Convert.ToInt32(num, 16);
        byte[] bytes = BitConverter.GetBytes(number);
        string retval = "";
        foreach (byte b in bytes)
            retval += b.ToString("X2");
        return retval;
    }

    public static int findFrameCount(string hex)
    {
        int frameCount = 0;
        string search = "21f9";

        for (int i = 0; i < hex.Length - 4; i++)
        {
            string temp = hex.Substring(i, 4).ToLower();
            if (temp == search)
            {
                frameCount++;
            }
        }
        //Debug.LogWarning("frame count: " + frameCount);
        return frameCount;
    }

}
