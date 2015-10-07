using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class AnimatedGifDrawerFront : MonoBehaviour
{
	public string loadingGifPath;
	public float speed = 1;
	public Vector2 drawPosition;
	public string pName;

	public float width;
	public float height;
	public float percentage;
	public Vector2 positionPlaceHolder;
    private SpriteImageArray sia;
	
	List<Texture2D> gifFrames = new List<Texture2D> ();

	void Start ()
	{
		percentage = 1f;	
		positionPlaceHolder = GameObject.FindGameObjectWithTag("PTRPlace").transform.position;
        sia = GameObject.FindGameObjectWithTag("FrontImages").GetComponent<SpriteImageArray>();
	}
	
	void OnGUI ()
	{
		height = (float)Screen.height - 80f / percentage;

		//GUI.DrawTexture (new Rect (Screen.width-width, Screen.height - height, gifFrames [0].width * percentage, gifFrames [0].height * percentage), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
        GUI.DrawTexture(new Rect(positionPlaceHolder.x, Screen.height - positionPlaceHolder.y, gifFrames[0].width * percentage, gifFrames[0].height * percentage), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);

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

    public void loadImage()
    {

        string[] path = pathGen();

        Debug.Log (path[0]);
        Debug.Log(loadingGifPath);
        //Debug.Log(Resources.Load(temp));
        System.Drawing.Image gifImage = System.Drawing.Image.FromFile(path[0].ToString());

        FrameDimension dimension = new FrameDimension (gifImage.FrameDimensionsList [0]);
		int frameCount = gifImage.GetFrameCount (dimension);
		for (int i = 0; i < frameCount; i++) {
			gifImage.SelectActiveFrame (dimension, i);
			Bitmap frame = new Bitmap (gifImage.Width, gifImage.Height);
			System.Drawing.Graphics.FromImage (frame).DrawImage (gifImage, Point.Empty);
			Texture2D frameTexture = new Texture2D (frame.Width, frame.Height);
			for (int x = 0; x < frame.Width; x++)
				for (int y = 0; y < frame.Height; y++) {
					System.Drawing.Color sourceColor = frame.GetPixel (x, y);
					frameTexture.SetPixel (frame.Width - 1 + x, - y, new Color32 (sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
				}
			frameTexture.Apply ();
			gifFrames.Add (frameTexture);
		}
	}
}