using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : EnemyController
{
    [SerializeField] private GameObject gameOverStuff;
    private void OnDestroy()
    {
        gameOverStuff.SetActive(true);
    }
}
