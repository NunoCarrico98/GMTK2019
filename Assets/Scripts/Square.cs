using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Square : MonoBehaviour
{
	[SerializeField] float initialSize;
	[SerializeField] float popUpAnimTime;
	[SerializeField] bool playSpawnAnim = false;

    // Start is called before the first frame update
    void Start()
    {
		if (playSpawnAnim)
		{
			transform.localScale = Vector3.zero;
			transform.DOScale(initialSize, popUpAnimTime);
		}
    }
}
