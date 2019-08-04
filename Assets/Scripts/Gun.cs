using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{

    Camera cam;
    private bool isDragging = false;
    Vector3 initialDragPos;
    [Range(0, 2)]public float movementFactor = 0.3f;
    public float releaseTime = 0.2f;
    public float maxDistance = 100;
    Vector3 originalPosition;
    private bool fullPull = false;
    private bool loaded = false;
    public Color onWinBackgroundColor = Color.black;
    public float delayToChangeBackgroundColor = 0.1f;

    private Vector3 smoothDampVelocity;
    public float smoothSpeed = 0.1f;

    public UnityEvent onAchieveMaxPull;
    public UnityEvent onLoadGun;
    public UnityEvent onReleaseLoaded;
    public UnityEvent onShoot;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if(isDragging)
        {
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                isDragging = false;
                Release();
            }

            Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
            Vector3 mousePos = Input.mousePosition;

            Vector3 difference = mousePos - initialDragPos;
            difference *= movementFactor;

            Vector3 newPos = cam.WorldToScreenPoint(originalPosition) + difference;
            newPos.y = screenPos.y;

            newPos = cam.ScreenToWorldPoint(newPos);
            newPos.x = Mathf.Clamp(newPos.x, originalPosition.x - maxDistance, originalPosition.x);
            newPos.z = 0;
            newPos = Vector3.SmoothDamp(transform.position, newPos, ref smoothDampVelocity, smoothSpeed);
            transform.position = newPos;

            bool firstFullPull = fullPull == false;
            fullPull = Mathf.Abs(newPos.x - (originalPosition.x - maxDistance)) < 0.01f;

            if (fullPull && firstFullPull)
                onAchieveMaxPull.Invoke();
        }

        if (loaded && Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void OnMouseDown()
    {
        initialDragPos = Input.mousePosition;
        isDragging = true;
    }

    private void Release()
    {
        if (fullPull)
        {
            loaded = true;
            onLoadGun.Invoke();
        }
        StartCoroutine(ReleaseRoutine());
    }

    private IEnumerator ReleaseRoutine()
    {
        float delta = 0;
        Vector3 startPos = transform.position;

        while (delta < 1)
        {
            Vector3 pos = Vector3.Lerp(startPos, originalPosition, delta);
            transform.position = pos;
            delta += 1 / (releaseTime / Time.deltaTime);
            yield return null;
        }

        transform.position = originalPosition;

        yield return new WaitForSeconds(delayToChangeBackgroundColor);

        OnFinishRelease();
    }

    private void OnFinishRelease()
    {
        if (fullPull)
        {
            onReleaseLoaded.Invoke();
        }
    }

    private void Shoot()
    {
        cam.backgroundColor = onWinBackgroundColor;
        onShoot.Invoke();
    }
}
