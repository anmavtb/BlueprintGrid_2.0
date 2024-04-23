using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPlacementManager : Singleton<ItemPlacementManager>
{
    [SerializeField, ReadOnly] Vector3 itemLocation = Vector3.zero;
    [SerializeField, ReadOnly] Item currentItem = null;
    [SerializeField] SnapGrid grid = null;
    [SerializeField, ReadOnly] bool wrongSelectionCheck = false;
    [SerializeField, ReadOnly] float selectorCooldown = 0;
    [SerializeField, ReadOnly] List<Item> itemsPlaced = new();

    public bool IsValid => grid;

    public List<Item> ItemsPlaced => itemsPlaced;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.Instance.OnEditableSurface += SetPosition;
        Cursor.Instance.OnSelection += SetItem;
        Cursor.Instance.RotateInput.performed += RotateCurrentItem;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemPosition();
        if (Cursor.Instance.SelectionInput.triggered)
            DropItem();
        WrongSelectorCoolDown();
    }

    public void CreateItem(Item _item)
    {
        if (currentItem)
            DestroyCurrentItem();
        SetItem(Instantiate(_item));
    }

    void SetItem(Item _item)
    {
        if (currentItem || wrongSelectionCheck) return;
        currentItem = _item;
        currentItem.SelectItem();
        wrongSelectionCheck = true;
    }

    void SetPosition(Vector3 _pos)
    {
        itemLocation = IsValid ? grid.GetSnapPosition(_pos) : _pos;
    }

    void UpdateItemPosition()
    {
        if (!IsValid || !currentItem) return;
        currentItem.SetPosition(itemLocation);
    }

    void DropItem()
    {
        if (!currentItem || !currentItem.CanDeselect || wrongSelectionCheck || !Cursor.Instance.IsMouseOverGrid()) return;
        currentItem.DeselectItem();
        if (!itemsPlaced.Contains(currentItem))
            itemsPlaced.Add(currentItem);
        currentItem = null;
    }

    public void WrongSelectorCoolDown()
    {
        if (!wrongSelectionCheck) return;
        selectorCooldown += Time.deltaTime;
        if (selectorCooldown > .1f)
        {
            selectorCooldown = 0;
            wrongSelectionCheck = false;
        }
    }

    void RotateCurrentItem(InputAction.CallbackContext _context)
    {
        float _rotateValue = Cursor.Instance.RotateInput.ReadValue<float>();
        RotateItem(_rotateValue);
    }

    public void RotateItem(float _rotateValue)
    {
        if (!currentItem) return;
        currentItem.RotateItem(_rotateValue);
    }

    public void CancelPlacement()
    {
        if (itemsPlaced.Count <= 0) return;
        Item lastItem = itemsPlaced.Last();
        itemsPlaced.Remove(lastItem);
        Destroy(lastItem.gameObject);
    }

    public void ClearAll()
    {
        if (currentItem)
            DestroyCurrentItem();
        if (itemsPlaced.Count <= 0) return;
        foreach (Item item in itemsPlaced)
            Destroy(item.gameObject);
        itemsPlaced.Clear();
    }

    void DestroyCurrentItem()
    {
        Destroy(currentItem.gameObject);
        currentItem = null;
    }
}