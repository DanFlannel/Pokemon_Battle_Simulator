using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class converts the images in the resource folder to a readable GIF by Unity
/// </summary>
public class AnimatedGifDrawerBack : MonoBehaviour
{
	public string loadingGifPath;
	public float speed = 1;
	public Vector2 drawPosition;
	public string pName;
	public bool test = false;
	public float width;
	public float height;
	public float percentage;
	public Vector2 positionPlaceHolder;
	
	List<Texture2D> gifFrames = new List<Texture2D> ();
	void Start ()
	{
		percentage = 1.3f;
		positionPlaceHolder = GameObject.FindGameObjectWithTag("PBLPlace").transform.position;
	}
	
	void OnGUI ()
	{
        //GUI.DrawTexture (new Rect (drawPosition.x, drawPosition.y, 200/*gifFrames [0].width*/, 200/*gifFrames [0].height*/), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
		//GUI.DrawTexture (new Rect (drawPosition.x, drawPosition.y, (int)((float)gifFrames [0].width * 1.5f),(int)((float)gifFrames [0].height), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
		//height = (float)Screen.height - 100f * percentage;
        //GUI.DrawTexture (new Rect (Screen.width-width, Screen.height - height, gifFrames [0].width * percentage, gifFrames [0].height * percentage), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
        GUI.DrawTexture (new Rect (positionPlaceHolder.x/1.5f, positionPlaceHolder.y/1.5f, gifFrames[0].width * percentage, gifFrames[0].height * percentage), gifFrames[(int)(Time.frameCount * speed) % gifFrames.Count]);
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

	public void loadImage ()
	{

        string[] path = pathGen();

        Debug.Log(path[0]);
        Debug.Log(loadingGifPath);
        //Debug.Log(Resources.Load(temp));
        System.Drawing.Image gifImage = System.Drawing.Image.FromFile(path[0].ToString());

        var dimension = new FrameDimension (gifImage.FrameDimensionsList [0]);
		int frameCount = gifImage.GetFrameCount (dimension);
		for (int i = 0; i < frameCount; i++) {
			gifImage.SelectActiveFrame (dimension, i);
			var frame = new Bitmap (gifImage.Width, gifImage.Height);
			System.Drawing.Graphics.FromImage (frame).DrawImage (gifImage, Point.Empty);
			var frameTexture = new Texture2D (frame.Width, frame.Height);
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