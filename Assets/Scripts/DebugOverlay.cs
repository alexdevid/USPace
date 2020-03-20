using UnityEngine;
using UnityEngine.UI;

public class DebugOverlay : MonoBehaviour
{
    private float _deltaTime;
    private Text _fpsLabel;

    private void Start()
    {
        GameObject fpsLabelGameObject = new GameObject("_fpsLabel");
        GameObject canvasGameObject = GameObject.Find("Canvas");
        
        _fpsLabel = fpsLabelGameObject.AddComponent<Text>();
        _fpsLabel.font = Font.CreateDynamicFontFromOSFont("Arial", 14);

        fpsLabelGameObject.transform.SetParent(canvasGameObject.transform);
        //fpsLabelGameObject.GetComponent<RectTransform>().anchorMin = Vector2.one;
        //fpsLabelGameObject.GetComponent<RectTransform>().anchorMax = Vector2.one;
        
        RectTransform rt = fpsLabelGameObject.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, rt.rect.width);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
    }

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
    }
    
    private void OnGUI()
    {
        float msec = _deltaTime * 1000.0f;
        float fps = 1.0f / _deltaTime;

        _fpsLabel.text = $"FPS: {fps}";
    }
}