using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grow : MonoBehaviour
{
	[SerializeField] private float scaleAmount;
	[SerializeField] private float scaleDuration;
	[SerializeField] private AudioSource source;

	private float scaleDesired = 1;
	private bool growing = true;
	private bool play = true;

	private void OnMouseDown()
	{
		if (play)
		{
			play = false;
			source.Play();
		}
		growing = true;
	}

	private void OnMouseUp()
	{
		if (!play)
		{
			play = true;
			source.Pause();
		}
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
