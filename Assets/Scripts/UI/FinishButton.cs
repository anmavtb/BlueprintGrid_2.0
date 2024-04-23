using UnityEngine;

public class FinishButton : GenericButton
{
    protected override void Behaviour()
    {
        Debug.Log("Prix final : " + PriceCalculator.Instance.TotalPrice);
    }
}