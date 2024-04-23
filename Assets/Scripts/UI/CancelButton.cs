using UnityEngine;

public class CancelButton : GenericButton
{
    protected override void Behaviour()
    {
        ItemPlacementManager.Instance.CancelPlacement();
    }
}