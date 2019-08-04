using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
	[SerializeField] private float angle;
	[SerializeField] private float colorChangeSpeed;

	private Camera[] cams;
	private bool rotate = true;

	private void Awake()
	{
		cams = FindObjectsOfType<Camera>();
	}

	private void OnMouseDrag()
	{
		if (rotate) Rotate();
	}

	private void Rotate()
	{
		if (Input.GetAxisRaw("Mouse Y") > .1f)
		{
			transform.Rotate(angle * Time.deltaTime * Input.GetAxisRaw("Mouse Y"), 0, 0);
			if (transform.eulerAngles.x >= 85)
			{
				transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, 90, angle * Time.deltaTime),
					0,0);
				rotate = false;
			}
			ChangeBackgroundColor();
		}
	}

	private void ChangeBackgroundColor()
	{
		foreach(Camera c in cams)
			if(c.backgroundColor != Color.white)
				c.backgroundColor = 
					Color.Lerp(Color.black, Color.white, 
					transform.eulerAngles.x / 90);
	}
}
