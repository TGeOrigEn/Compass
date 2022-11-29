using System.Diagnostics;
using UnityEngine;

public class Human : MonoBehaviour
{
    public bool locationPermission = UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation);
 
    public Vector2 position;

    public float angle;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (locationPermission)
        {
            var locationInfo = GetLocationInfo();
            if (!locationInfo.Equals(new LocationInfo()))
            {
                position = new(locationInfo.altitude * 10000f - 550230f, locationInfo.latitude * 10000f - 732980f);
                angle = Input.gyro.userAcceleration.x;
            }
        }

        transform.position = position;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private static LocationInfo GetLocationInfo()
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
