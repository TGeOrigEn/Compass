using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public GameObject selected;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.touches[0].position), out RaycastHit hit))
                if (hit.collider != null && hit.transform.CompareTag("Marker"))
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        
    }
}
