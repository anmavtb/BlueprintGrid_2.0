using UnityEngine;

public class RotateButton : GenericButton
{
    protected override void Behaviour()
    {
        ItemPlacementManager.Instance.RotateItem(1);
    }
}