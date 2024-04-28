using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditorInternal.VersionControl;
using UnityEngine;

[Serializable]
class FinishList
{
    public string Label { get; set; }
    public float Price { get; set; }
    public float Count { get; set; }

    // Constructor to initialize the values
    public FinishList(string _label, float _price, float _count)
    {
        Label = _label;
        Price = _price;
        Count = _count;
    }
}

public class PriceCalculator : Singleton<PriceCalculator>
{
    [SerializeField, ReadOnly] float totalPrice = 0;
    [SerializeField, ReadOnly] TextMeshProUGUI priceText = null;
    [SerializeField, ReadOnly] List<FinishList> finishList = new();

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
        buildFinalList();
        if (finishList.Count == 0) return;
        float finalPrice = 0;
        foreach (var _item in finishList)
        {
            Debug.Log($"{_item.Label} ({_item.Price} €) x {_item.Count} = {_item.Price * _item.Count} €");
            finalPrice += _item.Price * _item.Count;
        }
        Debug.Log($"Total = {finalPrice} €");
    }

    void buildFinalList()
    {
        if (ItemPlacementManager.Instance.ItemsPlaced.Count == 0) return;
        foreach (Item _item in ItemPlacementManager.Instance.ItemsPlaced)
        {
            bool _isNewItem = true;
            foreach (var _object in finishList)
            {
                if (_object.Label == _item.Label)
                {
                    _object.Count++;
                    _isNewItem = false;
                }
            }
            if (_isNewItem)
                finishList.Add(new FinishList(_item.Label, _item.Price, 1));
        }
    }
}