using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraRotateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool isRight;
    Button button = null;
    bool isPressed = false;

    // Start is called before the first frame update
    protected void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            if (isRight)
                GameCamera.Instance.Rotate(1);
            else
                GameCamera.Instance.Rotate(-1);
        }
    }

    public void OnPointerDown(PointerEventData _data) { isPressed = true; }

    public void OnPointerUp(PointerEventData eventData) { isPressed = false; }
}
