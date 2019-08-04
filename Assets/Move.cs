using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
	[SerializeField] private float moveDuration;
	[SerializeField] private float speed;
	[SerializeField] private AudioSource source;
	[SerializeField] private AudioClip carIdle;

	private bool move = true;

	private void Update()
	{
		MoveSquare();
	}

	private void MoveSquare()
	{
		if (move)
		{
			transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
			if (transform.position.y <= 0)
			{
				move = false;
				transform.position = new Vector3(0,0,0);
				transform.DOPunchPosition(new Vector3(0,1,0), 0.3f, 10, 0f);
				source.Play();
				StartCoroutine(PlayCarIdle());
			}
		}
	}

	private IEnumerator PlayCarIdle()
	{
		yield return new WaitForSeconds(1f);

		source.clip = carIdle;
		source.loop = true;
		source.Play();
	}
}
