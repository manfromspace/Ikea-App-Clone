using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Globalization;
using UnityEngine.EventSystems;
using System.CodeDom.Compiler;

public class InputManager : MonoBehaviour
{
    
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;
    private bool isDetecting;

    // Start is called before the first frame update
   public void Start()
    {
        isDetecting = true;
    }

    public void DetectionOff()
    {
        isDetecting = false;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    // Update is called once per frame
    void Update()
    {
        CrosshairCalculation();
        touch = Input.GetTouch(0);

        if ((Input.touchCount < 0 || touch.phase != TouchPhase.Began) && isDetecting == true && !IsPointerOverUIObject())
            return;

        if (IsPointerOverUI(touch)) return;

        Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);






    }

    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0; 
    }
    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray ray = arCam.ScreenPointToRay(origin);

        if (_raycastManager.Raycast(ray, _hits))
        {
             pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
            
        }


    }
}
