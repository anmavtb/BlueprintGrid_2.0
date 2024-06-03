using System.Collections;
using UnityEngine;

public class GameCamera : Singleton<GameCamera>
{
    [SerializeField] Transform target = null;
    [SerializeField] float rotateSpeed = 50;
    [SerializeField, ReadOnly] bool isTopDownView = false;
    [SerializeField] float transitionDuration = 1;
    [SerializeField, ReadOnly] Vector3 originalPositionOffset = new Vector3(0, 10, 4);
    [SerializeField, ReadOnly] Vector3 topDownPositionOffset = new Vector3(0, 10, 0);
    [SerializeField, ReadOnly] Vector3 originalRotationAngles = new Vector3(70, 0, 0);
    Vector3 initialPosition = Vector3.zero;
    Quaternion initialRotation = Quaternion.identity;

    public bool IsTopDownView => isTopDownView;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    public void Rotate(float _rotValue)
    {
        transform.RotateAround(target.position, Vector3.up, -_rotValue * Time.deltaTime * rotateSpeed);
    }

    void RotateCamera()
    {
        float _rotValue = InputManager.Instance.RotateCameraInput.ReadValue<float>();
        Rotate(_rotValue);
    }

    public void ChangeView()
    {
        if (!isTopDownView)
            SwitchToTopDownView();
        else
            SwitchToOriginalView();
        isTopDownView = !isTopDownView;
    }

    public void SwitchToTopDownView()
    {
        Vector3 topDownPosition = target.position + topDownPositionOffset;
        Quaternion topDownRotation = Quaternion.Euler(90, 180, 0);
        StartCoroutine(SwitchCameraView(topDownPosition, topDownRotation));
    }

    public void SwitchToOriginalView()
    {
        Vector3 originalPosition = target.position + originalPositionOffset;
        Quaternion originalRotation = Quaternion.Euler(originalRotationAngles);
        StartCoroutine(SwitchCameraView(originalPosition, originalRotation));
    }

    IEnumerator SwitchCameraView(Vector3 _targetPosition, Quaternion _targetRotation)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            transform.position = Vector3.Lerp(startPosition, _targetPosition, t);
            transform.rotation = Quaternion.Lerp(startRotation, _targetRotation, t);

            yield return null;
        }

        transform.position = _targetPosition;
        transform.rotation = _targetRotation;
    }
}