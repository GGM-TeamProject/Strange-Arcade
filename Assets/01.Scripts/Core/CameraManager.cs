using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Body Setting")]
    [SerializeField] public bool CanFallowValue_X = false;
    [SerializeField] public bool CanFallowValue_y = false;
    [SerializeField] public Vector2 Offset;
    
    [Header("Fallow")]
    public Transform FallowTarget = null;

    private Camera _gameCamera;

    private Vector3 _initCamPos = new Vector3(0, 0, -10);

    private void Awake() {
        _gameCamera = GameObject.Find("Screen/InGameCamera").GetComponent<Camera>();
    }

    private void Update() {
        if(FallowTarget  != null){
            MoveCamera(SetMovementValue());
        }
        else{
            MoveCamera(_initCamPos);
        }
    }

    private Vector3 SetMovementValue(){
        Vector3 movementValue = FallowTarget.position;

        if(!CanFallowValue_X) movementValue.x = 0f;
        if(!CanFallowValue_y) movementValue.y = 0f;

        movementValue += (Vector3)Offset;

        return movementValue;
    }

    public void CamSetting(Transform target, bool canFallow_X = true, bool canFallow_Y = true, Vector2? offset = null){
        FallowTarget = target;
        CanFallowValue_X = canFallow_X;
        CanFallowValue_y = canFallow_Y;
        Offset = (offset == null) ? Vector2.zero : (Vector2)offset;
    }
    
    public void MoveCamera(Vector3 movementPos){
        _gameCamera.transform.position = new Vector3(movementPos.x, movementPos.y, _initCamPos.z);
    }
}
