using UnityEngine;

public class UI : MonoBehaviour
{
    public static bool IsOver { get; private set; } = false;
    Rect menuRect = new Rect(0, 0, Screen.width * .2f, Screen.height);

    private void OnGUI()
    {
        menuRect = GUI.Window(0, menuRect, Window, "");
        IsOver = menuRect.Contains(Event.current.mousePosition);
    }

    void Window(int _id)
    {
        GUILayout.Box("Items");
        DrawGameItemsUI();
        GUI.DragWindow();
    }

    void DrawGameItemsUI()
    {
        Item[] _items = DataBase.Instance.Catalogue;
        int _max = _items.Length;
        for (int i = 0; i < _max; i++)
        {
            if (GUILayout.Button(_items[i].name))
                ItemPlacementManager.Instance.CreateItem(_items[i]);
        }
    }
}