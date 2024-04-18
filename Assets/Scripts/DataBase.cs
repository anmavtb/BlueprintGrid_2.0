using UnityEngine;

public class DataBase : Singleton<DataBase>
{
    [SerializeField] Item[] catalogue = null;

    public Item[] Catalogue => catalogue;
}