using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVision : MonoBehaviour
{
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float enemiesCheckRange;
    [SerializeField] private float enemiesCheckDelay = 0.5f;

    [Space]
    [ReadOnly, SerializeField] private List<Actor> enemies;

    private Actor actor;

    public Actor Actor { set => actor = value; }
    public List<Actor> Enemies { get => enemies; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemiesCheckRange);
    }

    public void Init()
    {
        enemies = new List<Actor>();

        StartCoroutine(ECheckEnemies());
    }

    public void Kill()
    {
        StopCoroutine(ECheckEnemies());
    }

    private IEnumerator ECheckEnemies()
    {
        while (true)
        {
            enemies.Clear();

            Collider2D[] colliderAround = Physics2D.OverlapCircleAll(transform.position, enemiesCheckRange, enemiesLayer);

            foreach (Collider2D col in colliderAround)
            {
                if (col.TryGetComponent(out Actor enemyActor) && actor.IsEnemy(enemyActor.ID))
                {
                    enemies.Add(enemyActor);
                }
            }

            yield return new WaitForSeconds(enemiesCheckDelay);
        }
    }
}
