using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPlacementManager : Singleton<ItemPlacementManager>
{
    [SerializeField] Vector3 itemLocation = Vector3.zero;
    [SerializeField] Item currentItem = null;
    [SerializeField] SnapGrid grid = null;
    [SerializeField] bool wrongSelectionCheck = false;
    [SerializeField] float selectorCooldown = 0;

    public bool IsValid => grid;

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
        if (UI.IsOver) return;
        UpdateItemPosition();
        if (Cursor.Instance.SelectionInput.triggered)
            DropItem();
        WrongSelectorCoolDown();
    }

    public void CreateItem(Item _item)
    {
        if (currentItem) return;
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
        if (!currentItem || !currentItem.CanDeselect || wrongSelectionCheck) return;
        currentItem.DeselectItem();
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
        if (!currentItem) return;
        currentItem.RotateItem(_rotateValue);
    }
}