using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField, ReadOnly] Controls controls = null;

    InputAction mousePositionInput = null;
    InputAction selectionInput = null;
    InputAction rotateItemInput = null;
    InputAction rotateCameraInput = null;

    public InputAction MousePositionInput => mousePositionInput;
    public InputAction SelectionInput => selectionInput;
    public InputAction RotateItemInput => rotateItemInput;
    public InputAction RotateCameraInput => rotateCameraInput;

    protected override void Awake()
    {
        base.Awake();
        controls = new();
    }

    private void OnEnable()
    {
        mousePositionInput = controls.Cursor.MousePosition;
        mousePositionInput.Enable();

        selectionInput = controls.Cursor.Select;
        selectionInput.Enable();

        rotateItemInput = controls.Cursor.RotateItem;
        rotateItemInput.Enable();

        rotateCameraInput = controls.GameCamera.RotateCamera;
        rotateCameraInput.Enable();
    }
}
