using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//터치한 장소를 arsessionorigin으로 만들어서 물체를 옮겨보자
public class MoveWorld : MonoBehaviour
{
    public Transform target;

    public ARRaycastManager raycastManager;
    public ARSessionOrigin arSessionOrigin;

    public List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitpose = hits[0].pose;
            arSessionOrigin.MakeContentAppearAt(target, hitpose.position, hitpose.rotation);
            //target이 hitpose에 위치하도록 세상을 바꿈
        }
    }
}
