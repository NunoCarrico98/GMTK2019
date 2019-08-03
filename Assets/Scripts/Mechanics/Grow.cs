using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grow : MonoBehaviour
{
	[SerializeField] private float scaleAmount;
	[SerializeField] private float scaleDuration;

	private float scaleDesired = 1;

	private void OnMouseDown()
	{
		GrowSquare();
	}

	public void GrowSquare()
	{
		if (Input.GetMouseButtonDown(0))
		{
			scaleDesired += scaleAmount;
			transform.DOScale(scaleDesired, scaleDuration);
		}
	}
}
