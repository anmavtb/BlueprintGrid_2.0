using TMPro;
using UnityEngine;

public class PriceCalculator : Singleton<PriceCalculator>
{
    [SerializeField, ReadOnly] float totalPrice = 0;
    [SerializeField, ReadOnly] TextMeshProUGUI priceText = null;

    public float TotalPrice => totalPrice;

    // Start is called before the first frame update
    void Start()
    {
        priceText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemPlacementManager.Instance.ItemsPlaced.Count <= 0) return;
        CalculatePrice();
        UptadeTextBox(totalPrice);
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

    void UptadeTextBox(float _price)
    {
        priceText.text = $"{_price} €";
    }

    public void SendPrice()
    {
        Debug.Log(totalPrice.ToString());
    }
}