using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fall : MonoBehaviour
{
	[SerializeField] private float fallDuration;

	private void OnMouseUp()
	{
		EnableDisableCursor();

		Vector3 rot = new Vector3(0, 0, 360);
		Vector3 rot2 = new Vector3(0, 0, 0);

		Sequence fall = DOTween.Sequence();
		fall.Append(transform.DORotate(rot, 1.5f, RotateMode.FastBeyond360).
			SetLoops(4, LoopType.Incremental).
			SetEase(Ease.Linear));
		fall.Insert(0, transform.DOScale(0, fallDuration))
			.Append(transform.DOMove(Vector2.zero, 0))
			.Append(transform.DORotate(rot2, 0))
			.Append(transform.DOScale(12, .5f))
			.SetAutoKill(true);
	}

	private void EnableDisableCursor()
	{
		Cursor.visible = !Cursor.visible;
	}
}
