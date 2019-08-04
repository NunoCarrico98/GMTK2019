using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Space : MonoBehaviour
{
	[SerializeField] private float scaleDesired;
	[SerializeField] private float scaleDuration;

    // Start is called before the first frame update
    void Start()
    {
		transform.DOScale(scaleDesired, scaleDuration);
    }
}
