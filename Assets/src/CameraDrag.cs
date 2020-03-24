using Scene;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private const float ZoomMin = 5;
    private const float ZoomMax = 500;
    public float zoomSensitivity = 20;

    public Transform[] parallaxLayers;
    
    private bool _drag;
    
    private Vector3 _positionOrigin;
    private Vector3 _positionDelta;
    private Vector3 _cameraPosition;
    
    private float _halfWidth;
    private float _halfHeight;

    public bool interactive = true;

    private void Start()
    {
        interactive = true;
        _cameraPosition = gameObject.GetComponent<Camera>().transform.position;
        
        UpdateCameraSize();
    }

    private void Update()
    {
        if (!interactive)
        {
            return;
        }
        
        UpdateCameraSize();
        
        if (Input.GetMouseButton(0))
        {
            _positionDelta = (gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)) - _cameraPosition;
            if (_drag == false)
            {
                _drag = true;
                _positionOrigin = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        UpdateCameraPosition();
        
        gameObject.GetComponent<Camera>().orthographicSize = GetZoom();
        gameObject.GetComponent<Camera>().transform.position = _cameraPosition;

        float speed = 1.01f;
        foreach (Transform parallaxLayer in parallaxLayers)
        {
            parallaxLayer.transform.position = new Vector3(_cameraPosition.x / speed, _cameraPosition.y / speed, 0);
            speed += 0.01f;
        }
    }

    private void UpdateCameraPosition()
    {
        if (_drag)
        {
            _cameraPosition = _positionOrigin - _positionDelta;
        }
        
        _cameraPosition.x = Mathf.Clamp(_cameraPosition.x, -Scene.Game.GameScreenSize / 2 + _halfWidth, Scene.Game.GameScreenSize / 2 - _halfWidth);
        _cameraPosition.y = Mathf.Clamp(_cameraPosition.y,-Scene.Game.GameScreenSize / 2 + _halfHeight, Scene.Game.GameScreenSize / 2 - _halfHeight);
    }

    private float GetZoom()
    {
        float zoom = gameObject.GetComponent<Camera>().orthographicSize;
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        
        return Mathf.Clamp(zoom, ZoomMin, ZoomMax);
    }
    
    private void UpdateCameraSize()
    {
        _halfHeight = gameObject.GetComponent<Camera>().orthographicSize;
        _halfWidth = gameObject.GetComponent<Camera>().aspect * _halfHeight;
    }
}