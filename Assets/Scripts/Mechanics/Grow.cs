using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grow : MonoBehaviour
{
	[SerializeField] private float scaleAmount;
	[SerializeField] private float scaleDuration;

	private float scaleDesired = 1;
	private bool growing = true;

	private void OnMouseDown()
	{
		growing = true;
	}

	private void OnMouseUp()
	{
		growing = false;
	}

	private void Update()
	{
		if (growing) GrowSquare();
	}

	public void GrowSquare()
	{
		if (Input.GetMouseButton(0))
		{
			scaleDesired += scaleAmount;
			transform.DOScale(scaleDesired, scaleDuration);
		}
	}
}
