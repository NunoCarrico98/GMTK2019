using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
	[SerializeField] private float moveDuration;
	[SerializeField] private float speed;

	private bool move = true;

    // Start is called before the first frame update
    void Start()
    {
		//transform.DOLocalMoveY(0, moveDuration);
    }

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
			}
		}
	}
}
