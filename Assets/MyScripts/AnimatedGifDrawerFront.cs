using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class converts the images in the resource folder to a readable GIF by Unity
/// </summary>
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
    public Text debugText;
    private SpriteImageArray sia;
    private string assetPath;
	
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

    public IEnumerator pathGen()
    {
        if (Application.isEditor)
        {
            assetPath = Application.dataPath + "/Bundle.Unity3D";
        }
        else
        {
            assetPath = Application.dataPath + "/Bundle.Unity3D";
        }
        WWW load = new WWW(assetPath);
        yield return load;
        
    }

    public System.Drawing.Image Texture2Image(Texture2D texture)
    {
        System.Drawing.Image img;     

        texture.EncodeToPNG();
        string path = Application.dataPath + "/Resources/Sprites/Front/" + pName + ".gif";
        img = Bitmap.FromFile(path);

        return img;
    }

    private int getSpriteInArray(string name)
    {
        int index = 5;
        Debug.Log("Searching for " + name + " index in sprite array");
        for (int i = 0; i < sia.spriteArray.Length; i++)
        {
            if (name == sia.spriteArray[i].name.ToLower())
            {
                index = i;
                Debug.Log(pName + " sprite found");
                return index;
            }
        }
        Debug.Log("sprite not founc");
        return index;
    }

    public void loadImage()
    {

        //Debug.Log(loadingGifPath);
        //Debug.Log(Resources.Load(temp));
        int index = getSpriteInArray(pName);
        Texture2D temp = sia.spriteArray[index];
        System.Drawing.Image gifImage = Texture2Image(temp);

        FrameDimension dimension = new FrameDimension (gifImage.FrameDimensionsList [0]);
		int frameCount = gifImage.GetFrameCount (dimension);
		for (int i = 0; i < frameCount; i++)
        {
			gifImage.SelectActiveFrame (dimension, i);
			Bitmap frame = new Bitmap (gifImage.Width, gifImage.Height);
			System.Drawing.Graphics.FromImage (frame).DrawImage (gifImage, Point.Empty);
			Texture2D frameTexture = new Texture2D (frame.Width, frame.Height);
			for (int x = 0; x < frame.Width; x++)
				for (int y = 0; y < frame.Height; y++)
                {
					System.Drawing.Color sourceColor = frame.GetPixel (x, y);
					frameTexture.SetPixel (frame.Width - 1 + x, - y, new Color32 (sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
				}
			frameTexture.Apply ();
			gifFrames.Add (frameTexture);
		}
	}
}