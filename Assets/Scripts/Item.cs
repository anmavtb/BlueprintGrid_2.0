using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string displayName = "";
    [SerializeField] protected float price = 0;
    [SerializeField] protected Sprite image = null;

    [SerializeField, ReadOnly] protected bool isSelected = false, canDeselect = true;
    [SerializeField, ReadOnly] protected float rotateValue = 90;
    Item collideItem = null;

    public bool CanDeselect => canDeselect;

    public string DisplayName => displayName;
    public float Price => price;
    public Sprite Image => image;

    public void SetPosition(Vector3 _pos)
    {
        if (!isSelected) return;
        transform.position = _pos;
    }

    public void SelectItem()
    {
        isSelected = true;
    }

    public void DeselectItem()
    {
        if (!canDeselect) return;
        isSelected = false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (!collideItem) return;
        Debug.Log(_other.gameObject.name);
        collideItem = _other.GetComponent<Item>();
        canDeselect = false;
    }

    private void OnTriggerExit(Collider _other)
    {
        canDeselect = true;
        collideItem = null;
    }

    public void RotateItem(float _axis)
    {
        if (!isSelected) return;
        transform.eulerAngles += Vector3.up * rotateValue * _axis;
    }

    private void OnDrawGizmos()
    {
        if (!DebugManager.Instance) return;
        AnmaGizmos.DrawSphere(transform.position, .2f, canDeselect ? Color.green : Color.red, AnmaGizmos.DrawMode.Wire, DebugManager.Instance.debugItemPlacement);
    }
}