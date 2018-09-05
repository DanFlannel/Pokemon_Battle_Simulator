using System;
using System.IO;
using UnityEngine;

internal class ProGifEncoder
{
	protected int m_Width;
	protected int m_Height;
	protected int m_Repeat = -1;                  // -1: no repeat, 0: infinite, >0: repeat count
	protected int m_FrameDelay = 0;               // Frame delay (milliseconds)
	protected bool m_HasStarted = false;          // Ready to output frames
	protected FileStream m_FileStream;

	protected Frame m_CurrentFrame;
	protected byte[] m_Pixels;                    // RGB byte array from frame
	protected byte[] m_PixelsAlpha;               // Alpha byte array from frame
	protected byte[] m_IndexedPixels;             // Converted frame indexed to palette
	protected int m_ColorDepth;                   // Number of bit planes
	protected byte[] m_ColorTab;                  // RGB palette
	protected bool[] m_UsedEntry = new bool[256]; // Active palette entries
	protected int m_PaletteSize = 7;              // Color table size (bits-1)
	protected int m_DisposalCode = -1;            // Disposal code (-1 = use default)
	protected bool m_ShouldCloseStream = false;   // Close stream when finished
	protected bool m_IsFirstFrame = true;
	protected bool m_IsSizeSet = false;           // If false, get size from first frame
	protected int m_SampleInterval = 10;          // Default sample interval for quantizer

	/// <summary> Set equal to one of the index in the color table to hide that color. </summary>
	protected int m_TransparentColorIndex = 0;

	/// <summary> The flag indicating if transparency is enabled/disabled. 0(disable) or 1(enable). </summary>
	protected int m_TransparentFlag = 0;

	/// <summary> The transparent color to hide in the GIF. </summary>
	protected Color32 m_TransparentColor;

	/// <summary> If 'TRUE', check if any pixels' alpha value equal zero for supporting GIF transparent. </summary>
	public bool m_AutoTransparent = false;
	protected bool m_HasTransparentPixel = false;

	/// <summary>
	/// Default constructor. Repeat will be set to -1 and Quality to 10.
	/// </summary>
	public ProGifEncoder() : this(-1, 10) {}

	/// <summary>
	/// Constructor with the number of times the set of GIF frames should be played.
	/// </summary>
	/// <param name="repeat">Default is -1 (no repeat); 0 means play indefinitely</param>
	/// <param name="quality">Sets quality of color quantization (conversion of images to
	/// the maximum 256 colors allowed by the GIF specification). Lower values (minimum = 1)
	/// produce better colors, but slow processing significantly. Higher values will speed
	/// up the quantization pass at the cost of lower image quality (maximum = 100).</param>
	public ProGifEncoder(int repeat, int quality)
	{
		if (repeat >= 0)
			m_Repeat = repeat;

		m_SampleInterval = (int)Mathf.Clamp(quality, 1, 100);
	}

	/// <summary>
	/// Sets the delay time between each frame, or changes it for subsequent frames (applies
	/// to last frame added).
	/// </summary>
	/// <param name="ms">Delay time in milliseconds</param>
	public void SetDelay(int ms)
	{
		m_FrameDelay = Mathf.RoundToInt(ms / 10f);
	}

	/// <summary>
	/// Sets frame rate in frames per second. Equivalent to <code>SetDelay(1000/fps)</code>.
	/// </summary>
	/// <param name="fps">Frame rate</param>
	public void SetFrameRate(float fps)
	{
		if (fps > 0f)
			m_FrameDelay = Mathf.RoundToInt(100f / fps);
	}

	/// <summary>
	/// Adds next GIF frame. The frame is not written immediately, but is actually deferred
	/// until the next frame is received so that timing data can be inserted. Invoking
	/// <code>Finish()</code> flushes all frames.
	/// </summary>
	/// <param name="frame">GifFrame containing frame to write.</param>
	public void AddFrame(Frame frame)
	{
		if ((frame == null))
			throw new ArgumentNullException("Can't add a null frame to the gif.");

		if (!m_HasStarted)
			throw new InvalidOperationException("Call Start() before adding frames to the gif.");

		// Use first frame's size
		if (!m_IsSizeSet) SetSize(frame.Width, frame.Height);

//		#if UNITY_EDITOR
//		SetTransparencyColor(new Color32(49, 77, 121, 255)); //Hard code test
//		#endif

		m_CurrentFrame = frame;
		GetImagePixels();
		AnalyzePixels();

		if (m_IsFirstFrame)
		{
			WriteLSD();
			WritePalette();

			if (m_Repeat >= 0)
				WriteNetscapeExt();
		}

		WriteGraphicCtrlExt();
		WriteImageDesc();

		if (!m_IsFirstFrame)
			WritePalette();

		WritePixels();
		m_IsFirstFrame = false;
	}

