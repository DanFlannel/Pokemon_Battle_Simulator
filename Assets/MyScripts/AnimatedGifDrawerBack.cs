using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Text;

/// <summary>
/// This class handles the animations for the Player, or the Pokemon with its back facing the camera.
/// Refernce site used for learning all of this shit http://giflib.sourceforge.net/whatsinagif/bits_and_bytes.html
/// </summary>
public class AnimatedGifDrawerBack : MonoBehaviour
{
    //for our own hex to binary conversion
    private Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
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
    private int gifWidth;
    private int gifHeigt;
    private bool uses_GlobalColorTable;
    private int colorResolution;
    private bool uses_SortFlag;


    /***************************
        Public Variables
    ****************************/
    private int curGifByteIndex;
    public float speed = 1;
    public float percentage;

    public string pName;

    public bool finishedWWW = false;

    public GameObject positionPlaceHolderGO;
    private Vector2 positionPlaceHolder;
    

    /***************************
        Private Variables
    ****************************/
    //OnGUI variables for gif height and width
    private float width;
    private float widthCalc;
    private float height;
    private float heightCalc;

    private float nativeWidth = 1024;
    private float nativeHeight = 786;

    private bool hasWWW = false;
    private bool canOnGUI = false;

    private string url;
    private WWW www;
    private System.Drawing.Image gifImage;

    private List<Texture2D> gifFrames = new List<Texture2D>();
    private byte[] bytearrayholder;

    void Start()
    {
        curGifByteIndex = 0;
        positionPlaceHolderGO = GameObject.FindGameObjectWithTag("PBLPlace");
        positionPlaceHolder = positionPlaceHolderGO.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        while (hasWWW == false)
        {
            //Debug.Log("in while loop");
            if (this.GetComponent<PokemonCreatorBack>().name == "")
            {

            }
            else
            {
                //Debug.log("Name Found");
                url = "www.pkparaiso.com/imagenes/xy/sprites/animados-espalda/" + this.GetComponent<PokemonCreatorBack>().PokemonName.ToLower() + ".gif";

                StartCoroutine(WaitForRequest(positionPlaceHolderGO, url));
                hasWWW = true;
                //Debug.log("hawWWW = true");
            }
        }
    }

    /// <summary>
    /// OnGUI method called to draw the gif image brought in from the byte array
    /// </summary>
    void OnGUI()
    {
        height = (float)Screen.height - 80f / percentage;
        if (canOnGUI)
        {
            height = Screen.height / nativeHeight;
            heightCalc = positionPlaceHolder.y;
            width = Screen.width / nativeWidth;
            widthCalc = positionPlaceHolder.x;

            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(width, height,1));
            
            //Debug.Log((gifFrames[0].height * 3.5f)/50f);
            float calc = (gifFrames[0].height * 3.5f) / 50f;
            float prediction = 0f;
            prediction = 150f + ((7.5f-calc) * 55.375f);
            prediction = Mathf.RoundToInt(prediction);
            //Debug.Log("predicted position " + prediction);
            GUI.DrawTexture(new Rect(50,prediction, gifFrames[0].width * 3.5f, gifFrames[0].height * 3.5f), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
        }
    }

    /// <summary>
    /// This checks the url for being a proper url with the gif image.
    /// </summary>
    /// <param name="go"> a game object </param>
    /// <param name="url"> the url that the gif image is sotred in</param>
    /// <returns></returns>
    IEnumerator WaitForRequest(GameObject go, string url)
    {
        www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            //Debug.Log("WWW Ok!: " + www.texture.name);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
        //Debug.log("finishedWWW = true");
        finishedWWW = true;
    }

