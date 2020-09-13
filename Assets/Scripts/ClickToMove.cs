using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    public LayerMask groundLayer;
    [System.Serializable]
    public class PointEvent : UnityEvent<Vector3> { }
    public PointEvent pointEvent;
    private Camera mainCam;
    private EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.IsPointerOverGameObject())
            return;
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 50, groundLayer))
            {
                pointEvent.Invoke(hit.transform.position);
            }
        }
    }
}
