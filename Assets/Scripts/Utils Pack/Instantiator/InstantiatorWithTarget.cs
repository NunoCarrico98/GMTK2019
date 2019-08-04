using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Instantiator/Follower Instantiator")]
public class InstantiatorWithTarget : ScriptableObject
{
    [SerializeField] private FollowerObject trail;
    [SerializeField] private bool applyRotation = false;

#if UNITY_EDITOR
    [SerializeField, TextArea]
    private string DeveloperDescirption =
        "This can instantiate a follower object and automatically set the target using Instantiate function, " +
        "good to use with EventTransform from utils pack";
#endif

    public void Instantiate(Transform target)
    {
        var trail = Instantiate(
            this.trail, target.position,
            applyRotation ? target.rotation : Quaternion.identity)
            .GetComponent<FollowerObject>();
        trail.Target = target;
    }
}
