using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFollowing : EnemyBase
{
    
    

    protected override void Awake()
    {
        base.Awake();


    }

    private void Update()
    {
        MoveEnemy();

    }

    protected override void MoveEnemy()
    {
        
        base.MoveEnemy();

    }
    
}
