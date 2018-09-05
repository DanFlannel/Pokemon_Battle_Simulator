using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ImageRotator
{
	public enum Rotation { None, Left, Right, HalfCircle }

	#region ----- Rotate Image (IEnumerable Interface) -----
	public static Texture2D RotateImage_IEnumerable(Texture2D texture, Rotation rotation)
	{
		texture.SetPixels32(RotateColors_IEnumerable(texture, rotation));
		texture.Apply();
		return texture;
	}

	public static Color32[] RotateColors_IEnumerable(Texture2D texture, Rotation rotation)
	{
		Color32[] originalPixels = texture.GetPixels32();
		IEnumerable<Color32> rotatedPixels;

		if (rotation == Rotation.HalfCircle)
		{
			rotatedPixels = originalPixels.Reverse();
		}
		else
		{    
			// Rotate left:
			int[] firstRowPixelIndeces = Enumerable.Range(0, texture.height).Select(i => i * texture.width).Reverse().ToArray();
			rotatedPixels = Enumerable.Repeat(firstRowPixelIndeces, texture.width).SelectMany(
				(frpi, rowIndex) => frpi.Select(i => originalPixels[i + rowIndex])
			);

			if(rotation == Rotation.Right)
			{
				rotatedPixels = rotatedPixels.Reverse();
			}
		}
		return rotatedPixels.ToArray();
	}
	#endregion


	#region ----- Rotate Image (Width, Height Loop)  -----
	public static Texture2D RotateImage(Texture2D originTexture, Rotation rotation)
	{
		int H = originTexture.height;
		int W = originTexture.width;
		if(rotation == Rotation.Left || rotation == Rotation.Right)
		{
			H = originTexture.width;
			W = originTexture.height;
		}

		Texture2D result = new Texture2D(W, H);
		result.SetPixels32(RotateImageToColor32(originTexture, rotation));
		result.Apply();
		return result;
	}

	public static Color32[] RotateImageToColor32(Texture2D originTexture, Rotation rotation)
	{
		Color32[] originColors = originTexture.GetPixels32();
		Color32[] resultColors = new Color32[originColors.Length];

		int originH = originTexture.height;
		int originW = originTexture.width;

		int px = 0;
		int idx = -1;

		switch(rotation)
		{
		case Rotation.Left:
			for(int i=0; i<originW; i++)
			{
				for(int j=0; j<originH; j++)
				{
					px = ((originH - j) * originW) - (originW - i);
					idx++;
					resultColors[idx] = originColors[px];
				}
			}
			break;

		case Rotation.Right:
			for(int i=0; i<originW; i++)
			{
				for(int j=0; j<originH; j++)
				{
					px = (j * originW) + (originW - i - 1);
					idx++;
					resultColors[idx] = originColors[px];
				}
			}
			break;

		case Rotation.HalfCircle:
			int lastIdx = originColors.Length - 1;
			for(int i=0; i<originW; i++)
			{
				for(int j=0; j<originH; j++)
				{
					idx++;
					resultColors[idx] = originColors[lastIdx-idx];
				}
			}
			break;

		default:
			resultColors = originColors;
			break;
		}

		return resultColors;
	}
	#endregion

}
