using System.Collections.Generic;
using UnityEngine;

public class SnapGrid : MonoBehaviour
{
    [SerializeField] MeshFilter filter;
    [SerializeField] Vector2Int gridSize = Vector2Int.zero;
    [SerializeField] float spaceValue = .5f;

    Mesh mesh;
    MeshRenderer surface = null;
    List<Vector3> verticies;
    List<int> indices;

    public Vector3 Extents => IsValid ? surface.bounds.extents : Vector3.zero;
    public bool IsValid => surface;
    public float CellSize => spaceValue;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        surface = filter.GetComponent<MeshRenderer>();
        surface.material.color = Color.white;
        BuildGrid();
    }

    void BuildGrid()
    {
        verticies = new List<Vector3>();
        indices = new List<int>();

        float xMin = spaceValue * gridSize.x / 2f;
        float zMin = spaceValue * gridSize.y / 2f;

        for (int i = 0; i <= gridSize.x; i++)
        {
            for (int j = 0; j <= gridSize.y; j++)
            {
                float x1 = i * spaceValue - xMin;
                float x2 = (i + 1) * spaceValue - xMin;
                float z1 = j * spaceValue - zMin;
                float z2 = (j + 1) * spaceValue - zMin;

                if (i != gridSize.x)
                {
                    verticies.Add(new Vector3(x1, 0, z1));
                    verticies.Add(new Vector3(x2, 0, z1));
                }

                if (j != gridSize.y)
                {
                    verticies.Add(new Vector3(x1, 0, z1));
                    verticies.Add(new Vector3(x1, 0, z2));
                }
            }
        }

        int indicesCount = verticies.Count;
        for (int i = 0; i < indicesCount; i++)
        {
            indices.Add(i);
        }

        mesh.vertices = verticies.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;
}

    public Vector3 GetSnapPosition(Vector3 _pos)
    {
        float _x = Mathf.Round(_pos.x * 2) / 2;
        float _z = Mathf.Round(_pos.z * 2) / 2;
        float _xLimit = Extents.x;
        float _zLimit = Extents.z;
        _x = Mathf.Clamp(_x, -_xLimit, _xLimit);
        _z = Mathf.Clamp(_z, -_zLimit, _zLimit);
        return new Vector3(_x, _pos.y, _z);
    }
}