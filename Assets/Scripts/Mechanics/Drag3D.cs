using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drag3D : MonoBehaviour
{
	private Camera cam;
	private Vector3 mOffset;
	private float mZCoord;
    public UnityEvent onStartDrag;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void OnMouseDown()
	{
		mZCoord = cam.WorldToScreenPoint(transform.position).z;
		mOffset = transform.position - GetMouseWorldPos();
        onStartDrag.Invoke();
	}

	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = mZCoord;

		return cam.ScreenToWorldPoint(mousePoint);
	}

	private void OnMouseDrag()
	{
		transform.position = GetMouseWorldPos() + mOffset;
	}
}
