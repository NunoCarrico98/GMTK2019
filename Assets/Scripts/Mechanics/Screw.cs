using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Screw : MonoBehaviour
{
	[SerializeField] private float scaleAmount;
	[SerializeField] private float scaleDuration;

	private Camera cam;
	private float scaleDesired = 1;
	private Vector2 dir;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void OnMouseDrag()
	{
		RotateSquare();
	}

	private void RotateSquare()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = cam.ScreenToWorldPoint(mousePos);


			Vector2 direction = mousePos - transform.position;
			dir = direction;

			float previousRot = transform.rotation.z;
			transform.up = direction;

			ScaleSquare(previousRot);
		}
	}

	private void ScaleSquare(float previousRot)
	{
		float newRot = transform.rotation.z;

		if (newRot == previousRot)
			return;
		else if (newRot > previousRot)
		{
			scaleDesired += scaleAmount;
			transform.DOScale(scaleDesired, scaleDuration);
		}
		else
		{
			if (transform.localScale.x >= 1)
			{
				scaleDesired -= scaleAmount;
				transform.DOScale(scaleDesired, scaleDuration);
			}
		}
	}
}
