using UnityEngine;

public class RectTransformController : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector3 _newPosition = Vector3.zero;

    public void SetRectTransformX(float x)
    {
        _rectTransform = GetComponent<RectTransform>();
        _newPosition = _rectTransform.localPosition;
        _newPosition.x = x;
        _rectTransform.localPosition = _newPosition;
    }

    public void SetRectTransformY(float y)
    {
        _rectTransform = GetComponent<RectTransform>();
        _newPosition = _rectTransform.localPosition;
        _newPosition.y = y;
        _rectTransform.localPosition = _newPosition;
    }
}
