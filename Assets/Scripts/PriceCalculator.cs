using UnityEngine;

public class PriceCalculator : Singleton<PriceCalculator>
{
    [SerializeField, ReadOnly] float totalPrice = 0;

    public float TotalPrice => totalPrice;

    // Update is called once per frame
    void Update()
    {
        if (ItemPlacementManager.Instance.ItemsPlaced.Count <= 0) return;
        CalculatePrice();
    }

    void CalculatePrice()
    {
        float intermediatePrice = 0;
        foreach (Item item in ItemPlacementManager.Instance.ItemsPlaced)
        {
            intermediatePrice += item.Price;
        }
        totalPrice = intermediatePrice;
    }

    public void SendPrice()
    {
        Debug.Log(totalPrice.ToString());
    }
}