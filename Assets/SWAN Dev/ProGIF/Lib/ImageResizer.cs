using UnityEngine;

public class ImageResizer
{
	/// <summary>
	/// Resize the texture (keep ratio). GetPixels32/SetPixels32 is used for faster speed than GetPixels/SetPixels
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="texOrigin">Origin texture.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	public Texture2D ResizeTexture32_KeepRatio(Texture2D texOrigin, int width, int height)
	{
		int iW = width;
		int iH = height;

		float originRatio = (float)texOrigin.width/(float)texOrigin.height;
		float targetRatio = (float)width/(float)height;

		if(originRatio > targetRatio)
		{
			iW = width;
			iH = (int)((float)iW / originRatio);
		}
		else if(originRatio < targetRatio)
		{
			iH = height;
			iW = (int)((float)iH * originRatio);
		}

		Texture2D resizeTex = ResizeTexture32(texOrigin, iW, iH);

		Texture2D texResult = new Texture2D(width, height);
		// Replace all pixels with a zero-ed pixel array (ensure alpha value equal zero)
		texResult.SetPixels32(new Color32[width * height]);
		texResult.SetPixels32((int)((float)(width-iW)/2f), (int)((float)(height-iH)/2f), iW, iH, resizeTex.GetPixels32());
		texResult.Apply();

		Texture2D.Destroy(resizeTex);

		return texResult;
	}

	/// <summary>
	/// Resize the texture (keep ratio). GetPixels32/SetPixels32 is used for faster speed than GetPixels/SetPixels
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="texOrigin">Origin texture.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	public Texture2D ResizeTexture32(Texture2D texOrigin, int width, int height)
	{
		int iW = width;
		int iH = height;

		Color32[] texResize = new Color32[iW * iH];
		Color32[] texBase = texOrigin.GetPixels32();

		int _iBaseWidth = texOrigin.width;
		int _iBaseHeight = texOrigin.height;

		float fRatioX = 1.0f / ((float)iW / (_iBaseWidth - 1));
		float fRatioY = 1.0f / ((float)iH / (_iBaseHeight - 1));

		for (int y = 0; y < iH; y++)
		{
			int iYFloor = (int)Mathf.Floor(y * fRatioY);
			float fYLerp = y * fRatioY - iYFloor;

			int iY1 = iYFloor * _iBaseWidth;
			int iY2 = (iYFloor + 1) * _iBaseWidth;
			int iYw = y * iW;

			for (int x = 0; x < iW; x++)
			{
				int iXFloor = (int)Mathf.Floor(x * fRatioX);
				float fXLerp = x * fRatioX - iXFloor;

				// Bilinear filtering
				texResize[iYw + x] = Color32.Lerp(Color32.Lerp(texBase[iY1 + iXFloor], texBase[iY1 + iXFloor + 1], fXLerp),
					Color32.Lerp(texBase[iY2 + iXFloor], texBase[iY2 + iXFloor + 1], fXLerp), fYLerp);
			}
		}

		Texture2D texResult = new Texture2D(iW, iH);
		texResult.SetPixels32(texResize);
		texResult.Apply();

		Texture2D.Destroy(texOrigin);

		return texResult;
	}

	/// <summary>
	/// Resize the texture (keep ratio). GetPixels/SetPixels is used for higher quality than GetPixels32/SetPixels32
	/// </summary>
	/// <returns>The texture (keep ratio).</returns>
	/// <param name="texOrigin">Origin texture.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	public Texture2D ResizeTexture_KeepRatio(Texture2D texOrigin, int width, int height)
	{
		int iW = width;
		int iH = height;

		float originRatio = (float)texOrigin.width/(float)texOrigin.height;
		float targetRatio = (float)width/(float)height;

		if(originRatio > targetRatio)
		{
			iW = width;
			iH = (int)((float)iW / originRatio);
		}
		else if(originRatio < targetRatio)
		{
			iH = height;
			iW = (int)((float)iH * originRatio);
		}

		Texture2D resizeTex = ResizeTexture(texOrigin, iW, iH);

		Texture2D texResult = new Texture2D(width, height);
		texResult.SetPixels((int)((float)(width-iW)/2f), (int)((float)(height-iH)/2f), iW, iH, resizeTex.GetPixels());
		texResult.Apply();

		Texture2D.Destroy(resizeTex);

		return texResult;
	}

	/// <summary>
	/// Resize the texture (keep ratio). GetPixels/SetPixels is used for higher quality than GetPixels32/SetPixels32
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="texOrigin">Origin texture.</param>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	public Texture2D ResizeTexture(Texture2D texOrigin, int width, int height)
	{
		int iW = width;
		int iH = height;

		Color[] texResize = new Color[iW * iH];
		Color[] texBase = texOrigin.GetPixels();

		int _iBaseWidth = texOrigin.width;
		int _iBaseHeight = texOrigin.height;

		float fRatioX = 1.0f / ((float)iW / (_iBaseWidth - 1));
		float fRatioY = 1.0f / ((float)iH / (_iBaseHeight - 1));

		for (int y = 0; y < iH; y++)
		{
			int iYFloor = (int)Mathf.Floor(y * fRatioY);
			float fYLerp = y * fRatioY - iYFloor;

			int iY1 = iYFloor * _iBaseWidth;
			int iY2 = (iYFloor + 1) * _iBaseWidth;
			int iYw = y * iW;

			for (int x = 0; x < iW; x++)
			{
				int iXFloor = (int)Mathf.Floor(x * fRatioX);
				float fXLerp = x * fRatioX - iXFloor;

				// Bilinear filtering
				texResize[iYw + x] = Color.Lerp(Color.Lerp(texBase[iY1 + iXFloor], texBase[iY1 + iXFloor + 1], fXLerp),
					Color.Lerp(texBase[iY2 + iXFloor], texBase[iY2 + iXFloor + 1], fXLerp), fYLerp);
			}
		}

		Texture2D texResult = new Texture2D(iW, iH);
		texResult.SetPixels(texResize);
		texResult.Apply();

		Texture2D.Destroy(texOrigin);

		return texResult;
	}
}