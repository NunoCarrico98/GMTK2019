using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
	private Camera cam;
	private Collider2D col;

	private float startPosX;
	private float startPosY;

	private void Awake()
	{
		cam = Camera.main;
		col = GetComponent<Collider2D>();
	}

	private void OnMouseDown()
	{
		col.enabled = false;
	}

	private void OnMouseDrag()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = cam.ScreenToWorldPoint(mousePos);

			startPosX = mousePos.x - transform.position.x;
			startPosX = mousePos.y - transform.position.y;

			transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
		}
	}

	private void OnMouseUp()
	{
		col.enabled = true;
	}
}
