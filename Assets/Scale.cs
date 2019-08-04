using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
	[SerializeField] private float scaleDuration;
	private bool scale = true;

	private void OnMouseDown()
	{
		if (scale)
		{
			scale = false;
			transform.DOScale(5, scaleDuration);
		}
	}
}
