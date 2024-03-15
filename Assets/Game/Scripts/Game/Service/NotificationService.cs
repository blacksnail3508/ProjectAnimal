using LazyFramework;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NotificationData
{
    public List<int> skateNotification;

    public NotificationData()
    {
        skateNotification=new List<int>();
    }
}
public static class NotificationService
{
    private static NotificationData notificationData;

    public static void Run()
    {
        //if new user, create new
        if (PlayerPrefs.HasKey(KeyString.Noti)==false)
        {
            //init starting items
            notificationData = new NotificationData();
            notificationData.skateNotification=new List<int> { };
            Save(notificationData);
        }
        else
        {
            string json = PlayerPrefs.GetString(KeyString.Noti);
            notificationData=JsonUtility.FromJson<NotificationData>(json);
        }
    }

    public static void Save(NotificationData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KeyString.Noti , json);
    }

    public static void PushSkateNotification(int id)
    {
        if (notificationData.skateNotification.Contains(id)==false)
        {
            notificationData.skateNotification.Add(id);
            Save(notificationData);

            Event<OnNotificationChange>.Post(new OnNotificationChange());
        }
    }
    public static void RemoveSkateNotification(int id)
    {
        if (notificationData.skateNotification.Contains(id)==true)
        {
            notificationData.skateNotification.Remove(id);
            Save(notificationData);

            Event<OnNotificationChange>.Post(new OnNotificationChange());
        }
    }

    public static bool IsAnySkateNoti()
    {
        Bug.Log("Notification count: "+notificationData.skateNotification.Count);

        if(notificationData.skateNotification.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsSkateNoti(int id)
    {
        if (notificationData.skateNotification.Contains(id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

