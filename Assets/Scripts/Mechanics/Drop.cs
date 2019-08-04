using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
	[SerializeField] private float gravity;
	[SerializeField] private DragAndRotate dragAndRot;

	private bool drop = false;

	private void Update()
	{
		if (drop) transform.position -= new Vector3(0, gravity, 0);
	}

	private void OnMouseUp()
	{
		drop = true;
		StartCoroutine(AbleToRotate());
	}

	private IEnumerator AbleToRotate()
	{
		yield return new WaitForSeconds(.5f);
		dragAndRot.RotateObject = true;
	}
}
