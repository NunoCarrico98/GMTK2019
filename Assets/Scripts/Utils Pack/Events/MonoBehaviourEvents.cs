using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEvents : MonoBehaviour
{
    [SerializeField] private bool awake;
    [SerializeField] private bool start;
    [SerializeField] private bool enable;
    [SerializeField] private bool disable;
    [SerializeField] private bool update;
    [SerializeField] private bool lateUpdate;
    [SerializeField] private bool fixedUpdate;
    [SerializeField] private bool destroy;

    public UnityEvent onAwake;
    public UnityEvent onStart;
    public UnityEvent onEnable;
    public UnityEvent onDisable;
    public UnityEvent onUpdate;
    public UnityEvent onLateUpdate;
    public UnityEvent onFixedUpdate;
    public UnityEvent onDestroy;

    private void Awake()
    {
        if (awake)
            onAwake.Invoke();
    }

    private void Start()
    {
        if (start)
            onStart.Invoke();
    }

    private void OnEnable()
    {
        if (enable)
            onEnable.Invoke();
    }

    private void OnDisable()
    {
        if (disable)
            onDisable.Invoke();
    }

    private void Update()
    {
        if (update)
            onUpdate.Invoke();
    }

    private void LateUpdate()
    {
        if (lateUpdate)
            onLateUpdate.Invoke();
    }

    private void FixedUpdate()
    {
        if (fixedUpdate)
            onFixedUpdate.Invoke();
    }

    private void OnDestroy()
    {
        if (destroy)
            onDestroy.Invoke();
    }
}
