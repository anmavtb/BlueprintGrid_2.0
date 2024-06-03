using UnityEngine;
using UnityEngine.UI;

public class CameraChangeView : Singleton<CameraChangeView>
{
    [SerializeField] Button button2D = null;
    [SerializeField] Button button3D = null;

    // Start is called before the first frame update
    void Start()
    {
        SwitchButtons();
    }

    public void SwitchButtons()
    {
        if (GameCamera.Instance.IsTopDownView)
        {
            button2D.interactable = false;
            button3D.interactable = true;
        }
        else
        {
            button2D.interactable = true;
            button3D.interactable = false;
        }
    }
}