using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.VR.WSA.WebCam;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System;

public class Manager : MonoBehaviour
{

	public static Manager Instance { get; private set; }

	GestureRecognizer recognizer;

	public GameObject neutral;
	public GameObject happy;
	public GameObject sad;

	public string FaceAPIKey;
	public string EmotionAPIKey;

	private Resolution cameraResolution;
	private PhotoCapture photoCaptureObject = null;

	private string currentEmotion = "neutral";
	private double nextTime = 0.0;

	void OnPhotoCaptureCreated(PhotoCapture captureObject)
	{
		photoCaptureObject = captureObject;

		cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

		CameraParameters c = new CameraParameters();
		c.hologramOpacity = 0.0f;
		c.cameraResolutionWidth = cameraResolution.width;
		c.cameraResolutionHeight = cameraResolution.height;
		c.pixelFormat = CapturePixelFormat.PNG;

		captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
	}

	private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
	{
		if (result.success)
		{
			Debug.Log("Camera ready");
			photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
		}
		else
		{
			Debug.LogError("Unable to start photo mode!");
		}
	}

	IEnumerator<object> PostToFaceAPI(byte[] imageData) {
		
		var url = "https://westus.api.cognitive.microsoft.com/face/v1.0/detect";
		var headers = new Dictionary<string, string>() {
			{ "Ocp-Apim-Subscription-Key", FaceAPIKey },
			{ "Content-Type", "application/octet-stream" }
		};

		WWW www = new WWW(url, imageData, headers);
		yield return www;
		string responseString = www.text;

		JSONObject j = new JSONObject(responseString);
		Debug.Log(j);

		if (j.GetField ("error")) {
			Debug.Log ("Error");
			yield break;
		}

		var faceRectangles = "";
		foreach (var result in j.list) {
			var p = result.GetField("faceRectangle");

			string id = string.Format(
				"{0},{1},{2},{3}", 
				p.GetField("left"),
				p.GetField("top"), 
				p.GetField("width"), 
				p.GetField("height")
			);

			if (faceRectangles == "") {
				faceRectangles = id;
			} else {
				faceRectangles += ";" + id;
			}
		}

		url = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?faceRectangles=" + faceRectangles;

		headers["Ocp-Apim-Subscription-Key"] = EmotionAPIKey;

		www = new WWW(url, imageData, headers);
		yield return www;
		responseString = www.text;

		j = new JSONObject(responseString);

		if (j.list.Count > 0) {
			var obj = j.list[0].GetField("scores");
			string highestEmotion = "Unknown";
			float highestC = 0;
			for (int i = 0; i < obj.list.Count; i++)
			{
				string key = obj.keys[i];
				float c = obj.list[i].f;
				if (c > highestC) {
					highestEmotion = key;
					highestC = c;
				}
			}			
			Debug.Log (highestEmotion);
			if (currentEmotion != highestEmotion) {
				neutral.active = highestEmotion == "neutral";
				happy.active = highestEmotion == "happiness";
				sad.active = highestEmotion == "sadness";
				currentEmotion = highestEmotion;
			}
		}
	}


	void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
	{
		if (result.success)
		{
			Debug.Log("photo captured");
			List<byte> imageBufferList = new List<byte>();
			photoCaptureFrame.CopyRawImageDataIntoBuffer(imageBufferList);
			StartCoroutine (PostToFaceAPI (imageBufferList.ToArray ()));
		}
		photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
	}

	void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
	{
		photoCaptureObject.Dispose();
		photoCaptureObject = null;
	}

	private double lastTapTime = 0.0;

	void Update() {
		recognizer = new GestureRecognizer();
		recognizer.TappedEvent += (source, tapCount, ray) =>
		{
			if (Time.time - lastTapTime > 2) {
				PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
				lastTapTime = Time.time;
			}
		};
		recognizer.StartCapturingGestures();
	}

	IEnumerator ScreenshotEncode() {
		yield return new WaitForEndOfFrame();

		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		texture.Apply();

		yield return 0;

		byte[] bytes = texture.EncodeToPNG();
		StartCoroutine (PostToFaceAPI (bytes));
	}
}
