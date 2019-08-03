using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void OnMouseDown()
	{
		anim.SetTrigger("Play");
	}

	private void LoadNewScene() => 
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
