using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBlack : MonoBehaviour
{
	[SerializeField] private GameObject black;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			black.SetActive(true);
		}
	}
}
