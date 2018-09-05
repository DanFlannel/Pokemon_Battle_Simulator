#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

/// <summary>
/// On editor GIF recorder: record GIF of your app's development screens.
/// </summary>
public class OnEditorGifRecorder : MonoBehaviour
{
	[Header("[ HOW ] Attach this script on a GameObject in the scene,")]
	[Header("modify settings and start record GIF in the Editor.")]
	[Space()]
	[Header("[ Recorder Settings ]")]
	public Vector2 m_AspectRatio = new Vector2(0, 0);
	public bool m_AutoAspect = true;
	public int m_Width = 360;
	public int m_Height = 360;
	public float m_Duration = 3f;
	[Range(1, 60)] public int m_Fps = 15;
	public int m_Loop = 0;								//-1: no repeat, 0: infinite, >0: repeat count
	[Range(1, 100)] public int m_Quality = 20;			//(1 - 100), 1: best(larger storage size), 100: faster(smaller storage size)

	[Space()]
	[Header("[ Camera Settings ]")]
	public Camera m_RecorderCamera;
	public Camera[] m_AllCameras;
	private int _currCameraIndex = 0;
	private const string _recorderName = "OnEditorGifRecorder";

	[HideInInspector] public string m_RecordingProgress = "0%";
	[HideInInspector] public string m_SaveProgress = "0%";
	[HideInInspector] public string m_State = "Idle";
	[HideInInspector] [TextArea(1, 2)] public string m_SavePath = "GIF Path";


	public void FindCameras(OnEditorGifRecorderCustomEditor editorScript)
	{
		m_AllCameras = Camera.allCameras;
		editorScript.SetCameraOptions(m_AllCameras);

		if(m_AllCameras != null && m_AllCameras.Length > 0 && m_RecorderCamera == null)
		{
			m_RecorderCamera = m_AllCameras[0];
		}
	}

	public void SetCamera(int index)
	{
		if(_currCameraIndex == index) return;
		_currCameraIndex = index;
		if(index < m_AllCameras.Length) m_RecorderCamera = m_AllCameras[index];
	}

	public void StartRecord()
	{
		if(!Application.isPlaying || !Application.isEditor)
		{
			Debug.LogWarning("This script is designed to work in the Editor Mode with Editor Playing.");
			return;
		}

		Debug.Log("Start Record");
		m_State = "Recording..";
		PGif.iSetRecordSettings(m_AutoAspect, m_Width, m_Height, m_Duration, m_Fps, m_Loop, m_Quality);
		PGif.iStartRecord(((m_RecorderCamera == null)?Camera.main:m_RecorderCamera), _recorderName, 
			(progress)=>{
				m_RecordingProgress = Mathf.CeilToInt(progress*100) + "%";
			}, 
			()=>{
				m_State = "Press the <Save Record> button to save GIF";
			},
			null,
			(id, progress)=>{
				m_SaveProgress = Mathf.CeilToInt(progress*100) + "%";
			},
			(id, path)=>{
				m_SavePath = path;
				m_RecordingProgress = "0%";
				m_SaveProgress = "0%";
				m_State = "Idle";
			}
		);
	}

	public void SaveRecord()
	{
		if(!Application.isPlaying || !Application.isEditor)
		{
			Debug.LogWarning("This script is designed to work in the Editor Mode with Editor Playing.");
			return;
		}

		Debug.Log("Save Record");
		m_State = "Saving..";
		PGif.iStopAndSaveRecord(_recorderName);
	}

}


[CustomEditor(typeof(OnEditorGifRecorder))]
public class OnEditorGifRecorderCustomEditor : Editor
{
	private static string[] cameraOptions = new string[]{}; 
	private static int cameraSelection = 0;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		OnEditorGifRecorder recorder = (OnEditorGifRecorder)target;

		cameraSelection = GUILayout.SelectionGrid(cameraSelection, cameraOptions, 2);
		recorder.SetCamera(cameraSelection);

		GUILayout.Label("Find all Cameras in the scene:\n(Drag the camera you want to Recorder Camera)");
		if(GUILayout.Button("Find Cameras"))
		{
			recorder.FindCameras(this);
		}


		GUILayout.Label("\n\n[ Start Record GIF ]\nStart record GIF with Recorder Camera, or main camera:");
		if(GUILayout.Button("Start Record"))
		{
			recorder.StartRecord();
		}

		GUILayout.Label("Stop and save the stored frames as GIF:");
		if(GUILayout.Button("Save Record"))
		{
			recorder.SaveRecord();
		}

		GUILayout.Label("\nRecord Progress: " + recorder.m_RecordingProgress
			+ "\nSave Progress: " + recorder.m_SaveProgress
			+ "\nStatus: " + recorder.m_State
			+ "\n\nGIF Path: " + recorder.m_SavePath + "\n");

		if(GUILayout.Button("View GIF"))
		{
			if(string.IsNullOrEmpty(recorder.m_SavePath)) return;
			Application.OpenURL(new FilePathName().EnsureLocalPath(recorder.m_SavePath));
		}

		if(GUILayout.Button("Reveal In Folder"))
		{
			if(string.IsNullOrEmpty(recorder.m_SavePath)) return;
			string fileName = Path.GetFileName(recorder.m_SavePath);
			string directoryPath = recorder.m_SavePath.Remove(recorder.m_SavePath.IndexOf(fileName));
			Application.OpenURL(new FilePathName().EnsureLocalPath(directoryPath));
		}

		if(GUILayout.Button("Copy GIF Path"))
		{
			if(string.IsNullOrEmpty(recorder.m_SavePath)) return;

			TextEditor te = null;
			te = new TextEditor();
			te.text = recorder.m_SavePath;
			te.SelectAll();
			te.Copy();
		}

	}

	public void SetCameraOptions(Camera[] cameras)
	{
		cameraSelection = 0;
		cameraOptions = new string[cameras.Length];
		for(int i=0; i<cameras.Length; i++)
		{
			cameraOptions[i] = cameras[i].name;
		}
	}
}
#endif