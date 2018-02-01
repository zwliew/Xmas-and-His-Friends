using System;
using UnityEngine;

public class AppUtility {
	public static RuntimePlatform platform {
		get {
#if UNITY_ANDROID
			return RuntimePlatform.Android;
#elif UNITY_STANDALONE_WIN
			return RuntimePlatform.WindowsPlayer;
#elif UNITY_STANDALONE_OSX
			return RuntimePlatform.OSXPlayer;
#endif
		}
	}			
}
