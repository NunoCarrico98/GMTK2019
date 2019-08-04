using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/SFX/SFX Reference")]
public class SFXReference : ScriptableObject
{
    public Audio audio;

    public void Play(Transform target)
    {
        SFX.PlayClip(audio, target);
    }

    public void Play()
    {
        SFX.PlayClip(audio);
    }
}
