using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;

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
		//Debug.Log("Width: " + gifFrames[0].width);
		//Debug.Log("Height: " + gifFrames[0].height);
		//GUI.DrawTexture (new Rect (drawPosition.x, drawPosition.y, 200/*gifFrames [0].width*/, 200/*gifFrames [0].height*/), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
		//GUI.DrawTexture (new Rect (drawPosition.x, drawPosition.y, (int)((float)gifFrames [0].width * 1.5f),(int)((float)gifFrames [0].height), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
		height = (float)Screen.height - 100f * percentage;
		GUI.DrawTexture (new Rect (Screen.width-width, Screen.height - height, gifFrames [0].width * percentage, gifFrames [0].height * percentage), gifFrames [(int)(Time.frameCount * speed) % gifFrames.Count]);
	}
	public void loadImage ()
	{
			loadingGifPath = Application.dataPath + "/Resources" + "/Sprites/" + "Back/" + pName + ".gif";

		//Debug.Log (loadingGifPath);
		var gifImage = Image.FromFile (loadingGifPath);
		//var gifImage = (Image)Resources.Load ("/Sprites/squirtle");
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