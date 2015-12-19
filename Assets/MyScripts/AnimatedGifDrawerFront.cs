﻿using System.Collections.Generic;

using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System;

public class AnimatedGifDrawerFront : MonoBehaviour
{
    public string loadingGifPath;
    public float speed = 1;
    public string pName;

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
    private System.Drawing.Image gifImage;

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


    List<Texture2D> gifFrames = new List<Texture2D>();

    void Start()
    {
        positionPlaceHolderGO = GameObject.FindGameObjectWithTag("PTRPlace");
        positionPlaceHolder = positionPlaceHolderGO.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        while (hasWWW == false)
        {
            //Debug.Log("in while loop");
            if (this.GetComponent<PokemonCreatorFront>().name == "")
            {

            }
            else
            {
                url = "www.pkparaiso.com/imagenes/xy/sprites/animados/" + this.GetComponent<PokemonCreatorFront>().PokemonName.ToLower() + ".gif";
                StartCoroutine(WaitForRequest(positionPlaceHolderGO, url));
                hasWWW = true;
            }
        }
    }

    void OnGUI()
    {

        if (canOnGUI) {
            height = Screen.height / nativeHeight;
            heightCalc = positionPlaceHolder.y;
            width = Screen.width / nativeWidth;
            widthCalc = positionPlaceHolder.x;

            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(width, height, 1));
            //Debug.Log((gifFrames[0].height * 3.5f) / 50f);
            float temp = gifFrames[0].width * 2.5f;
            GUI.DrawTexture(new Rect(750, 50, gifFrames[0].width * 2.5f, gifFrames[0].height * 2.5f), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
        }
    }

    IEnumerator WaitForRequest(GameObject go, string url)
    {
        www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            //Debug.Log("WWW Ok!: ");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
        finishedWWW = true;
    }

    public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
    {
        System.Drawing.Image returnImage;
        if (finishedWWW == false)
        {
            Debug.Log("Called too early");
            return null;
        }
        if (byteArrayIn == null)
        {
            Debug.Log("Null byte array");
            return null;
        }
        Debug.Log("KB array in length: " + Mathf.RoundToInt(byteArrayIn.Length / 1000f));

        try
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            returnImage = System.Drawing.Image.FromStream(ms);
            finishedWWW = true;
            return returnImage;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message.ToString());
            return null;
        }

    }

    public void loadImage()
    {
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
        canOnGUI = true;
    }
}