    /// <summary>
    /// This converts a byte array to a System.Drawing.Image type
    /// </summary>
    /// <param name="byteArrayIn"> the byte array to be converted</param>
    /// <returns></returns>
    public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
    {
        bytearrayholder = byteArrayIn;
        if (finishedWWW == false)
        {
            Debug.Log("Called too early");
        }
        if (byteArrayIn == null)
        {
            Debug.Log("Null byte array");
        }

        Debug.Log("KB array in length: " + Mathf.RoundToInt(byteArrayIn.Length));

        try
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            gifImage = System.Drawing.Image.FromStream(ms);     //MAIN SOURCE OF ERROR HERE
            //Debug.Log("Created image from stream");
            finishedWWW = true;
            return gifImage;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message.ToString());
            return null;
            /*System.Drawing.Bitmap bmp = byteArrayToBitMap(byteArrayIn);
            returnImage = System.Drawing.Image.FromHbitmap(bmp.GetHbitmap());
            Debug.Log("Created image from hbitmap");
            finishedWWW = true;*/
        }
        
    }

    /// <summary>
    /// This method converts the read in byte arry to a System.Drawing.Bitmap
    /// </summary>
    /// <param name="data"> the byte array to be convereted</param>
    /// <returns></returns>
    /*public System.Drawing.Bitmap byteArrayToBitMap(byte[] data){
       System.Drawing.Bitmap bmp;

       System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
       bmp = (System.Drawing.Bitmap)ic.ConvertFrom(data);
       //Debug.Log("Converted byteArray to bit map");
       return bmp;

   } */

    /// <summary>
    /// This handles loading all fo the data from the given url and converts it into a readable image type and then allows the OnGUI function to draw the gif
    /// </summary>
    public void loadImage()
    {
        //Debug.Log("Called Load Image BACK");
        gifImage = ByteArrayToImage(www.bytes);

        if (gifImage == null)
            return;

        var dimension = new System.Drawing.Imaging.FrameDimension(gifImage.FrameDimensionsList[0]);
        int frameCount = gifImage.GetFrameCount(dimension);
        int width;
        int height;
        for (int i = 0; i < frameCount; i++)
        {
            gifImage.SelectActiveFrame(dimension, i);
            var frame = new System.Drawing.Bitmap(gifImage.Width, gifImage.Height);
            System.Drawing.Graphics.FromImage(frame).DrawImage(gifImage, System.Drawing.Point.Empty);
            Texture2D frameTexture = new Texture2D(frame.Width, frame.Height);
            for (int x = 0; x < frame.Width; x++)
                for (int y = 0; y < frame.Height; y++)
                {
                    System.Drawing.Color sourceColor = frame.GetPixel(x, y);
                    frameTexture.SetPixel(frame.Width + x + 1, -y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
                }
            frameTexture.Apply();
            gifFrames.Add(frameTexture);
            width = frame.Width;
            height = frame.Height;
        }
        byteArrayTextConversion(bytearrayholder, gifImage.Width, gifImage.Height, gifImage.GetFrameCount(dimension));
        //Debug.Log("Starting ON GUI!");
        canOnGUI = true;
    }

    private void byteArrayTextConversion(byte[] byteArrayIn, int width, int height, int frameCount)
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\Flannel\\Desktop\\ImageBytes.txt");    //general byte array in
        System.IO.StreamWriter file2 = new System.IO.StreamWriter("C:\\Users\\Flannel\\Desktop\\ImageBytes2.txt");

        //this is the hexidecimal file
        string hex = ByteArrayToString(byteArrayIn);
        file.Write(hex);
        file.Close();
        Debug.logger.Log("est width: " + byteArrayIn[6] + " est height: " + bytearrayholder[8] + " est frames: ");

        //here we are splitting up the file for our own purposes
        string header = gif_Header(hex);

        string descriptor = gif_logicalScreenDescriptor(hex);

        int gTableLen = descriptor_PacketFieldConversion(descriptor);
        string gTable = hex.Substring(26, gTableLen*2);
        Debug.LogWarning("Global Table: " + gTable);

        graphicsControlExtension(hex);


        string hex2 = ByteArrayToStringReadable(byteArrayIn);
        file2.Write(hex2);
        file2.Close();
        Debug.Log("width: " + width + " height: " + height + " count: " + frameCount);
        //71 73 70 56 57 97 standard first six bytes in the text file
        //115   0   82 these are the next three bytes in the text file ex:
        //Width     Height 
        
    }

    #region outputting the data to a text file
    public static string ByteArrayToString(byte[] ba)
    {
        string hex = BitConverter.ToString(ba);
        return hex.Replace("-", "");
    }

    public static string ByteArrayToStringReadable(byte[] ba)
    {
        string hex = BitConverter.ToString(ba);
        return hex.Replace("-", " ");
    }
    #endregion

    /// <summary>
    /// this gets all of the information regarding the gif's header
    /// </summary>
    /// <returns>a string of the gif header</returns>
    public string gif_Header(string hex)
    {
        //this is the first 6 bytes of the file, this is always 6 bytes
        string header = "";
        for(int i = 0; i < 11; i+=2)
        {
            header += hex.Substring(i, 2);
        }
        curGifByteIndex += header.Length / 2; ;
        if (header.Contains("474946"))
        {
            Debug.logger.Log("gif header: " + header + " cur index: " + curGifByteIndex);
            if (header.Contains("383961"))
            {
                Debug.Log("Gif Version: 87A");
            }else if (header.Contains("383961"))
            {
                Debug.Log("Gif Version: 87A");
            }
            else
            {
                Debug.LogError("Unknown Gif Version");
            }
            return header;
        }
        else
        {
            Debug.LogError("This is not a GIF");
            return header;
        }
    }

    /// <summary>
    /// this handles the logical screen descriptor part of the gif packets
    /// </summary>
    /// <param name="hex">the entire hex string</param>
    /// <returns> the logical descriptor part of it</returns>
    public string gif_logicalScreenDescriptor(string hex)
    {
        //the next 7 bytes after the header, this is always 7 bytes so we can hard code this
        string logicalScreenDescriptor = "";
        int bytesToAdd = 7 * 2;
        int length = (curGifByteIndex * 2) +bytesToAdd;
        for(int i = curGifByteIndex * 2; i < length; i+=2)
        {
            logicalScreenDescriptor += hex.Substring(i, 2);
        }

        string widthHex = logicalScreenDescriptor.Substring(2, 2) + logicalScreenDescriptor.Substring(0,2);
        string heightHex = logicalScreenDescriptor.Substring(6,2) + logicalScreenDescriptor.Substring(4, 2);

        gifWidth = int.Parse(widthHex, System.Globalization.NumberStyles.HexNumber);
        gifHeigt = int.Parse(heightHex, System.Globalization.NumberStyles.HexNumber);

        Debug.LogWarning("width: " + gifWidth + " height: " + gifHeigt);

        curGifByteIndex += logicalScreenDescriptor.Length / 2;
        Debug.Log("gif logical_descriptor: " + logicalScreenDescriptor + " cur index: " + curGifByteIndex);
        
        


        return logicalScreenDescriptor;
    }

    public int descriptor_PacketFieldConversion(string descriptor)
    { //although we can usually assume this to be 768 we cannot be completely sure
        //this also should be noted that the packet field is always the 5th bit in the descriptor string
        string binary = HexStringToBinary(descriptor.Substring(8, 2));
        Debug.LogWarning("Binary byte: " + descriptor.Substring(8, 2));
        //we only need the last three bits of those two bytes
        
        Debug.LogWarning("Size of Color Table BINARY: " + binary);

        set_globalColorTableFlag(binary.Substring(0, 1));
        set_colorResolution(binary.Substring(1, 3));
        set_sortFlag(binary.Substring(4, 1));

        binary = binary.Substring(5, 3);
        Debug.LogWarning("Size bytes: " + binary);

        long length = Convert.ToInt64(binary, 2);
        float size = length + 1;
        float final = Mathf.Pow(2,size) * 3;
        curGifByteIndex += (int)final;
        Debug.Log("Size of Color Table DECIMAL: " + final + " cur index: " + curGifByteIndex);
        return (int)final;
    }

    private void set_globalColorTableFlag(string hex)
    {
        Debug.LogWarning("color table flag input: " + hex);
        if (hex == "0")
        {
            uses_GlobalColorTable = false;
        }
        else
        {
            uses_GlobalColorTable = true;
        }
        Debug.Log("The Gif uses the global color table: " + uses_GlobalColorTable);
    }

    private void set_colorResolution(string hex)
    {
        Debug.LogWarning("Color Resolution input: " + hex);
        long resolution = Convert.ToInt64(hex, 2);
        colorResolution = (int)resolution + 1;
        Debug.Log("Gif Color Resolution: " + colorResolution);
    }

    private void set_sortFlag(string hex)
    {
        Debug.LogWarning("sort flag input: " + hex);
        if (hex == "0")
        {
            uses_SortFlag = false;
        }
        else
        {
            uses_SortFlag = true;
        }
        Debug.Log("The Gif sorts the global color table: " + uses_SortFlag);
    }

    private string graphicsControlExtension(string hex)
    {
        string GCE = "";
        GCE = hex.Substring(curGifByteIndex * 2, 16);
        curGifByteIndex += 16;
        Debug.Log("Graphics control extension: " + GCE);
        if(GCE.Substring(0,2) != "21")
        {
            Debug.LogWarning("Not an extension block");
        }
        if(GCE.Substring(2,2) != "F9")
        {
            Debug.LogWarning("this is not an graphics control extension block");
        }
        if(GCE.Substring(14,2) != "00")
        {
            Debug.LogWarning("GCE Terminator missing");
        }

        return GCE;
    }

    /// <summary>
    /// Converts Hex to Binary
    /// Source: http://stackoverflow.com/questions/6617284/c-sharp-how-convert-large-hex-string-to-binary
    /// because why would i write my own..... 
    /// wait why am I writing my own gif reader.... oh right Unity hates GIFs andI love them... only for the purpose of this game do I love them
    /// </summary>
    /// <param name="hex">a hexidecimal string</param>
    /// <returns>the binary value of the hexidecimal string</returns>
    public string HexStringToBinary(string hex)
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
    /// Converts little endian hex to hex
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string LittleEndian(string num)
    {
        int number = Convert.ToInt32(num, 16);
        byte[] bytes = BitConverter.GetBytes(number);
        string retval = "";
        foreach (byte b in bytes)
            retval += b.ToString("X2");
        return retval;
    }


}