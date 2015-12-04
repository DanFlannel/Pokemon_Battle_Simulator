using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class handles the animations for the Player, or the Pokemon with its back facing the camera.
/// </summary>
public class AnimatedGifDrawerBack : MonoBehaviour
{

    /***************************
        Public Variables
    ****************************/
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

    private bool hasWWW = false;
    private bool canOnGUI = false;

    private string url;
    private WWW www;
    private System.Drawing.Image gifImage;

    private List<Texture2D> gifFrames = new List<Texture2D>();

    void Start()
    {
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
            height = gifImage.Height / 2f;    //y
            heightCalc = positionPlaceHolder.y;
            width = gifImage.Width /2f;      //x
            widthCalc = positionPlaceHolder.x;

            GUI.DrawTexture(new Rect(Screen.width/4f + widthCalc, Screen.height/3f + (heightCalc* percentage), gifFrames[0].width * percentage, gifFrames[0].height * percentage), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
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
        if (finishedWWW == false)
        {
            Debug.Log("Called too early");
        }
        if (byteArrayIn == null)
        {
            Debug.Log("Null byte array");
        }

        Debug.Log("KB array in length: " + Mathf.RoundToInt(byteArrayIn.Length/1000f));
       

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
    public System.Drawing.Bitmap byteArrayToBitMap(byte[] data){
       System.Drawing.Bitmap bmp;

       System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
       bmp = (System.Drawing.Bitmap)ic.ConvertFrom(data);
       //Debug.Log("Converted byteArray to bit map");
       return bmp;

   }

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
                    frameTexture.SetPixel(frame.Width - 1 + x, -y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
                }
            frameTexture.Apply();
            gifFrames.Add(frameTexture);
        }
        //Debug.Log("Starting ON GUI!");
        canOnGUI = true;
    }
}