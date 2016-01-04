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

    private Dictionary<int, string> codeTable = new Dictionary<int, string>();

    private int gifWidth;
    private int gifHeigt;
    private bool uses_GlobalColorTable;
    private int colorResolution;
    private bool uses_SortFlag;
    private bool user_input_flag;
    private bool transparentColorFlag;
    private string reservedForFutureUseGCE;
    private string GCE_DisposalMethod;
    private string transparentColorIndex;

    private string Header;
    private string LogicalScreenDescriptor;
    private string GlobalColorTable;

    private string ApplicationExtensionBlock;   //optional

    private string GraphicsControlExtension;
    private string ImageDescriptor;

    //drawing the image variables
    private int
        drawLeft, drawTop, drawWidth, drawHeight;
    private List<string> ImageDataBlocks = new List<string>();


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
    private List<Texture2D> custom_gifFrames = new List<Texture2D>();
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

            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(width, height, 1));

            //Debug.Log((gifFrames[0].height * 3.5f)/50f);
            float calc = (gifFrames[0].height * 3.5f) / 50f;
            float prediction = 0f;
            prediction = 150f + ((7.5f - calc) * 55.375f);
            prediction = Mathf.RoundToInt(prediction);
            //Debug.Log("predicted position " + prediction);
            GUI.DrawTexture(new Rect(50, prediction, gifFrames[0].width * 3.5f, gifFrames[0].height * 3.5f), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
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
        Debug.Log("Dimensions: Frames: " + frameCount + " Width: " + gifImage.Width + " Height: " + gifImage.Height);
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

        //this is the one that we as humans can read
        string hex2 = ByteArrayToStringReadable(byteArrayIn);
        file2.Write(hex2);
        file2.Close();

        int myframeCount = findFrameCount(hex);
        Debug.LogWarning("frame count: " + frameCount);

        //here we are splitting up the file for our own purposes into each associated block
        Header = gif_Header(hex);

        LogicalScreenDescriptor = logicalScreenDescriptor(hex);

        int gTableLen = LSD_packetfield(LogicalScreenDescriptor);

        GlobalColorTable = hex.Substring(26, gTableLen * 2);
        Debug.LogWarning("Global Table: " + GlobalColorTable);

        GraphicsControlExtension = graphicsControlExtension(hex);

        ApplicationExtensionBlock = applicationExtensionBlock(hex);

        ImageDescriptor = ImageDescriptorBlock(hex);

        //Now that we have everything setup we are on to the drawing data

        int ImageDataLength = findLengthOfImageData(hex);
        int bytesInImageData = ImageDataLength / 2;
        string ImageData = hex.Substring(curGifByteIndex * 2, ImageDataLength);
        splitDataIntoBlocks(ImageData);
        curGifByteIndex += bytesInImageData;

        Debug.Log("Image Data for 1st frame: " + ImageData);
        GraphicsControlExtension = graphicsControlExtension(hex);
        ImageDescriptor = ImageDescriptorBlock(hex);

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
        for (int i = 0; i < 11; i += 2)
        {
            header += hex.Substring(i, 2);
        }
        curGifByteIndex += header.Length / 2; ;
        if (header.Contains("474946"))
        {
            Debug.logger.Log("gif header: " + header + " cur index: " + curGifByteIndex);
            if (header.Contains("383961"))
            {
                Debug.LogWarning("Gif Version: 87A");
            }
            else if (header.Contains("383961"))
            {
                Debug.LogWarning("Gif Version: 87A");
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
    public string logicalScreenDescriptor(string hex)
    {
        //the next 7 bytes after the header, this is always 7 bytes so we can hard code this
        string logicalScreenDescriptor = "";
        int bytesToAdd = 7 * 2;
        int length = (curGifByteIndex * 2) + bytesToAdd;
        for (int i = curGifByteIndex * 2; i < length; i += 2)
        {
            logicalScreenDescriptor += hex.Substring(i, 2);
        }

        string widthHex = logicalScreenDescriptor.Substring(2, 2) + logicalScreenDescriptor.Substring(0, 2);
        string heightHex = logicalScreenDescriptor.Substring(6, 2) + logicalScreenDescriptor.Substring(4, 2);

        gifWidth = HexToDecimal(widthHex);
        gifHeigt = HexToDecimal(heightHex);

        Debug.LogWarning("width: " + gifWidth + " height: " + gifHeigt);

        curGifByteIndex += logicalScreenDescriptor.Length / 2;
        Debug.Log("gif logical_descriptor: " + logicalScreenDescriptor + " cur index: " + curGifByteIndex);

        return logicalScreenDescriptor;
    }

    /// <summary>
    /// Logical Screen Descriptor packet field conversion
    /// </summary>
    /// <param name="descriptor"></param>
    /// <returns></returns>
    public int LSD_packetfield(string descriptor)
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

        long length = Convert.ToInt64(binary, 2);   //converts to decimal
        float size = length + 1;
        float final = Mathf.Pow(2, size) * 3;
        curGifByteIndex += (int)final;
        Debug.Log("Size of Color Table DECIMAL: " + final + " cur index: " + curGifByteIndex);
        return (int)final;
    }

    /// <summary>
    /// GIFs can have either a global color table or local color tables for each sub-image. 
    /// Each color table consists of a list of RGB (Red-Green-Blue) color component intensities, three bytes for each color
    /// the length of the global color table is 2^(N+1) entries where N is the value of the color depth field in the logical screen descriptor
    /// The table will take up 3*2^(N+1) bytes in the stream.
    /// </summary>
    /// <param name="hex"></param>
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
        Debug.LogWarning("The Gif uses the global color table: " + uses_GlobalColorTable);
    }

    private void set_colorResolution(string hex)
    {
        Debug.LogWarning("Color Resolution input: " + hex);
        long resolution = Convert.ToInt64(hex, 2);
        colorResolution = (int)resolution + 1;
        Debug.LogWarning("Gif Color Resolution: " + colorResolution);
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
        Debug.LogWarning("The Gif sorts the global color table: " + uses_SortFlag);
    }


    /// <summary>
    /// Graphic control extension blocks are used to specify transparency settings and control animations. They are an optional GIF89 extension.
    /// 8 bytes long
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    private string graphicsControlExtension(string hex)
    {
        string GCE = "";
        int bytesToAdd = 8;
        int subStringLen = bytesToAdd * 2;
        GCE = hex.Substring(curGifByteIndex * 2, subStringLen);
        curGifByteIndex += bytesToAdd;
        Debug.Log("Graphics control extension: " + GCE + " cur index: " + curGifByteIndex);
        if (GCE.Substring(0, 2) != "21")
        {
            Debug.LogWarning("Not an extension block");
        }
        if (GCE.Substring(2, 2) != "F9")
        {
            Debug.LogWarning("this is not an graphics control extension block");
        }
        if (GCE.Substring(14, 2) != "00")
        {
            Debug.LogWarning("GCE Terminator missing");
        }

        string binary = GCE.Substring(4, 2);

        Debug.LogWarning("GCE byte size: " + binary);
        binary = HexStringToBinary(binary);
        long length = Convert.ToInt64(binary, 2);
        Debug.LogWarning("GCE block size: " + length);


        GCE_packetfield(GCE);

        string delayTime = GCE.Substring(8, 4);
        Debug.LogWarning("GCE Delay Time: " + delayTime);
        if (transparentColorFlag)
        {
            transparentColorIndex = GCE.Substring(12, 2);
            Debug.Log("Transparent Color Index HEX: " + transparentColorIndex);
            Debug.Log("Transparent Color Index DECIMAL: " + HexToDecimal(transparentColorIndex));

        }

        return GCE;
    }

    private void GCE_packetfield(string GCE)
    {
        string packetField = GCE.Substring(6, 2);
        Debug.LogWarning("Packet Field HEX: " + packetField);
        packetField = HexStringToBinary(packetField);
        Debug.LogWarning("Packet Field BINARY: " + packetField);


        set_userInputFlag(packetField.Substring(6, 1));
        set_transparentColorFlag(packetField.Substring(7, 1));

    }

    private void set_userInputFlag(string hex)
    {
        if (hex == "0")
        {
            user_input_flag = false;
        }
        else if (hex == "1")
        {
            user_input_flag = true;
        }
        else
        {
            Debug.LogError("User input field error");
        }
        Debug.LogWarning("User Input Flag: " + user_input_flag);
    }

    private void set_transparentColorFlag(string hex)
    {
        if (hex == "0")
        {
            transparentColorFlag = false;
        }
        else if (hex == "1")
        {
            transparentColorFlag = true;
        }
        else
        {
            Debug.LogError("Transparent Color Flag error");
        }
        Debug.LogWarning("Transparent Color Flag: " + transparentColorFlag);
    }



    /// <summary>
    /// The GIF89 specification allows for application-specific information to be embedded in the GIF file itself. This capability is not much used.
    /// 19 bytes long
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    private string applicationExtensionBlock(string hex)
    {
        string appExtensionBlock = "";
        int bytesToAdd = 19;
        int subStringLen = bytesToAdd * 2;
        appExtensionBlock = hex.Substring(curGifByteIndex * 2, subStringLen);
        curGifByteIndex += bytesToAdd;
        Debug.Log("Application Extension: " + appExtensionBlock + " cur index: " + curGifByteIndex);
        return appExtensionBlock;
    }

    /// <summary>
    /// A single GIF file may contain multiple images. multiple images are normally used for animations. Each image begins with an image descriptor block. 
    /// This block is exactly 10 bytes long
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    private string ImageDescriptorBlock(string hex)
    {
        string descriptor = "";
        int bytesToAdd = 10;
        int subStringLen = bytesToAdd * 2;
        descriptor = hex.Substring(curGifByteIndex * 2, subStringLen);
        curGifByteIndex += bytesToAdd;
        Debug.Log("Image Descriptor: " + descriptor + " cur index: " + curGifByteIndex);
        if (descriptor.Substring(0, 2) != "2C")
        {
            Debug.LogError("This is not a descriptor");
        }

        string redrawLeftHex = descriptor.Substring(4, 2) + descriptor.Substring(2, 2);
        drawLeft = HexToDecimal(redrawLeftHex);
        Debug.LogWarning("Resize Left: " + drawLeft);

        string redrawTopHex = descriptor.Substring(8, 2) + descriptor.Substring(6, 2);
        drawTop = HexToDecimal(redrawTopHex);
        Debug.LogWarning("Resize Top: " + drawTop);

        string redrawWidthHex = descriptor.Substring(12, 2) + descriptor.Substring(10, 2);
        drawWidth = HexToDecimal(redrawWidthHex);
        Debug.LogWarning("Resize Width: " + drawWidth);

        string redrawHeightHex = descriptor.Substring(16, 2) + descriptor.Substring(14, 2);
        drawHeight = HexToDecimal(redrawHeightHex);
        Debug.LogWarning("Resize Height: " + drawHeight);

        string PacketField = descriptor.Substring(18, 2);
        Debug.LogWarning("PacketField: " + PacketField);

        return descriptor;
    }


    private int findLengthOfImageData(string hex)
    {
        int length = 0;
        for (int i = (curGifByteIndex * 2); i < hex.Length - 4; i += 2)
        {
            if (hex.Substring(i, 4) == "21F9")
            {
                Debug.LogWarning("we found a GCE Block");
                return length;
            }
            else
            {
                length += 2;
            }
        }
        return length;
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
    /// Hex to decimal
    /// </summary>
    /// <param name="hex">the hex you want to convert to decimal</param>
    /// <returns>decimal form of the hex input</returns>
    private int HexToDecimal(string hex)
    {
        int n = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        return n;
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

    private int findFrameCount(string hex)
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

        return frameCount;
    }

    /// <summary>
    /// This splits our Image Data into sub blocks based off of the LZW compression
    /// </summary>
    /// <param name="imageData">takes in a string of image data</param>
    private void splitDataIntoBlocks(string imageData)
    {
        int index ,total, subBlockLength;
        string temp;
        
        Debug.LogWarning("LZW minimum code size: " + imageData.Substring(0, 2));
        index = 2;
        total = 2;

        for (int i = 0; i < imageData.Length; i++)
        {
            subBlockLength = HexToDecimal(imageData.Substring(index, 2)) * 2;
            index += 2;
            if (subBlockLength == 0)
            {
                Debug.LogWarning("byte index in image data: " + index + " /" + imageData.Length.ToString());
                Debug.Log("number of sub blocks:" + ImageDataBlocks.Count);
                return;
            }
            Debug.LogWarning("sub block length: " + subBlockLength);
            total += subBlockLength + 2;
            
            temp = imageData.Substring(index, subBlockLength);
            index += subBlockLength;
            ImageDataBlocks.Add(temp);
            Debug.LogWarning("byte index in image data: " + index + " /" + imageData.Length.ToString());

        }
    }

    //The likely hood of this next coding working is minimal at best but I will do my best anyways
    private void LZW_Decompress(string hex)
    {
        List<int> data = new List<int>();
        //convert the strig data to a list of integers
        for (int i = 0; i < hex.Length; i += 2)
        {
            string temp = hex.Substring(i, 2);
            data.Add(HexToDecimal(temp));
        }

        //create our dictionary or code table
        //our dictionary is the doce table
        for (int i = 0; i < 256; i++)
        {
            codeTable.Add(i, ((char)i).ToString());
        }

    }


}