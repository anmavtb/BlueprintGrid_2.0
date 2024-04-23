using UnityEngine;

public class ClearButton : GenericButton
{
    protected override void Behaviour()
    {
        ItemPlacementManager.Instance.ClearAll();
    }
}