	/// <summary>
	/// Initiates GIF file creation on the given stream. The stream is not closed automatically.
	/// </summary>
	/// <param name="os">OutputStream on which GIF images are written</param>
	public void Start(FileStream os)
	{
		if (os == null) 
			throw new ArgumentNullException("Stream is null.");

		m_ShouldCloseStream = false;
		m_FileStream = os;

		try
		{
			WriteString("GIF89a"); // header
		}
		catch (IOException e)
		{
			throw e;
		}

		m_HasStarted = true;
	}

	/// <summary>
	/// Initiates writing of a GIF file with the specified name. The stream will be handled for you.
	/// </summary>
	/// <param name="file">String containing output file name</param>
	public void Start(String file)
	{
		try
		{
			m_FileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			Start(m_FileStream);
			m_ShouldCloseStream = true;
		}
		catch (IOException e)
		{
			throw e;
		}
	}

	/// <summary>
	/// Flushes any pending data and closes output file.
	/// If writing to an OutputStream, the stream is not closed.
	/// </summary>
	public void Finish()
	{
		if (!m_HasStarted)
			throw new InvalidOperationException("Can't finish a non-started gif.");

		m_HasStarted = false;

		try
		{
			m_FileStream.WriteByte(0x3b); // Gif trailer
			m_FileStream.Flush();

			if (m_ShouldCloseStream)
				m_FileStream.Close();
		}
		catch (IOException e)
		{
			throw e;
		}

		// Reset for subsequent use
		m_FileStream = null;
		m_CurrentFrame = null;
		m_Pixels = null;
		m_PixelsAlpha = null;
		m_IndexedPixels = null;
		m_ColorTab = null;
		m_ShouldCloseStream = false;
		m_IsFirstFrame = true;
	}

	// Sets the GIF frame size.
	protected void SetSize(int w, int h)
	{
		m_Width = w;
		m_Height = h;
		m_IsSizeSet = true;
	}

	// Extracts image pixels into byte array "pixels".
	protected void GetImagePixels()
	{
		m_Pixels = new Byte[3 * m_CurrentFrame.Width * m_CurrentFrame.Height];
//		m_PixelsAlpha = new Byte[m_CurrentFrame.Width * m_CurrentFrame.Height];
		Color32[] p = m_CurrentFrame.Data;
		int count = 0;
		int count_a = 0;

		if(m_AutoTransparent)
		{
			m_PixelsAlpha = new Byte[m_CurrentFrame.Width * m_CurrentFrame.Height];

			// Texture data is layered down-top, so flip it
			for (int th = m_CurrentFrame.Height - 1; th >= 0; th--)
			{
				for (int tw = 0; tw < m_CurrentFrame.Width; tw++)
				{
					Color32 color = p[th * m_CurrentFrame.Width + tw];
					m_Pixels[count] = color.r; count++;
					m_Pixels[count] = color.g; count++;
					m_Pixels[count] = color.b; count++;
					m_PixelsAlpha[count_a] = color.a; count_a++;
					if(!m_HasTransparentPixel)
					{
						if(color.a == 0) 
						{
							m_HasTransparentPixel = true;
							m_TransparentFlag = 1;
						}
					}
				}
			}
		}
		else
		{
			// Texture data is layered down-top, so flip it
			for (int th = m_CurrentFrame.Height - 1; th >= 0; th--)
			{
				for (int tw = 0; tw < m_CurrentFrame.Width; tw++)
				{
					Color32 color = p[th * m_CurrentFrame.Width + tw];
					m_Pixels[count] = color.r; count++;
					m_Pixels[count] = color.g; count++;
					m_Pixels[count] = color.b; count++;
				}
			}
		}

	}

