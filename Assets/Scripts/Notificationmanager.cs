using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.SceneManagement;

public class Notificationmanager : MonoBehaviour
{
    void Start()
    {
        MakeNotification();
        SendNotification();
    }
    void MakeNotification()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "notif1",
            Name = "Reminder",
            Importance = Importance.High,
            Description = "Reminds the player to play the game",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }
    void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Notification Test";
        notification.Text = "Have you played today?";
        notification.FireTime = System.DateTime.Now.AddSeconds(5);
        notification.LargeIcon = "large_icon_1";
        AndroidNotificationCenter.SendNotification(notification, "notif1");
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
