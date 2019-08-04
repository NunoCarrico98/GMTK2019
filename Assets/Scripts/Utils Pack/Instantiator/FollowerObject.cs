using System.Collections;
using UnityEngine;

/// <summary>
/// This component is meant to be used with FollowerInstantiator scriptable to follow a target
/// </summary>
public class FollowerObject : MonoBehaviour
{
    public virtual Transform Target { get; set; }

    private void OnEnable()
    {
        StartCoroutine(Follow());
    }

    IEnumerator Follow()
    {
        while (true)
        {
            if(Target)
                transform.position = Target.position;

            yield return null;
        }
    }
}