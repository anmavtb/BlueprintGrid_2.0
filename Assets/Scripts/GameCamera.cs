using UnityEngine;

public class GameCamera : Singleton<GameCamera>
{
    [SerializeField] Transform targetPoint = null;
    [SerializeField] float rotateSpeed = 50;

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    public void Rotate(float _rotValue)
    {
        transform.RotateAround(targetPoint.position, Vector3.up, -_rotValue * Time.deltaTime * rotateSpeed);
    }

    void RotateCamera()
    {
        float _rotValue = InputManager.Instance.RotateCameraInput.ReadValue<float>();
        Rotate(_rotValue);
        //transform.RotateAround(targetPoint.position, Vector3.up, -_rotValue * Time.deltaTime * rotateSpeed);
    }
}