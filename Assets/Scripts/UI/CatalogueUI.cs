using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogueUI : MonoBehaviour
{
    [SerializeField] GameObject itemButton = null;
    [SerializeField] GameObject parent = null;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Item _item in DataBase.Instance.Catalogue)
        {
            GameObject _button = Instantiate(itemButton, parent.transform);
            Image _imageField = _button.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI _textField = _button.GetComponentInChildren<TextMeshProUGUI>();
            _button.GetComponentInChildren<ItemButton>().Item = _item;

            _imageField.sprite = _item.Image;
            _textField.text = _item.DisplayName;
        }
    }
}