using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tunnel : MonoBehaviour
{
	[SerializeField] private float scaleDuration;

	[SerializeField] private Material[] materials;
	[SerializeField] private float scaleFactor;
	[SerializeField] private float rotationAmount;

	[Header("After Complete Tunnel")]
	[SerializeField] private float desiredScale;
	[SerializeField] private float initialScaleDuration;
	[SerializeField] private float fastScaleDuration;

	private TunnelManager tunnelManager;
	private bool doOnce = false;
	private bool scale = true;

	public int MatIndex { get; set; } = 1;
	public int OrderInLayer { get; set; } = 1;
	public int SpawnedSquares { get; set; } = 1;
	public bool CanClick { get; set; } = true;

	private void Awake()
	{
		tunnelManager = FindObjectOfType<TunnelManager>();
	}

	private void Update()
	{
		SpawnSquare();
	}

	private void OnMouseDown()
	{
		if (scale)
		{
			scale = false;
			transform.DOScale(5, scaleDuration);
		}
	}

	private void SpawnSquare()
	{
		if (Input.GetMouseButtonDown(0) &&
			CanClick && !scale &&
			SpawnedSquares != tunnelManager.MaxSquares)
		{
			CanClick = false;

			GameObject squareInst = Instantiate(gameObject, transform.position, transform.rotation);
			squareInst.transform.localScale = transform.localScale;
			squareInst.GetComponent<SpriteRenderer>().material = materials[MatIndex];
			squareInst.GetComponent<SpriteRenderer>().sortingOrder = OrderInLayer;
			squareInst.transform.DOScale(transform.localScale.x * scaleFactor, .2f);
			Vector3 newRot = new Vector3(0, 0, squareInst.transform.eulerAngles.z + rotationAmount);
			squareInst.transform.DORotate(newRot, .2f);

			UpdateMaterialIndex();
			OrderInLayer++;
			SpawnedSquares++;

			squareInst.GetComponent<Tunnel>().MatIndex = MatIndex;
			squareInst.GetComponent<Tunnel>().OrderInLayer = OrderInLayer;
			squareInst.GetComponent<Tunnel>().SpawnedSquares = SpawnedSquares;

			tunnelManager.AddToList(squareInst);
		}
	}

	private void UpdateMaterialIndex() => MatIndex = MatIndex == 1 ? 0 : 1;
}
