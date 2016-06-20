using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.InteropServices;

public class ShareImage : MonoBehaviour {

    private bool isProcessing = false;
    private string shareText = "#BlockDash";
    private string subject = "#BlockDash";
    public void onShareImage()
    {
        if (!isProcessing)
            StartCoroutine(ShareScreenshot());
    }
    private IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();
        // take screen shot
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();
        byte[] dataToSave = screenTexture.EncodeToPNG();
        // save
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);

#if UNITY_ANDROID
        ShareImageFromPath(destination);
#elif UNITY_IOS
        ShareImageFromPath(destination);
#endif
        isProcessing = false;
    }
#if UNITY_ANDROID
    private void ShareImageFromPath(string _destination)
    {
        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + _destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intentObject);
        }
    }

#elif UNITY_IOS
    private void ShareImageFromPath(string _destination)
    {
        GeneralSharingiOSBridge.ShareTextWithImage(_destination, shareText);
    }
#endif
}
