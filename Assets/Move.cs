using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
	[SerializeField] private float moveDuration;

    // Start is called before the first frame update
    void Start()
    {
		transform.DOLocalMoveY(0, moveDuration);
    }
}
