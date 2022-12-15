using System;
using UnityEngine;

public class Joke : MonoBehaviour
{
    const double D_Y = 7366253.21162507d;

    const double D_X = 8159688.44005576d;

    public float lat;

    public float lon;   

    void Start()
    {

    }

    void Update()
    {
        transform.position = new(X(), Y());
    }

    public float X()
    {
        var x = lon * 2 * Math.PI * 6378137 / 2 / 180;
        return (float)(x - D_X);
    }

    public float Y()
    {
        var y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
        y = y * 2 * Math.PI * 6378137 / 2 / 180;
        return (float) (y - D_Y);
    }
}
