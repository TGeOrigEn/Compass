using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour, IPointerClickHandler
{
    public Node node;
    public string description = "";
    public Trace trace;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.FindGameObjectsWithTag("Marker").Select(x => x.GetComponent<SpriteRenderer>()).ToList().ForEach(x => x.color = Color.white);
        GetComponent<SpriteRenderer>().color = Color.green;
        trace.Node = node;
        trace.UpdateCurrentPath();
    }

    public void Show()
    {
        GameObject.FindGameObjectsWithTag("Marker").Select(x => x.GetComponent<SpriteRenderer>()).ToList().ForEach(x => x.color = Color.white);
        GetComponent<SpriteRenderer>().color = Color.green;
        trace.Node = node;
        trace.UpdateCurrentPath();
    }
}
