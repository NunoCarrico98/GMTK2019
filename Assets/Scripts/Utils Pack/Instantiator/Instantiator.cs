using UnityEngine;

[CreateAssetMenu(menuName = "Game/Instantiator/Position Instantiator")]
public class Instantiator : ScriptableObject
{
    [SerializeField] private GameObject[] objectsToInstantiate;
    [Tooltip("Apply rotation only work with transform reference")]
    [SerializeField] private bool applyRotation = false;

#if UNITY_EDITOR
    [SerializeField, TextArea] private string DeveloperDescirption = 
        "This can instantiate objects using Instantiate function in a position, " +
        "good to use on events which require a Vector3 or a Transform as parameter \n" +
        "(Apply rotation only work with transform reference)";
#endif

    public void Instantiate(Vector3 position)
    {
        foreach (var item in objectsToInstantiate)
        {
            Instantiate(item, position, Quaternion.identity);
        }
    }

    public void Instantiate(Transform transform)
    {
        Quaternion rotation = applyRotation ? transform.rotation : Quaternion.identity;

        foreach (var item in objectsToInstantiate)
        {
            Instantiate(item, transform.position, rotation);
        }
    }
}
