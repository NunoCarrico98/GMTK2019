using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomExtension;

public class SFX : MonoBehaviour
{
    private static SFX Instance;
    private static float fadeOutTime = 2.5f;

    [SerializeField] private static List<AudioSource> backgroundMusic = new List<AudioSource>();

    public static void PlayClip(Audio audio, Transform target = null)
    {
        if (!Instance)
            CreateInstance();

        AudioSource source;

        if (!target)
            source = Instance.gameObject.AddComponent<AudioSource>();
        else
            source = target.gameObject.AddComponent<AudioSource>();

        source.clip = audio.clip;
        source.volume = audio.volume.GetRandom();
        source.pitch = audio.pitch.GetRandom();
        source.loop = audio.backgroundMusic;
        if(!audio.backgroundMusic)
        {
            source.spatialBlend = audio.sound3D;
            source.rolloffMode = audio.rolloffMode;
            source.maxDistance = audio.maxDistance;
            if(audio.rolloffMode == AudioRolloffMode.Custom)
            {
                source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, audio.customRolloffCurve);
            }
            
        }
        
        if(audio.backgroundMusic)
        {
            BackgroundFadeOut();
            backgroundMusic.Add(source);
            FadeInBackground(source, source.volume, audio.backgroundMusicFadeInTime);
        }
        else
        {
            Instance.StartCoroutine(PlayAndWaitToDestroy(source, false));
        }
    }

    private static IEnumerator PlayAndWaitToDestroy(AudioSource source, bool destroyTarget)
    {
        source.Play();
        yield return new WaitUntil(() => source.isPlaying == false);

        if (destroyTarget)
            Destroy(source.gameObject);
        else
            Destroy(source);
    }

    private static void BackgroundFadeOut()
    {
        if (backgroundMusic.Count == 0)
            return;

        Instance.StartCoroutine(FadeOutRoutine());
    }

    static IEnumerator FadeOutRoutine()
    {
        List<AudioSource> sources = new List<AudioSource>(backgroundMusic.ToArray());
        List<float> volumes = new List<float>(sources.Select(x => x.volume));

        float delta = 1;
        while (true)
        {
            for (int i = 0; i < sources.Count; i++)
            {
                var vol = Mathf.Lerp(0, volumes[i], delta);
                sources[i].volume = vol;
            }

            if (delta == 0)
                break;

            yield return null;
            delta -= Time.deltaTime / fadeOutTime;
            delta = Mathf.Clamp01(delta);
        }

        for (int i = 0; i < sources.Count; i++)
        {
            var s = sources[i];
            backgroundMusic.Remove(s);
            Destroy(s);
        }
    }

    private static void FadeInBackground(AudioSource source, float volume, float time)
    {
        Instance.StartCoroutine(FadeInRoutine(source, volume, time));
    }

    static IEnumerator FadeInRoutine(AudioSource source, float volume, float fadeInTime)
    {
        source.volume = 0;
        source.Play();
        float delta = 0;
        while (true)
        {
            var vol = Mathf.Lerp(0, volume, delta);
            source.volume = vol;

            if (delta == 1)
                break;

            yield return null;
            delta += Time.deltaTime / fadeInTime;
            delta = Mathf.Clamp01(delta);
        }
    }

    private static void CreateInstance()
    {
        var i = new GameObject(name: "SFX Manager").AddComponent<SFX>();
        DontDestroyOnLoad(i.gameObject);
        Instance = i;
    }

    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
    }
}

[Serializable]
public class Audio
{
    public AudioClip clip;
    [MinMaxRange(0,1)] public RangedFloat volume;
    [MinMaxRange(-3,3)]public RangedFloat pitch;
    public bool backgroundMusic = false;
    public float backgroundMusicFadeInTime = 5f;
    [Space(10)]
    public float sound3D = 1;
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
    public float maxDistance = 55;
    public AnimationCurve customRolloffCurve;

    public void Play()
    {
        SFX.PlayClip(this);
    }
}