	// Analyzes image colors and creates color map.
	protected void AnalyzePixels()
	{
		int len = m_Pixels.Length;
		int nPix = len / 3;
		m_IndexedPixels = new byte[nPix];
		ProGifNeuQuant nq = new ProGifNeuQuant(m_Pixels, len, (int)m_SampleInterval, (m_AutoTransparent && m_HasTransparentPixel)? true:false);
		m_ColorTab = nq.Process(); // Create reduced palette

		//N-0074 Transparency Color
		SetTransparencyIndexAndFlag(m_TransparentColor);

		// Map image pixels to new palette
		int k = 0;
		for (int i = 0; i < nPix; i++)
		{
			int index = nq.Map(m_Pixels[k++] & 0xff, m_Pixels[k++] & 0xff, m_Pixels[k++] & 0xff);

			//N-0074 - Find m_TransparentColor in the Picture(m_Pixels), set the same Index as TransparencyColorIndex
			if(m_TransparentFlag == 1)
			{
				if(m_AutoTransparent && m_PixelsAlpha[i] == 0)
				{
					index = m_TransparentColorIndex;
				}
				else if(!m_AutoTransparent && m_Pixels[k-3] == m_TransparentColor.r && m_Pixels[k-2] == m_TransparentColor.g && m_Pixels[k-1] == m_TransparentColor.b)
				{
					index = m_TransparentColorIndex;
				}
			}

			m_UsedEntry[index] = true;
			m_IndexedPixels[i] = (byte)index;
		}

		m_Pixels = null;
		m_PixelsAlpha = null;
		m_ColorDepth = 8;
		m_PaletteSize = 7;
	}

	//N-0074-----------------------------------
	public void SetTransparencyColor(Color32 c32)
	{
		m_TransparentColor = c32;
		m_TransparentFlag = 1;
		m_AutoTransparent = false;
	}

	protected void SetTransparencyIndexAndFlag(Color32 c32)
	{
		if(m_TransparentFlag != 1) return;

		int index = -1;

		int diffR = 255, diffG = 255, diffB = 255;
		int diff1 = 255, diff2 = 255, diff3 = 255;

		for(int i=0; i<m_ColorTab.Length; i+=3)
		{
			int smallerCounter = 0;
			diff1 = Mathf.Abs(c32.r - m_ColorTab[i]);
			if(diff1 <= diffR)
			{
				smallerCounter++;
			}

			diff2 = Mathf.Abs(c32.g - m_ColorTab[i+1]);
			if(diff2 <= diffG)
			{
				smallerCounter++;
			}

			diff3 = Mathf.Abs(c32.b - m_ColorTab[i+2]);
			if(diff3 <= diffB)
			{
				smallerCounter++;
			}

			if(smallerCounter >= 2 && diff1 + diff2 + diff3 <= diffR + diffG + diffB)
			{
				index = i;
				diffR = diff1;
				diffG = diff2;
				diffB = diff3;
			}
		}

		// Check color bias not greater than 4
		if(index != -1)
		{
			if(Mathf.Abs(c32.r - m_ColorTab[index]) > 4 || Mathf.Abs(c32.g - m_ColorTab[index+1]) > 4 || Mathf.Abs(c32.b - m_ColorTab[index+2]) > 4)
			{
				index = -1; //The input color not found
			}
		}

		m_TransparentColorIndex = (index == -1) ? -1 : index/3;

		if(m_TransparentColorIndex == -1 && m_AutoTransparent && m_HasTransparentPixel)
		{
			m_TransparentColorIndex = 255; // The 256th index of ColorTable reserved for auto detected transparent pixels
		}

		m_TransparentFlag = (m_TransparentColorIndex == -1) ? 0 : 1;

//		#if UNITY_EDITOR
//		Debug.Log("m_ColorTab: " + m_ColorTab.Length + " | index: " + index 
//			+ " |m_TransparentFlag: " + m_TransparentFlag + " | m_TransparentColorIndex: " + m_TransparentColorIndex 
//			+ " | m_AutoTransparency: " + m_AutoTransparency + " | m_HasTransparentPixel: " + m_HasTransparentPixel
//			+ " | diff 1 2 3: " + diff1 + "|" + diff2 + "|" + diff3 + " | diff R G B: " + diffR + "|" + diffG + "|" + diffB);
//
//		//new FilePathName().WriteBytesToText(m_ColorTab, "/SwanDev/UnityAssets/ProGIF/Assets/m_ColorTableTans.txt", "-", false);
//		#endif
	}
	//N-0074-----------------------------------

