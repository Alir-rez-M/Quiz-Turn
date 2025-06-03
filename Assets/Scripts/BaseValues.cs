
using UnityEngine;

public class BaseValues : MonoBehaviour
{
    public BattleManager battleManager;
    public Transform goPoint;
    public Transform startPosition;
    public float speed;
    public float attackDuration;
    public float damage;
    public Transform attackingPoint;
    public  float radius;
    public Animator animator;
    public LayerMask enemy;


    public virtual void MoveToEnemy()
    {
        Debug.Log("HIII");
    }
}
