using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class Search : MonoBehaviour
{
    public TextMeshPro textMesh;
    public GameObject item;
    public GameObject viewPort;

    void Start()
    {
        var markers = GameObject.FindGameObjectsWithTag("Marker").Select(x => x.GetComponent<Marker>()).ToArray();

        for (int i = 0; i < markers.Length; i++)
        {
            var g = Instantiate(item, viewPort.transform);
            g.GetComponent<Item>().marker = markers[i];
            g.GetComponent<RectTransform>().position -= new Vector3(0, i * 100);
        }

        viewPort.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 100 * markers.Length);
    }

    void Update()
    {
    }
}
