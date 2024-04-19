using UnityEngine;

public class UI : MonoBehaviour
{
    public static bool IsOver { get; private set; } = false;
    Rect menuLeftRect = new Rect(0, 0, Screen.width * .2f, Screen.height);
    Rect menuTopRightRect = new Rect(Screen.width - Screen.width * .2f, 0, Screen.width * .2f, Screen.height * .2f);
    Rect menuBottomRightRect = new Rect(Screen.width - Screen.width * .2f, Screen.height - Screen.height * .25f, Screen.width * .2f, Screen.height * .25f);

    GUIStyle style = new();

    private void OnGUI()
    {
        menuLeftRect = GUI.Window(0, menuLeftRect, Window, "Items");
        menuTopRightRect = GUI.Window(1, menuTopRightRect, Window, "Total Price :");
        menuBottomRightRect = GUI.Window(2, menuBottomRightRect, Window, "Menu");
        IsOver = menuLeftRect.Contains(Event.current.mousePosition) || menuTopRightRect.Contains(Event.current.mousePosition) || menuBottomRightRect.Contains(Event.current.mousePosition);
    }

    void Window(int _id)
    {
        if (_id == 0)
            DrawGameItemsUI();
        else if (_id == 1)
            DrawMenuUI();
        else
            DrawButtonsMenuUI();
        GUI.DragWindow();
    }

    void DrawGameItemsUI()
    {
        Item[] _items = DataBase.Instance.Catalogue;
        int _max = _items.Length;
        for (int i = 0; i < _max; i++)
        {
            if (GUILayout.Button($"{_items[i].DisplayName} | {_items[i].Price} €"))
                ItemPlacementManager.Instance.CreateItem(_items[i]);
        }
    }

    void DrawMenuUI()
    {
        GUILayout.BeginVertical();
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleRight;
        GUILayout.Label($"{PriceCalculator.Instance.TotalPrice} €", style);
        if (GUILayout.Button("Finish"))
            PriceCalculator.Instance.SendPrice();
        GUILayout.EndVertical();
    }

    void DrawButtonsMenuUI()
    {
        GUILayout.BeginVertical();
        if (GUILayout.Button("Rotate (R)"))
            ItemPlacementManager.Instance.RotateItem(1);
        if (GUILayout.Button("Cancel"))
            ItemPlacementManager.Instance.CancelPlacement();
        if (GUILayout.Button("Clear"))
            ItemPlacementManager.Instance.ClearAll();
        GUILayout.EndVertical();
    }
}