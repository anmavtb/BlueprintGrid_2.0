using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : Singleton<Cursor>
{
    public event Action<Item> OnSelection = null;
    public event Action<Vector3> OnEditableSurface = null;

    [SerializeField] LayerMask editableSurfaceLayer = 0, itemLayer = 0;
    [SerializeField] Camera gameCamera = null;
    [SerializeField, Range(1, 50)] float detectionDistance = 20;
    [SerializeField]
    Controls controls = null;
    InputAction mousePositionInput = null;
    InputAction selectionInput = null;
    InputAction rotateInput = null;

    public Vector3 CursorLocation => GetMousePosition();
    public InputAction SelectionInput => selectionInput;
    public InputAction RotateInput => rotateInput;
    public bool IsValid => gameCamera;

    protected override void Awake()
    {
        base.Awake();
        controls = new Controls();
    }

    // Update is called once per frame
    void Update()
    {
        Interact(editableSurfaceLayer, OnEditableSurface, detectionDistance);
        if (selectionInput.triggered)
            InteractWithComponent(itemLayer, OnSelection, detectionDistance);
    }

    private void OnEnable()
    {
        mousePositionInput = controls.Cursor.MousePosition;
        mousePositionInput.Enable();

        selectionInput = controls.Cursor.Select;
        selectionInput.Enable();

        rotateInput = controls.Cursor.Rotate;
        rotateInput.Enable();
    }

    void Interact(LayerMask _validMask, Action<Vector3> _callback, float _distance = 20)
    {
        if (!IsValid) return;
        Ray _ray = gameCamera.ScreenPointToRay(this.CursorLocation); // Recup le rayon entre le curseur et la cam ScreenPointToRay(_cursor.CursorLocation);
        bool _hit = Physics.Raycast(_ray, out RaycastHit _hitRay, _distance, _validMask);
        Debug.DrawRay(_ray.origin, _ray.direction * _distance, _hit ? Color.green : Color.red);
        if (!_hit) return;
        _callback?.Invoke(_hitRay.point);
    }

    T InteractWithComponent<T>(LayerMask _validMask, Action<T> _callback, float _distance = 20) where T : MonoBehaviour // l'event envoyé doit au minimum
    {                                                                                                                   // prendre un monobehaviour en param
        if (!IsValid) return null;
        Ray _ray = gameCamera.ScreenPointToRay(this.CursorLocation); // recup le rayon entre le curseur et la caméra
        bool _hit = Physics.Raycast(_ray, out RaycastHit _hitRay, _distance, _validMask); // fait le raycast pour detect le layer en param
        if (!_hit) return null;
        T _get = _hitRay.transform.GetComponent<T>(); // si objet avec layer touché : essaye de récupérer le compo en rapport avec le param
        _callback?.Invoke(_get); // trigger l'event en passant le compo en param
        return _get;
    }

    Vector3 GetMousePosition()
    {
        Vector2 _mousePos = mousePositionInput.ReadValue<Vector2>();
        return new Vector3(_mousePos.x, _mousePos.y, 0);
    }
}