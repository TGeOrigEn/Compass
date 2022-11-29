using System.Diagnostics;
using UnityEngine;

public class Geolocation
{
    private static LocationInfo GetLocationService()
    {
        if (!RequestPermission())
            return new LocationInfo();

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
