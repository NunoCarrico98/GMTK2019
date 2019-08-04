using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
	[SerializeField] private Transform square;
	[SerializeField] private float scaleDuration;
	[SerializeField] private bool scale = false;

	private Tunnel tunnel;

	private void Awake()
	{
		tunnel = square.GetComponent<Tunnel>();
	}

	private void Start()
	{
		tunnel.enabled = false;
		square.DOScale(5, scaleDuration);
	}

	private void Update()
	{
		if (square.localScale.z == 5) tunnel.enabled = true;
	}

}
