using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class AnimatedGifDrawerBack : MonoBehaviour
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


        percentage = 1.3f;
        positionPlaceHolderGO = GameObject.FindGameObjectWithTag("PBLPlace");
        positionPlaceHolder = positionPlaceHolderGO.transform.position;

    }

    void Update()
    {
        while (hasWWW == false)
        {
            Debug.Log("in while loop");
            if (this.GetComponent<PokemonCreatorBack>().name == "")
            {

            }
            else
            {
                debugText.text = "Name Found";
                url = "www.pkparaiso.com/imagenes/xy/sprites/animados-espalda/" + this.GetComponent<PokemonCreatorBack>().PokemonName.ToLower() + ".gif";

                StartCoroutine(WaitForRequest(positionPlaceHolderGO, url));
                hasWWW = true;
                debugText.text = "hawWWW = true";
            }
        }
    }

    void OnGUI()
    {
        height = (float)Screen.height - 80f / percentage;

        //GUI.DrawTexture (new Rect (Screen.width-width, Screen.height - height, gifFrames [0].width * percentage, gifFrames [0].height * percentage), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
        if (canOnGUI)
            GUI.DrawTexture(new Rect(positionPlaceHolder.x, positionPlaceHolder.y, gifFrames[0].width * percentage, gifFrames[0].height * percentage), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);

    }

    private string[] pathGen()
    {
        loadingGifPath = Application.dataPath + "/Resources" + "/Sprites/" + "Front/" + pName + ".gif";
        string temp = "Sprites/" + "Front/" + pName + ".gif";
        string temp2 = Application.dataPath + "/Resources" + "/Sprites/" + "Front/";
        string temp3 = pName + "*";
        string[] path = Directory.GetFiles(temp2, temp3);

        return path;
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
        if (finishedWWW == false)
        {
            Debug.Log("Called too early");
        }
        if (byteArrayIn == null)
        {
            Debug.Log("Null byte array");
            return null;
        }
        byteArrayIn.Initialize();
        MemoryStream ms = new MemoryStream(byteArrayIn);
        System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
        finishedWWW = true;
        debugText.text = "System.Image Created";
        return returnImage;
    }

    public void loadImage()
    {
        Debug.Log("Called Load Image BACK");
        debugText.text = "Called Load Image BACK";
        System.Drawing.Image gifImage = ByteArrayToImage(www.bytes);


        FrameDimension dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
        int frameCount = gifImage.GetFrameCount(dimension);
        for (int i = 0; i < frameCount; i++)
        {
            gifImage.SelectActiveFrame(dimension, i);
            Bitmap frame = new Bitmap(gifImage.Width, gifImage.Height);
            System.Drawing.Graphics.FromImage(frame).DrawImage(gifImage, Point.Empty);
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
        Debug.Log("Starting ON GUI!");
        debugText.text = "Starting OnGUI";
        canOnGUI = true;
    }
}