using UnityEngine;
using UnityEngine.UI;

public class DisplayGridButton : MonoBehaviour
{
    [SerializeField] MeshRenderer surface = null;
    protected Toggle toggle = null;

    // Start is called before the first frame update
    protected void Start()
    {
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener((b) =>
        {
            ChangeGridDisplay(b);
        });
    }

    protected void ChangeGridDisplay(bool _value)
    {
        surface.enabled = _value;
    }
}