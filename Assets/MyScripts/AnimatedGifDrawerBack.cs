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


    private Dictionary<int, string> codeTable = new Dictionary<int, string>();
    private FrameDimensions Frame = new FrameDimensions();
    public List<FrameDimensions> DimensionList = new List<FrameDimensions>();
    public List<GifFrame> GifFrames = new List<GifFrame>();

    GifGlobalColorTable.GlobalColorTable GlobalColorTable = new GifGlobalColorTable.GlobalColorTable();
    GifHeader.Header Header = new GifHeader.Header();
    GifLogicalScreenDescriptor.LogicalScreenDescriptor LogicalScreenDescriptor = new GifLogicalScreenDescriptor.LogicalScreenDescriptor();
    GifGraphicsControlExtension.GraphicsControlExtension GraphicsControlExtension = new GifGraphicsControlExtension.GraphicsControlExtension();
    GifImageDescriptor.ImageDescriptor ImageDescriptor = new GifImageDescriptor.ImageDescriptor();
    GifApplicationExtension.ApplicationExtension ApplicationExtensionBlock = new GifApplicationExtension.ApplicationExtension();
    GifImageData.ImageData ImageData = new GifImageData.ImageData();


    //private string ImageDescriptor;

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
        Init();
    }

    void Init()
    {
        ApplicationExtensionBlock.bits = 19 * 2;
        GraphicsControlExtension.bits = 8 * 2;
        LogicalScreenDescriptor.bits = 7 * 2;
        Header.bits = 6 * 2;
        ImageDescriptor.bits = 10 * 2;
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
                //url = "www.pkparaiso.com/imagenes/xy/sprites/animados-espalda/" + this.GetComponent<PokemonCreatorBack>().PokemonName.ToLower() + ".gif";
                url = "www.pkparaiso.com/imagenes/xy/sprites/animados-espalda/pikachu.gif";
                Debug.LogError("URL: " + url);

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

        Debug.Log("Array in length: " + Mathf.RoundToInt(byteArrayIn.Length));

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
        Debug.Log("Dimensions: Frames: " + frameCount + " Width: " + gifImage.Width + " Height: " + gifImage.Height + " Pixels: " + (gifImage.Width * gifImage.Height));
        int width = 0;
        int height = 0;
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

        int myframeCount = GifHelper.findFrameCount(hex);

        //here we are splitting up the file for our own purposes into each associated block
        Header.Set(hex.Substring(0, Header.bits));
        curGifByteIndex += Header.bits / 2;
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length / 2));

        LogicalScreenDescriptor.Set(hex.Substring(Header.bits, LogicalScreenDescriptor.bits));
        curGifByteIndex += LogicalScreenDescriptor.bits / 2;

        LogicalScreenDescriptor.DebugLog();
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length / 2));

        GlobalColorTable.Set(hex.Substring(curGifByteIndex * 2, LogicalScreenDescriptor.GlobalColorTableSize * 2));
        curGifByteIndex += LogicalScreenDescriptor.GlobalColorTableSize;

        GlobalColorTable.DebugLog();
        Debug.LogWarning("GLOBAL COLOR TABLE LENGTH: " + LogicalScreenDescriptor.GlobalColorTableSize);
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length / 2));

        GraphicsControlExtension.Set(hex.Substring(curGifByteIndex * 2, GraphicsControlExtension.bits));
        curGifByteIndex += GraphicsControlExtension.bits / 2;

        GraphicsControlExtension.DebugLog();
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length / 2));

        ApplicationExtensionBlock.Set(hex.Substring(curGifByteIndex * 2, ApplicationExtensionBlock.bits));
        curGifByteIndex += ApplicationExtensionBlock.bits / 2;

        ApplicationExtensionBlock.DebugLog();
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length / 2));

        ImageDescriptor.Set(hex.Substring(curGifByteIndex * 2, ImageDescriptor.bits));
        curGifByteIndex += ImageDescriptor.bits / 2;

        ImageDescriptor.DebugLog();
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length / 2));
        

        //Now that we have everything setup we are on to the drawing data
        int ImageDataLength = findLengthOfImageData(hex.Substring(curGifByteIndex * 2));
        ImageData.Set(hex.Substring(curGifByteIndex * 2, ImageDataLength));
        ImageData.bits = ImageDataLength;
        curGifByteIndex += ImageData.bits/2;
        Debug.LogWarning("CUR INDEX: " + curGifByteIndex + "/" + (hex.Length/2));
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


    private int findLengthOfImageData(string hex)
    {
        int length = 0;
        for (int i = (curGifByteIndex * 2); i < hex.Length - 4; i += 2)
        {
            if (hex.Substring(i, 4) == "21F9")
            {
                Debug.LogWarning("we found a GCE Block at byte : " + (i + (curGifByteIndex * 2)));
                Debug.LogWarning("IMAGE DATA LENGTH: " + i);
                return i;
            }
        }
        return -1;
    }

    private void AddFramDimensions(GifImageDescriptor.ImageDescriptor id)
    {
        FrameDimensions frame = new FrameDimensions();
        frame.Set( id.Left, id.Top,id.Width, id.Height);
        DimensionList.Add(frame);
    }

    //The likely hood of this next coding working is minimal at best but I will do my best anyways
    private void LZW_Decompress(string hex)
    {
        List<int> data = new List<int>();
        //convert the strig data to a list of integers
        for (int i = 0; i < hex.Length; i += 2)
        {
            string temp = hex.Substring(i, 2);
            data.Add(GifHelper.HexToDecimal(temp));
        }

        //create our dictionary or code table
        //our dictionary is the doce table
        for (int i = 0; i < 256; i++)
        {
            codeTable.Add(i, ((char)i).ToString());
        }
    }

    public struct FrameDimensions
    {
        public int left { get; private set; }
        public int top { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        public void Set(int l, int t, int w, int h)
        {
            left = l;
            top = t;
            width = w;
            height = h;
        }
    }

    public struct GifFrame
    {
        public GifGraphicsControlExtension.GraphicsControlExtension GCE { get; set; }
        public GifImageDescriptor.ImageDescriptor desciptor { get; set; }
        public GifImageData.ImageData data { get; set; }

        public void Set(GifGraphicsControlExtension.GraphicsControlExtension g, GifImageDescriptor.ImageDescriptor id, GifImageData.ImageData d)
        {
            GCE = g;
            desciptor = id;
            data = d;
        }
    }
}