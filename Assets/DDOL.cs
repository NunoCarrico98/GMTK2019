using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DDOL : MonoBehaviour
{
	[SerializeField] private float fadeDuration;
	[SerializeField] private AudioClip music;

	private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);

		source = GetComponent<AudioSource>();
    }

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private IEnumerator ChangeSound()
	{
		yield return new WaitForSeconds(fadeDuration);
		source.clip = music;
		source.Play();
		source.DOFade(1, fadeDuration);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if(scene.name == "Level_Space")
		{
			source.DOFade(0, fadeDuration);
			StartCoroutine(ChangeSound());
		}
	}
}
