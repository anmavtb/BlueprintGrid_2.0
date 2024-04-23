using UnityEngine;

public class ItemButton : GenericButton
{
    [SerializeField, ReadOnly] Item item = null;

    public Item Item { get { return item; } set { item = value; } }

    protected override void Behaviour()
    {
        ItemPlacementManager.Instance.CreateItem(item);
    }
}