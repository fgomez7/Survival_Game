using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalClock : MonoBehaviour
{
    public static GlobalClock Instance;

    public float tickInterval = 1f;
    private float timer = 0f;

    public event Action OnTick;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            timer = 0f;
            OnTick?.Invoke();
        }
    }
}
