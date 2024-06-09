using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPlacementManager : Singleton<ItemPlacementManager>
{
    [SerializeField, ReadOnly] Vector3 itemLocation = Vector3.zero;
    [SerializeField, ReadOnly] Item currentItem = null;
    [SerializeField, ReadOnly] Item previousItem = null;
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
        InputManager.Instance.RotateItemInput.performed += RotateCurrentItem;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemPosition();
            if (InputManager.Instance.SelectionInput.triggered)
                DropItem();
            if (InputManager.Instance.CancelSelection.triggered)
                DestroyCurrentItem();
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
        if (!_item)
        {
            Debug.LogError("SetItem : called with a null _item");
            return;
        }
        currentItem = _item;
        previousItem = _item;
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
        CreateItem(previousItem);
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
        float _rotateValue = InputManager.Instance.RotateItemInput.ReadValue<float>();
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
        RemoveItemFromList(lastItem);
        Destroy(lastItem.gameObject);
    }

    void RemoveItemFromList(Item _item)
    {
        if (!itemsPlaced.Contains(_item))
            return;
        itemsPlaced.Remove(_item);
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
        if (!currentItem) return;
        Destroy(currentItem.gameObject);
        currentItem = null;
        previousItem = null;
    }

    public bool IsItemPlaced(Item _item)
    {
        return itemsPlaced.Contains(_item);
    }

    public void PickUpItem(Item _item)
    {
        if (itemsPlaced.Count <= 0) return;
        RemoveItemFromList(_item);
        SetItem(_item);
    }
}