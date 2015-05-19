using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PushBehaviour : MonoBehaviour {

	string deviceToken = String.Empty;
	bool tokenSent = false;

	void Start () 
    {
        Debug.Log( "Registering for push notifications" );

        NotificationServices.RegisterForRemoteNotificationTypes( RemoteNotificationType.Alert 
            | RemoteNotificationType.Badge | RemoteNotificationType.Sound );

        
	}

	RemoteNotification lastNotification;

	void Update () 
    {
        if ( !tokenSent )
		{
			var token = NotificationServices.deviceToken;
			if ( token != null )
			{
				tokenSent = true;
				deviceToken = System.BitConverter.ToString( token ).Replace("-","");
				Debug.Log( "Device Token: " + deviceToken );
			}
		}

		if( NotificationServices.remoteNotificationCount > 0)
		{
			lastNotification = NotificationServices.remoteNotifications[0];
			
			Debug.Log("AlertBody " + lastNotification.alertBody);
			Debug.Log("UserInfo " + lastNotification.userInfo);

			NotificationServices.ClearRemoteNotifications();

		}
	    
	}

	void OnGUI()
	{
		GUILayout.BeginVertical();

		GUILayout.Label(string.Format("Device Token: {0}", deviceToken));

		if( lastNotification != null )
		{
			GUILayout.Label(string.Format("AlertBody: {0}", lastNotification.alertBody));
			GUILayout.Label(string.Format("UserInfo:"));
			foreach(var key in lastNotification.userInfo.Keys )
			{
				GUILayout.Label(string.Format("{0}: {1}", key, lastNotification.userInfo[key]));
			}

		}

		GUILayout.EndVertical();
	}

}
