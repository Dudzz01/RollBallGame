using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class EnemyBase : MonoBehaviour
{
    #region AttributesOfEnemy

    [Header("Attributes Of Enemy")]
    [Space(5)]

    [SerializeField] protected float speedEnemy;

    [SerializeField] protected int lifeEnemy;

    [SerializeField] protected int damageEnemy;

    private NavMeshAgent enemy;
    

    protected bool canCauseDamage = true;
    #endregion

    #region ComponentsOfEnemy

    [Header("Components Of Enemy")]
    [Space(5)]

    [SerializeField] protected Rigidbody rig;

    [SerializeField] protected GameObject bulletEnemy;

    #endregion

    #region ReferencesRelationWithEnemyAttribues
    protected GameObject target; 
    #endregion

    protected virtual void Awake() 
    {
        target = PlayerController.Instance.gameObject;
        enemy = gameObject.GetComponent<NavMeshAgent>();
    }

    protected virtual void MoveEnemy()
    {
        if(enemy!=null)
        {
            enemy.SetDestination(target.transform.position);
        }
        
    }

    protected virtual void ShootEnemy()
    {

    }

    protected void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "Player" && canCauseDamage)
        {
            StartCoroutine(DamagePlayer());
        }
    }

    protected IEnumerator DamagePlayer()
    {
        PlayerController.Instance.lifePlayer -= damageEnemy;
        canCauseDamage = false;
        yield return new WaitForSeconds(1f);
        canCauseDamage = true;
        yield return null;
    }






}
