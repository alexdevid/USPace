using Scene;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public Camera mainCamera;
    
    private const float ZoomMin = 20;
    private const float ZoomMax = 70;
    private const float ZoomSensitivity = 20;
    
    private bool _drag;
    
    private Vector3 _positionOrigin;
    private Vector3 _positionDelta;
    private Vector3 _cameraPosition;
    
    private float _halfWidth;
    private float _halfHeight;

    private void Start()
    {
        _cameraPosition = mainCamera.transform.position;
        
        UpdateCameraSize();
    }

    private void Update()
    {
        UpdateCameraSize();
        
        if (Input.GetMouseButton(0))
        {
            _positionDelta = (mainCamera.ScreenToWorldPoint(Input.mousePosition)) - _cameraPosition;
            if (_drag == false)
            {
                _drag = true;
                _positionOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }
        
        mainCamera.orthographicSize = GetZoom();
        mainCamera.transform.position = GetCameraPosition();
    }

    private Vector3 GetCameraPosition()
    {
        if (_drag)
        {
            _cameraPosition = _positionOrigin - _positionDelta;
        }
        
        _cameraPosition.x = Mathf.Clamp(_cameraPosition.x, -Game.GameScreenSize / 2 + _halfWidth, Game.GameScreenSize / 2 - _halfWidth);
        _cameraPosition.y = Mathf.Clamp(_cameraPosition.y,-Game.GameScreenSize / 2 + _halfHeight, Game.GameScreenSize / 2 - _halfHeight);

        return _cameraPosition;
    }

    private float GetZoom()
    {
        float zoom = mainCamera.orthographicSize;
        zoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity;
        
        return Mathf.Clamp(zoom, ZoomMin, ZoomMax);
    }
    
    private void UpdateCameraSize()
    {
        _halfHeight = mainCamera.orthographicSize;
        _halfWidth = mainCamera.aspect * _halfHeight;
    }
}