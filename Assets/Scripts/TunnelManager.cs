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

	[SerializeField] private AudioSource source;
	[SerializeField] private AudioClip countdown;
	[SerializeField] private AudioClip launch;

	private Camera[] cams;
	private List<GameObject> squares;
	private bool playCountdown = true;
	private bool playLaunch = true;

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
			PlaySoundCountdown();
			yield return new WaitForSeconds(waitTime);
			PlaySoundLaunch();
			foreach (Camera c in cams)
			{
				c.orthographicSize -= decreaseValue;
				if (c.orthographicSize <= desiredSize) c.orthographicSize = desiredSize;
			}
		}
	}

	private void PlaySoundCountdown()
	{
		if (playCountdown)
		{
			playCountdown = false;
			source.clip = countdown;
			source.Play();
		}
	}

	private void PlaySoundLaunch()
	{
		if (playLaunch)
		{
			playLaunch = false;
			source.clip = launch;
			source.Play();
		}
	}
}
