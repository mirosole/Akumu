using System.Collections;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float duration = 30f;   // Время до деактивации врага
    [SerializeField] private float spawnDelay = 0f;  // Настраиваемая задержка перед спавном

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpawnEnemyWithDelay());
        }
    }

    private IEnumerator SpawnEnemyWithDelay()
    {
        if (spawnDelay > 0)
        {
            yield return new WaitForSeconds(spawnDelay);
        }
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (enemy != null && !enemy.activeSelf)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            
            enemy.SetActive(true);
            
            StartCoroutine(EnemyDestroyAfterTime());
        }
    }
    
    private IEnumerator EnemyDestroyAfterTime()
    {
        yield return new WaitForSeconds(duration);

        if (enemy != null)
        {
            enemy.SetActive(false);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
#endif
}