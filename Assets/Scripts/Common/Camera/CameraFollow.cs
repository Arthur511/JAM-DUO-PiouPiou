using UnityEngine;


/// <summary>
/// Represents a following camera
/// on the X and Y axis
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Tooltip("The target to follow")] [SerializeField]
    GameObject _target;
    
    [Tooltip("Time in seconds to catch the target")] [SerializeField]
    float _speed = 0.3f;
    
    [Header("Limits")] [SerializeField] Transform _topRight;
    [SerializeField] Transform _bottomLeft;

    Transform _transform;
    Vector3 _velocity;
    Camera _camera;
    
    void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //float orthoSizeY = _camera.orthographicSize;
        //float orthoSizeX = (Screen.width * _camera.orthographicSize) / Screen.height;

        var targetPosition = _target.transform.position + new Vector3(0, 10, 0);

        targetPosition.x = Mathf.Clamp(targetPosition.x, _bottomLeft.transform.position.x,
            _topRight.transform.position.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, _bottomLeft.transform.position.z,
            _topRight.transform.position.z);

        _transform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref _velocity, _speed);
    }
}