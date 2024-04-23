using UnityEngine;
using UnityEngine.UI;

public abstract class GenericButton : MonoBehaviour
{
    Button button = null;

    // Start is called before the first frame update
    protected void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            Behaviour();
        });
    }

    abstract protected void Behaviour();
}