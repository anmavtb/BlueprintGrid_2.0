using UnityEngine;

public class FinishButton : GenericButton
{
    protected override void Behaviour()
    {
        PriceCalculator.Instance.SendPrice();
    }
}