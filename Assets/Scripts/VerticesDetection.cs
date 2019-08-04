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

    private bool finished = false;

    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        cam = FindObjectOfType<Camera>();
        mesh = filter.mesh;
        currentVertices = mesh.vertices;
    }

    private void Update()
    {
        if (finished)
            return;

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

            if (Input.GetKeyUp(KeyCode.Mouse0))
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

        bool[] winConditions = new bool[4];
        for (int i = 0; i < smoothPositions.Count; i++)
        {
            Vector3 v = cam.WorldToViewportPoint(smoothPositions[i]);

            if (v.x > 0.99 && v.y > 0.99)
                winConditions[0] = true;
            else if (v.x < 0.01 && v.y > 0.99)
                winConditions[1] = true;
            else if (v.x > 0.99 && v.y < 0.01)
                winConditions[2] = true;
            else if (v.x < 0.01 && v.y < 0.01)
                winConditions[3] = true;
        }

        bool matchAll = true;
        for (int i = 0; i < winConditions.Length; i++)
        {
            if (!winConditions[i])
            {
                matchAll = false;
                break;
            }
        }

        if (matchAll)
            Won();
    }


    private void Won()
    {
        finished = true;
        onWin.Invoke();
        print("YOU WIN :D");
    }

    private void OnDrawGizmos()
    {
        if(closerVertice != -1)
            Gizmos.DrawSphere(GetScaledVertices()[closerVertice], 0.2f);
    }

    private Vector3[] GetScaledVertices()
    {
        Vector3[] scaled = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            scaled[i] = Vector3.Scale(filter.transform.localScale, mesh.vertices[i]);
        }

        return scaled;
    }

    private int GetClosestFromCursor(Vector3[] vertices, float maxDistance)
    {
        
        int closer = -1;
        Vector3 hitPoint = GetMouseOnPlane();

        float closerDistance = float.MaxValue;
        for (int i = 0; i < vertices.Length; i++)
        {
            var distance = Vector2.Distance(hitPoint, vertices[i]);

            if (distance <= maxDistance && distance < closerDistance)
            {
                closer = i;
                closerDistance = distance;
            }
        }

        return closer;
    }

    private Vector3 GetMouseOnPlane()
    {
        Plane plane = new Plane(mesh.vertices[0], mesh.vertices[1], mesh.vertices[2]);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float dist;
        int closer = -1;
        plane.Raycast(ray, out dist);
        return ray.origin + ray.direction * dist;
    }
}
