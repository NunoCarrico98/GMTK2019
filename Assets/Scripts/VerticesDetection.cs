using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VerticesDetection : MonoBehaviour
{
    public float distanceToGrab = 20;

    private MeshFilter filter;
    private Camera cam;
    private Mesh mesh;
    private Vector3[] currentVertices;

    private bool isDragging = false;
    private int closerVertice = -1;

    public float smoothDampTime = 1;
    private Vector3[] smoothDampVelocities = new Vector3[4];

    public UnityEvent onWin;

    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        cam = FindObjectOfType<Camera>();
        mesh = filter.mesh;
        currentVertices = mesh.vertices;
    }

    private void Update()
    {
        if (!isDragging)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                closerVertice = GetClosestFromCursor(currentVertices, distanceToGrab);

                if (closerVertice != -1)
                {
                    isDragging = true;
                }
            }
        }
        else
        {
            currentVertices[closerVertice] = cam.ScreenToWorldPoint(Input.mousePosition);

            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                closerVertice = -1;
                isDragging = false;
            }
        }

        List<Vector3> smoothPositions = new List<Vector3>(mesh.vertices);
        for (int i = 0; i < smoothPositions.Count; i++)
        {
            Vector3 newPos = Vector3.SmoothDamp(smoothPositions[i], currentVertices[i], ref smoothDampVelocities[i], smoothDampTime);
            newPos.z = 0;
            smoothPositions[i] = newPos;
        }

        mesh.SetVertices(smoothPositions);

        bool win = true;
        for (int i = 0; i < smoothPositions.Count; i++)
        {
            Vector3 v = cam.WorldToViewportPoint(smoothPositions[i]);
            if (cam.rect.Contains(v))
            {
                win = false;
                break;
            }
        }

        if (win)
            Won();
    }


    private void Won()
    {
        onWin.Invoke();
        print("YOU WIN :D");
    }

    private int GetClosestFromCursor(Vector3[] vertices, float maxDistance)
    {
        Vector2 mousePos = Input.mousePosition;

        var debug = "mouse: " + mousePos;

        int closerVertice = -1;
        float closerDistance = float.MaxValue;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 vPos = cam.WorldToScreenPoint(vertices[i]);
            var distance = Vector2.Distance(vPos, mousePos);

            if (distance <= maxDistance && distance < closerDistance)
            {
                closerVertice = i;
                closerDistance = distance;
                break;
            }
        }

        return closerVertice;
    }
}
