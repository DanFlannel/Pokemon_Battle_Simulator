/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ProGifControlPanel : MonoBehaviour
{
	private string PP_GIFDurationKey = "ProGIF_Duration";
	private string PP_GIFFpsKey = "ProGIF_FPS";
	private string PP_GIFAspectRatioOptionKey = "ProGIF_AspectRatioOption";
	private string PP_GIFRotationOptionKey = "ProGIF_RotationOption";

	public GameObject containerGO;
	public Text text_Title;
	public Slider slider_Duration;
	public Slider slider_FPS;
	public Text text_Duration;
	public Text text_FPS;
	public Dropdown dropdown_AspectRatio;
	public Dropdown dropdown_Rotation;

	public Action _OnStartRecord = null;
	public Action<float> _OnRecordProgress = null;
	public Action _OnRecordDurationMax = null;

	/// <summary>
	/// Create an instance of ProGifControlPanel from provided prefab, and set parent.
	/// </summary>
	/// <param name="prefab">The Prefab of ProGifControlPanel.</param>
	/// <param name="parentT">The container/parent for this instance.</param>
	public static ProGifControlPanel Create(GameObject prefab, Transform parentT)
	{
		ProGifControlPanel gifPanel = ProGifManager.InstantiatePrefab<ProGifControlPanel>(prefab);
		if(gifPanel == null) return null;
		gifPanel.transform.SetParent(parentT);
		gifPanel.transform.rotation = parentT.rotation;
		gifPanel.transform.localScale = Vector3.one;
		gifPanel.transform.localPosition = Vector3.zero;
		gifPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		gifPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		return gifPanel;
	}

	// Use this for initialization
	public void Setup(Action onStartRecord = null, Action<float> onRecordProgress = null, Action onRecordDurationMax = null)
	{
		_OnStartRecord = onStartRecord;
		_OnRecordProgress = onRecordProgress;
		_OnRecordDurationMax = onRecordDurationMax;

		int gifDuration = PlayerPrefs.GetInt(PP_GIFDurationKey, 3);
		int gifFps = PlayerPrefs.GetInt(PP_GIFFpsKey, 15);
		int gifAspectRatioOption = PlayerPrefs.GetInt(PP_GIFAspectRatioOptionKey, 0);
		int gifRotationOption = PlayerPrefs.GetInt(PP_GIFRotationOptionKey, 0);

		slider_Duration.value = gifDuration;
		slider_FPS.value = gifFps;
		_SetDurationText(gifDuration);
		text_Title.text = "GIF Setting";
		dropdown_AspectRatio.value = gifAspectRatioOption;
		dropdown_Rotation.value = gifRotationOption;

		_Show();
	}

	public void OnDropdownAspectRatioChange(Dropdown dropdown)
	{
		
		//dropdown.value;
	}

	private void _SetDurationText(int duration)
	{
		text_Duration.text = "Duration: " + duration + "s";
	}

	private void _SetFpsText(int fps)
	{
		text_FPS.text = "FPS: " + fps;
	}

	public void OnSliderDurationChange(Slider slider)
	{
		_SetDurationText((int)slider.value);
	}

	public void OnSliderFpsChange(Slider slider)
	{
		_SetFpsText((int)slider.value);
	}

	public void OnButtonRecordClicked()
	{
		//Close the panel first, and then start recording
		Close(()=>{
			//Make some changes to the record setting before StartRecord
			if(dropdown_AspectRatio.value != 0)
			{
				ProGifManager.Instance.SetRecordSettings(_GetAspectRatio(dropdown_AspectRatio.value), 
					360, 360, slider_Duration.value, (int)slider_FPS.value, 0, 25);
			}
			else
			{
				ProGifManager.Instance.SetRecordSettings(true, 360, 360, slider_Duration.value, (int)slider_FPS.value, 0, 25);
			}

			//Start record
			ProGifManager.Instance.StartRecord(Camera.main, _OnRecordProgress, _OnRecordDurationMax);

			//Set rotation after StartRecord & before Save gif
			ProGifManager.Instance.SetGifRotation(_GetRotation(dropdown_Rotation.value));

			//Debug.Log("dropdown_Rotation.value: " + dropdown_Rotation.value + " - " + _GetRotation(dropdown_Rotation.value));

			if(_OnStartRecord != null)
			{
				_OnStartRecord();
			}
		});
	}

	//Gets the related aspect ratio with the aspect ratio option, 
	//add your own and modify the prefab(ProGif_Panel_Prefab) as need
	private Vector2 _GetAspectRatio(int option)
	{
		Vector2 aspect = Vector2.zero;
		switch(option)
		{
		case 0:
			aspect = Vector2.zero;
			break;
		case 1:
			aspect = new Vector2(9,16);
			break;
		case 2:
			aspect = new Vector2(2,3);
			break;
		case 3:
			aspect = new Vector2(3,4);
			break;
		case 4:
			aspect = new Vector2(1,1);
			break;
		}
		return aspect;
	}

	private ImageRotator.Rotation _GetRotation(int option)
	{
		ImageRotator.Rotation rotation = ImageRotator.Rotation.None;
		switch(option)
		{
		case 1:
			rotation = ImageRotator.Rotation.Left;
			break;
		case 2:
			rotation = ImageRotator.Rotation.Right;
			break;
		case 3:
			rotation = ImageRotator.Rotation.HalfCircle;
			break;
		}
		return rotation;
	}

	private void _Show()
	{
		gameObject.SetActive(true);

		//Show animation
		SDemoAnimation.Instance.Scale(containerGO, Vector3.zero, Vector3.one, 0.5f);
		SDemoAnimation.Instance.Move(containerGO, new Vector3(0, -1920f, 0), Vector3.zero, 0.5f);
		SDemoAnimation.Instance.Rotate(containerGO, new Vector3(0, 0, 900f), Vector3.zero, 0.5f);
	}

	public void OnCloseButtonClicked()
	{
		Close(null);
	}

	public void Close(Action onClosed)
	{
		//Save settings to PP before close
		if(PlayerPrefs.GetInt(PP_GIFDurationKey, 0) != (int)slider_Duration.value)
		{
			PlayerPrefs.SetInt(PP_GIFDurationKey, (int)slider_Duration.value);
		}
		if(PlayerPrefs.GetInt(PP_GIFFpsKey, 0) != (int)slider_FPS.value)
		{
			PlayerPrefs.SetInt(PP_GIFFpsKey, (int)slider_FPS.value);
		}
		if(PlayerPrefs.GetInt(PP_GIFAspectRatioOptionKey, 0) != dropdown_AspectRatio.value)
		{
			PlayerPrefs.SetInt(PP_GIFAspectRatioOptionKey, dropdown_AspectRatio.value);
		}
		if(PlayerPrefs.GetInt(PP_GIFRotationOptionKey, 0) != dropdown_Rotation.value)
		{
			PlayerPrefs.SetInt(PP_GIFRotationOptionKey, dropdown_Rotation.value);
		}

		_Close(onClosed);
	}

	private void _Close(Action onClosed)
	{
		//Animation to hide and remove panel
		SDemoAnimation.Instance.Scale(containerGO, Vector3.one, Vector3.zero, 0.3f, SDemoAnimation.LoopType.None, ()=>{
			if(onClosed != null) onClosed();
			Destroy(gameObject);
		});
	}

}
