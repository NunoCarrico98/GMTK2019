using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
	[SerializeField] private GameObject square;

	private Camera cam;

	private void OnMouseDrag()
	{
		if (Input.GetMouseButton(0))
		{
			Instantiate(square, gameObject.transform.position, gameObject.transform.rotation);
		}
	}
}