	// Writes Graphic Control Extension.
	protected void WriteGraphicCtrlExt()
	{
		m_FileStream.WriteByte(0x21); // Extension introducer
		m_FileStream.WriteByte(0xf9); // GCE label
		m_FileStream.WriteByte(4);    // Data block size
		
		// Packed fields (8bits) - v1.7.0
		m_FileStream.WriteByte(Convert.ToByte(0 |	// 1-3 bit: reserved
			((m_TransparentFlag == 1)? 8:0) |     	// 4-6 bit: disposal (8 = 0b00001000) 
			0 |     								// 7   bit: user input - 0 = none
			m_TransparentFlag));    				// 8   bit: transparency flag 

		// Packed fields (8bits) - old
//		m_FileStream.WriteByte(Convert.ToByte(0 |	// 1-3 bit: reserved
//			0 |     								// 4-6 bit: disposal
//			0 |     								// 7   bit: user input - 0 = none
//			0));    								// 8   bit: transparency flag

		WriteShort(m_FrameDelay); // Delay x 1/100 sec

		//B-0073
		//m_FileStream.WriteByte(0); // Transparent color index
		m_FileStream.WriteByte(Convert.ToByte((m_TransparentColorIndex != -1) ? m_TransparentColorIndex : 0)); // Transparent color index (New)

		m_FileStream.WriteByte(0); // Block terminator
	}

	// Writes Image Descriptor.
	protected void WriteImageDesc()
	{
		m_FileStream.WriteByte(0x2c); // Image separator
		WriteShort(0);                // Image position x,y = 0,0
		WriteShort(0);
		WriteShort(m_Width);          // image size
		WriteShort(m_Height);

		// Packed fields
		if (m_IsFirstFrame)
		{
			m_FileStream.WriteByte(0); // No LCT  - GCT is used for first (or only) frame
		}
		else
		{
			// Specify normal LCT
			m_FileStream.WriteByte(Convert.ToByte(0x80 |           // 1 local color table  1=yes
				0 |              // 2 interlace - 0=no
				0 |              // 3 sorted - 0=no
				0 |              // 4-5 reserved
				m_PaletteSize)); // 6-8 size of color table
		}
	}

	// Writes Logical Screen Descriptor.
	protected void WriteLSD()
	{
		// Logical screen size
		WriteShort(m_Width);
		WriteShort(m_Height);

		// Packed fields
		m_FileStream.WriteByte(Convert.ToByte(0x80 |           // 1   : global color table flag = 1 (gct used)
			0x70 |           // 2-4 : color resolution = 7
			0x00 |           // 5   : gct sort flag = 0
			m_PaletteSize)); // 6-8 : gct size

		m_FileStream.WriteByte(0); // Background color index

		m_FileStream.WriteByte(0); // Pixel aspect ratio - assume 1:1
	}

	// Writes Netscape application extension to define repeat count.
	protected void WriteNetscapeExt()
	{
		m_FileStream.WriteByte(0x21);    // Extension introducer
		m_FileStream.WriteByte(0xff);    // App extension label
		m_FileStream.WriteByte(11);      // Block size
		WriteString("NETSCAPE" + "2.0"); // App id + auth code
		m_FileStream.WriteByte(3);       // Sub-block size
		m_FileStream.WriteByte(1);       // Loop sub-block id
		WriteShort(m_Repeat);            // Loop count (extra iterations, 0=repeat forever)
		m_FileStream.WriteByte(0);       // Block terminator
	}

	// Write color table.
	protected void WritePalette()
	{
		m_FileStream.Write(m_ColorTab, 0, m_ColorTab.Length);
		int n = (3 * 256) - m_ColorTab.Length;

		for (int i = 0; i < n; i++)
			m_FileStream.WriteByte(0);
	}

	// Encodes and writes pixel data.
	protected void WritePixels()
	{
		ProGifLzwEncoder encoder = new ProGifLzwEncoder(m_Width, m_Height, m_IndexedPixels, m_ColorDepth);
		encoder.Encode(m_FileStream);
	}

	// Write 16-bit value to output stream, LSB first.
	protected void WriteShort(int value)
	{
		m_FileStream.WriteByte(Convert.ToByte(value & 0xff));
		m_FileStream.WriteByte(Convert.ToByte((value >> 8) & 0xff));
	}

	// Writes string to output stream.
	protected void WriteString(String s)
	{
		char[] chars = s.ToCharArray();

		for (int i = 0; i < chars.Length; i++)
			m_FileStream.WriteByte((byte)chars[i]);
	}
}


internal class Frame
{
	public int Width;
	public int Height;
	public Color32[] Data;

	//TODO: variable frame rate
	//public float DelaySec;
}