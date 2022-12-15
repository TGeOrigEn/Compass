using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Marker marker;

    void Update()
    {
        if (marker == null) return;
        transform.GetComponentInChildren<TextMeshProUGUI>().text = marker.description;
        GetComponent<Button>().onClick.AddListener(marker.Show);
        marker = null;
    }
}
