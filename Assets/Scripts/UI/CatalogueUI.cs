using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogueUI : MonoBehaviour
{
    [SerializeField] GameObject itemButton = null;
    [SerializeField] GameObject parent = null;
    //[SerializeField] GameObject category = null;
    //[SerializeField, ReadOnly] GameObject subParent = null;

    // Start is called before the first frame update
    void Start()
    {
        BuildCatalogue();
    }

    void BuildCatalogue()
    {
        //MakeCategory("Structure");
        foreach (Item _struct in DataBase.Instance.Catalogue)
        {
            if (_struct is not Structure) continue;
            MakeItemButton(_struct, parent);
        }
        //MakeCategory("Accessory");
        foreach (Item _access in DataBase.Instance.Catalogue)
        {
            if (_access is not Accessory) continue;
            MakeItemButton(_access, parent);
        }
    }

    //void MakeCategory(string _title)
    //{
    //    GameObject _categoryTitle = Instantiate(category, parent.transform);
    //    TextMeshProUGUI _textField = category.GetComponentInChildren<TextMeshProUGUI>();
    //    _textField.text = _title;
    //    subParent = _categoryTitle;
    //}

    void MakeItemButton(Item _item, GameObject _parent)
    {
        GameObject _button = Instantiate(itemButton, _parent.transform);
        Image _imageField = _button.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI _textField = _button.GetComponentInChildren<TextMeshProUGUI>();
        _button.GetComponentInChildren<ItemButton>().Item = _item;
        _imageField.sprite = _item.Image;
        _textField.text = _item.Label;
    }
}