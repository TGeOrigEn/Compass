using System;
using System.Diagnostics;
using UnityEngine;

public class Human : MonoBehaviour
{
    const double D_Y = 7366253.21162507d;

    const double D_X = 8159688.44005576d;

    public bool locationPermission = UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation);
 
    public Vector2 position;

    public float angle;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (RequestPermission())
        {
            var locationInfo = GetLocationInfo();
            if (!locationInfo.Equals(new LocationInfo()))
            {
                position = new(X(locationInfo), Y(locationInfo));
                angle = Input.gyro.attitude.x;
            }
        }

        transform.position = position;

        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public float X(LocationInfo locationInfo)
    {
        var x = locationInfo.longitude * 2 * Math.PI * 6378137 / 2 / 180;
        return (float)(x - D_X);
    }

    public float Y(LocationInfo locationInfo)
    {
        var y = Math.Log(Math.Tan((90 + locationInfo.latitude) * Math.PI / 360)) / (Math.PI / 180);
        y = y * 2 * Math.PI * 6378137 / 2 / 180;
        return (float)(y - D_Y);
    }

    private static LocationInfo GetLocationInfo()
    {
        Input.location.Start(1f, 1f);

        var stopWatch = new Stopwatch();

        while (Input.location.status == LocationServiceStatus.Initializing)
            if (stopWatch.ElapsedMilliseconds >= 30_000) return new LocationInfo();

        if (Input.location.status == LocationServiceStatus.Running)
        {
            Input.location.Stop();
            return Input.location.lastData;
        }
        else
        {
            Input.location.Stop();
            return new LocationInfo();
        }
    }

    private static bool RequestPermission()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation))
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);

        return Input.location.isEnabledByUser;
    }
}
