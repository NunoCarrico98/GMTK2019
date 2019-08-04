using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{
	[SerializeField] private int maxSquares;

	[Header("After Complete Tunnel")]
	[SerializeField] private float waitTime;
	[SerializeField] private float desiredSize;
	[SerializeField] private float decreaseValue;

	private Camera[] cams;
	private List<GameObject> squares;

	public int MaxSquares => maxSquares;

	private void Awake()
	{
		squares = new List<GameObject>();
		cams = FindObjectsOfType<Camera>();
	}

	private void Update()
	{
		StartCoroutine(FinishLevel());
	}

	public void AddToList(GameObject square)
	{
		squares.Add(square);
	}

	public GameObject GetLastElement()
	{
		return squares[squares.Count - 1];
	}

	public IEnumerator FinishLevel()
	{
		if (squares.Count >= MaxSquares - 1)
		{
			yield return new WaitForSeconds(waitTime);
			foreach (Camera c in cams)
			{
				c.orthographicSize -= decreaseValue;
				if (c.orthographicSize <= desiredSize) c.orthographicSize = desiredSize;
			}
		}
	}
}
