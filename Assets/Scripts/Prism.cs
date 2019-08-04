using System;
using UnityEngine;
using UnityEngine.Events;

public class Prism : MonoBehaviour
{
    public Transform cameraRotationAxis;
    public Vector3 cameraFinalRotation;
    private Quaternion cameraOriginalRotation;
    public float rotationSpeed;

    int stage = 0;
    bool rotatingCamera = false;

    private Camera cam;

    private float rotationPercentage = 0;
    private float smoothDampVelocity;
    public float smoothDampTime = 0.5f;
    private float smoothRotation = 0;

    private bool rotatingCube = false;
    private float rotationPercentageCube = 0;
    private float smoothDampVelocityCube;
    public float smoothDampTimeCube = 0.5f;
    private float smoothRotationCube = 0;

    public Transform rotatingPartCubeAxis;
    public Transform finalCubeParent;
    public Vector3 cubeFinalRotation;
    private Quaternion cubeOriginalRotation;
    private Vector3 cameraOriginalPosition;
    public Vector3 cameraFinalPosition;
    public Vector3 cubeFinalScale;

    public UnityEvent onFinishRotateCube;
    public UnityEvent onStartBreakCube;
    public UnityEvent onFinishBreakCube;

    private void Start()
    {
        cameraOriginalRotation = cameraRotationAxis.rotation;
        cubeOriginalRotation = rotatingPartCubeAxis.localRotation;
        cam = FindObjectOfType<Camera>();
        cameraOriginalPosition = cam.transform.localPosition;
    }

    private void Update()
    {
        switch (stage)
        {
            case 0:
                RotateCamera();
                break;
            case 1:
                BreakCube();
                break;
        }
    }


    private void RotateCamera()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            rotatingCamera = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            rotatingCamera = false;
        }

        if(rotatingCamera)
        {
            rotationPercentage += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            rotationPercentage = Mathf.Clamp01(rotationPercentage);
        }
        smoothRotation = Mathf.SmoothDamp(smoothRotation, rotationPercentage, ref smoothDampVelocity, smoothDampTime);
        var rotation = Quaternion.Lerp(cameraOriginalRotation, Quaternion.Euler(cameraFinalRotation), smoothRotation);

        cameraRotationAxis.rotation = rotation;

        if (Mathf.Abs(1 - smoothRotation) < 0.01)
        {
            stage++;
            onFinishRotateCube.Invoke();
        }
    }

    private void BreakCube()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rotatingCube = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            rotatingCube = false;
        }

        if (rotatingCube)
        {
            rotationPercentageCube += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotationPercentageCube = Mathf.Clamp01(rotationPercentageCube);
        }

        bool initial = smoothRotationCube == 0;

        smoothRotationCube = Mathf.SmoothDamp(smoothRotationCube, rotationPercentageCube, ref smoothDampVelocityCube, smoothDampTimeCube);
        var rotation = Quaternion.Lerp(cubeOriginalRotation, Quaternion.Euler(cubeFinalRotation), smoothRotationCube);
        var position = Vector3.Lerp(cameraOriginalPosition, cameraFinalPosition, smoothRotationCube);
        var scale = Vector3.Lerp(Vector3.one, cubeFinalScale, smoothRotationCube);

        rotatingPartCubeAxis.localRotation = rotation;
        cam.transform.localPosition = position;
        finalCubeParent.transform.localScale = scale;

        if (initial && smoothRotationCube != 0)
            onStartBreakCube.Invoke();

        if (Mathf.Abs(1 - smoothRotationCube) < 0.01)
            FinishStage();
    }

    private void FinishStage()
    {
        onFinishBreakCube.Invoke();
    }
}
