using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpinAndScale : MonoBehaviour
{
	[SerializeField] private float spinDuration;

	// Start is called before the first frame update
	void Start()
    {
		Vector3 rot = new Vector3(0, 0, 720);

		Sequence fall = DOTween.Sequence();
		fall.Append(transform.DORotate(rot, spinDuration/2, RotateMode.FastBeyond360).
			SetLoops(2, LoopType.Incremental).
			SetEase(Ease.Linear));
		fall.Insert(0, transform.DOScale(1, spinDuration));

	}
}
