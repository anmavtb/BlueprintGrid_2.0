using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] bool isSelected = false, canDeselect = true;
    [SerializeField] float rotateValue = 90;
    Item collideItem = null;

    public bool CanDeselect => canDeselect;

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
        AnmaGizmos.DrawSphere(transform.position, .2f, canDeselect ? Color.green : Color.red, AnmaGizmos.DrawMode.Wire, DebugManager.Instance.debugItemPlacement);
    }
}