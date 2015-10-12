using System.Collections.Generic;

using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System;

public class AnimatedGifDrawerFront : MonoBehaviour
{
    public string loadingGifPath;
    public float speed = 1;
    public Vector2 drawPosition;
    public string pName;

    public float width;
    public float height;
    public float percentage;
    public GameObject positionPlaceHolderGO;
    public Vector2 positionPlaceHolder;
    public Text debugText;
    private SpriteImageArray sia;
    private string url;
    private WWW www;
    public bool finishedWWW = false;
    public bool hasWWW = false;
    public bool canOnGUI = false;

    List<Texture2D> gifFrames = new List<Texture2D>();

    void Start()
    {


        percentage = 1f;
        positionPlaceHolderGO = GameObject.FindGameObjectWithTag("PTRPlace");
        positionPlaceHolder = positionPlaceHolderGO.transform.position;

    }

    void Update()
    {
        while (hasWWW == false)
        {
            Debug.Log("in while loop");
            if (this.GetComponent<PokemonCreatorFront>().name == "")
            {

            }
            else
            {
                //debugText.text = "Name Found";
                url = "www.pkparaiso.com/imagenes/xy/sprites/animados-espalda/" + this.GetComponent<PokemonCreatorFront>().PokemonName.ToLower() + ".gif";

                StartCoroutine(WaitForRequest(positionPlaceHolderGO, url));
                hasWWW = true;
                //debugText.text = "hawWWW = true";
            }
        }
    }

    void OnGUI()
    {
        height = (float)Screen.height - 80f / percentage;

        //GUI.DrawTexture (new Rect (Screen.width-width, Screen.height - height, gifFrames [0].width * percentage, gifFrames [0].height * percentage), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
        if (canOnGUI)
            GUI.DrawTexture(new Rect(positionPlaceHolder.x, Screen.height - positionPlaceHolder.y, gifFrames[0].width * percentage, gifFrames[0].height * percentage), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);

    }

    IEnumerator WaitForRequest(GameObject go, string url)
    {
        www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.texture.name);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
        debugText.text = "finishedWWW = true";
        finishedWWW = true;
    }

    public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
    {
        System.Drawing.Image returnImage;
        if (finishedWWW == false)
        {
            Debug.Log("Called too early");
        }
        if (byteArrayIn == null)
        {
            Debug.Log("Null byte array");
        }

        Debug.Log("Bytra array in length: " + byteArrayIn.GetLongLength(0));


        try
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            returnImage = System.Drawing.Image.FromStream(ms);     //MAIN SOURCE OF ERROR HERE
            debugText.text = "System.Image Created";
            Debug.Log("Created image from stream");
            finishedWWW = true;
            return returnImage;
        }
        catch (Exception e)
        {
            debugText.text = e.Message.ToString();
            return null;
            /*System.Drawing.Bitmap bmp = byteArrayToBitMap(byteArrayIn);
            returnImage = System.Drawing.Image.FromHbitmap(bmp.GetHbitmap());
            Debug.Log("Created image from hbitmap");
            finishedWWW = true;*/
        }

    }

    public System.Drawing.Bitmap byteArrayToBitMap(byte[] data)
    {
        System.Drawing.Bitmap bmp;

        System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
        bmp = (System.Drawing.Bitmap)ic.ConvertFrom(data);
        Debug.Log("Converted byteArray to bit map");
        return bmp;

    }

    public void loadImage()
    {
        Debug.Log("Called Load Image FRONT");
        System.Drawing.Image gifImage = ByteArrayToImage(www.bytes);

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