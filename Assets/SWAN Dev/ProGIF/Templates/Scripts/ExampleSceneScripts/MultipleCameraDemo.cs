using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleCameraDemo : MonoBehaviour
{
	public Camera mCamera1;
	public Camera mCamera2;
	public Camera mCamera3;

	public Text cam1Text;
	public Text cam2Text;
	public Text cam3Text;

	public  UnityEngine.UI.Image image1;
	public  UnityEngine.UI.Image image2;
	public  UnityEngine.UI.Image image3;

    public RawImage rawImage1;

	public GUITexture m_GuiTexture;
	private Texture2D _refTexture2d, _refImageTexture2d;

	// Use this for initialization
	void Start()
	{
		//Change setting before camera1 start recording
		PGif.iSetRecordSettings(true, 300, 300, 3, 24, 0, 50);
		//Start recording with camera1
		PGif.iStartRecord(mCamera1, "Cam1", OnRecordProgress1, OnRecordDurationMax1, OnPreProcessingDone1, OnFileSaveProgress1, OnFileSaved1, autoClear:false);
		cam1Text.text = "Camera1 started recording";

		//Change setting before camera2 start recording
		PGif.iSetRecordSettings(new Vector2(1, 1), 300, 300, 5, 10, 1, 50);
		//Start recording with camera2
		PGif.iStartRecord(mCamera2, "Cam2", OnRecordProgress2, OnRecordDurationMax2, OnPreProcessingDone2, OnFileSaveProgress2, OnFileSaved2, autoClear:true);
		cam2Text.text = "Camera2 started recording";

		//Change setting before camera3 start recording
		PGif.iSetRecordSettings(new Vector2(4, 3), 200, 200, 7, 10, 1, 50);
		//Start recording with camera3
		PGif.iStartRecord(mCamera3, "Cam3", OnRecordProgress3, OnRecordDurationMax3, OnPreProcessingDone3, OnFileSaveProgress3, OnFileSaved3, autoClear:true);
		cam3Text.text = "Camera3 started recording";
	}


	public void OnRecordProgress1(float progress)
	{
		//Debug.Log("Cam1 - [MultipleCameraDemo] On record progress: " + progress);
	}

	public void OnRecordDurationMax1()
	{
		Debug.Log("Cam1 - [MultipleCameraDemo] On recorder buffer max.");
		cam1Text.text = "Camera1 duration Max";
	}

	public void OnPreProcessingDone1()
	{
		Debug.Log("Cam1 - [MultipleCameraDemo] On pre-processing done.");
		cam1Text.text = "Camera1 pre-processing done";

        //If the Stop/Clear method has not been called before, 
        //you can resume the recorder to continue the recording after PreProcessing done
        //(this is the earliest timing for resuming the same recorder after SaveRecord is called).
        PGif.iResumeRecord("Cam1");
    }

	public void OnFileSaveProgress1(int id, float progress)
	{
		//Debug.Log("Cam1 - [MultipleCameraDemo] Save progress: " + progress);
		cam1Text.text = "Camera1 Save progress: " + progress;
    }

	public void OnFileSaved1(int id, string path)
	{
		Debug.Log("Cam1 - [MultipleCameraDemo] On saved, path: " + path);
		cam1Text.text = "Camera1 Saved: " + path;

        //(MobileMedia Plugin) Save the gif to native gallery (Android/iOS)
        //MobileMedia.CopyMedia(path, "MobileMedia", "FileName", ".gif", true);

        //(Preview) ---------
        //Preview the saved gif or play it? Do not clear the recorder if you want to preview the gif, 
        //you can clear the recorder after you quit from the preview
        // ---> PGif.iClearRecorder("Cam1"); <---

        // Preview with Image
        //_PlayGif("Cam1", "GifPlayer1", image1);

        // Preview with RawImage
        PGif.iPlayGif(PGif.iGetRecorder("Cam1"), rawImage1, "GifPlayer1", (progress) => {
            //Set display size
            rawImage1.SetNativeSize();
        });

		PGif.iGetPlayer("GifPlayer1").SetOnPlayingCallback((gTex)=>{
			gTex.SetDisplay(image1, ref _refImageTexture2d);
		});
        //(Preview) ---------
    }


    public void OnRecordProgress2(float progress)
	{
		//Debug.Log("Cam2 - [MultipleCameraDemo] On record progress: " + progress);
	}

	public void OnRecordDurationMax2()
	{
		Debug.Log("Cam2 - [MultipleCameraDemo] On recorder buffer max.");
		cam2Text.text = "Camera2 duration Max";
	}

	public void OnPreProcessingDone2()
	{
		Debug.Log("Cam2 - [MultipleCameraDemo] On pre-processing done.");
		cam2Text.text = "Camera2 pre-processing done";
	}

	public void OnFileSaveProgress2(int id, float progress)
	{
		//Debug.Log("Cam2 - [MultipleCameraDemo] Save progress: " + progress);
		cam2Text.text = "Camera2 Save progress: " + progress;
	}

	public void OnFileSaved2(int id, string path)
	{
		Debug.Log("Cam2 - [MultipleCameraDemo] On saved, path: " + path);
		cam2Text.text = "Camera2 Saved: " + path;

		_PlayGif("Cam2", "GifPlayer2", image2);
	}


	public void OnRecordProgress3(float progress)
	{
		//Debug.Log("Cam3 - [MultipleCameraDemo] On record progress: " + progress);
	}

	public void OnRecordDurationMax3()
	{
		Debug.Log("Cam3 - [MultipleCameraDemo] On recorder buffer max.");
		cam3Text.text = "Camera3 duration Max";
	}

	public void OnPreProcessingDone3()
	{
		Debug.Log("Cam3 - [MultipleCameraDemo] On pre-processing done.");
		cam3Text.text = "Camera3 pre-processing done";
	}

	public void OnFileSaveProgress3(int id, float progress)
	{
		//Debug.Log("Cam3 - [MultipleCameraDemo] Save progress: " + progress);
		cam3Text.text = "Camera3 Save progress: " + progress;
	}

	public void OnFileSaved3(int id, string path)
	{
		Debug.Log("Cam3 - [MultipleCameraDemo] On saved, path: " + path);
		cam3Text.text = "Camera3 Saved: " + path;

		_PlayGif("Cam3", "GifPlayer3", image3);

		//Make use of OnPlaying callback to display gif on extra target object that supports Texture2D/Sprite.
		PGif.iGetPlayer("GifPlayer3").SetOnPlayingCallback((getGTex)=>{
			//m_GuiTexture.texture = getGTex.GetTexture2D();

			getGTex.SetDisplay(m_GuiTexture, ref _refTexture2d);
		});
	}

	private void _PlayGif(string recorderName, string playerName,  UnityEngine.UI.Image destination)
	{
		PGif.iPlayGif(PGif.iGetRecorder(recorderName), destination, playerName, (progress)=>{
			//Set display size
			float gifRatio = (float)PGif.iGetRecorder(recorderName).Width/(float)PGif.iGetRecorder(recorderName).Height;
			_SetDisplaySize(gifRatio, destination);
		});
	}

	private void _SetDisplaySize(float gifWHRatio, UnityEngine.UI.Image destination)
	{
		int maxDisplayWidth = (int)destination.rectTransform.sizeDelta.x;
		int maxDisplayHeight = (int)destination.rectTransform.sizeDelta.y;

		int displayWidth = maxDisplayWidth;
		int displayHeight = maxDisplayHeight;
		if(gifWHRatio > 1f)
		{
			displayWidth = maxDisplayWidth;
			displayHeight = (int)((float)displayWidth / gifWHRatio);
		}
		else if(gifWHRatio < 1f)
		{
			displayHeight = maxDisplayHeight;
			displayWidth = (int)((float)displayHeight * gifWHRatio);
		}
		destination.rectTransform.sizeDelta = new Vector2(displayWidth, displayHeight);
	}

	#region ---- UI Control ----
	public void SaveRecord_Cam1()
	{
        //Save all stored frames to gif (the recorder will be Paused, you can resume it after PreProcessing done)
        PGif.iSaveRecord("Cam1");
	}

	public void SaveRecord_Cam2()
	{
        //Stop the recorder and Save all stored frames to gif. (You can't resume the recorder. As the Stop method is called, )
        PGif.iStopAndSaveRecord("Cam2");
	}

	public void SaveRecord_Cam3()
	{
		PGif.iStopAndSaveRecord("Cam3");
	}

	int _counter = 0;
	public void UpdateCubeText(TextMesh tm)
	{
		_counter++;
		if(_counter > 9) _counter = 0;
		tm.text = _counter.ToString();
	}
	#endregion


}
