using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    
    public ARRaycastManager arRaycaster;
    public GameObject spawnPrefab;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Update is called once per frame
    bool isActive = false;

    void Awake()
    {
        ARSession.stateChanged += OnstateChanged;
    }
    //AR 라이프사이클 확인하기
    void OnstateChanged(ARSessionStateChangedEventArgs args)
    {
        Debug.Log(args.state);

        if (args.state == ARSessionState.Unsupported)
        {//지원되지 않는 기기라면 종료
            Application.Quit();
            return;
        }
        if (args.state == ARSessionState.Ready)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
    void Update()
    {
        if (!isActive) return;
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        //bool 타입 보다 Touch가 많은 정보를 가지고 있다
        if (touch.phase != TouchPhase.Began)
        {
            return;
        }

        if(arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
        {
            //평면들을 대상으로만 Raycast 실행
            //동시에 여러 평면을 감지할 수도 있어서 list 타입으로 hits에 담아둠
            Pose hitPose = hits[0].pose;
            //Pose로 카메라의 위치에 대한 상대위치나 그런 좀 더 고급정보들을 담을 수 있음
            Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
            //클릭시 Plane에 프리팹 생성되는 기능
        }

    }
}
