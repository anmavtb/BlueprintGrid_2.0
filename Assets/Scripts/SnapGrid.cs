using UnityEngine;

public class SnapGrid : MonoBehaviour
{
    [SerializeField] MeshRenderer surface = null;
    [SerializeField] float spaceValue = .5f;

    public Vector3 Extents => IsValid ? surface.bounds.extents : Vector3.zero;
    public bool IsValid => surface;

    private void OnDrawGizmos()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        if (!IsValid) return;
        Transform _surface = surface.transform;
        float _xLimit = Extents.x;
        float _zLimit = Extents.z;
        for (float i = -Extents.x; i <= Extents.x; i += spaceValue)
        {
            for (float j = -Extents.z; j <= Extents.z; j += spaceValue)
            {
                Vector3 _gridpoint = new Vector3(i, Extents.y, j) + _surface.position;
                if (i % 1 == 0 && j % 1 == 0)
                    AnmaGizmos.DrawSphere(_gridpoint, .1f, Color.white, AnmaGizmos.DrawMode.Full);
                else
                    AnmaGizmos.DrawSphere(_gridpoint, .05f, Color.white, AnmaGizmos.DrawMode.Full);
            }
        }
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