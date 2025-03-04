using UnityEngine;
using System.Collections;

public class InfiniteStaircaseTeleport : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform secondFloorBaseTeleportPoint;
    [SerializeField] private float teleportDuration = 0.01f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerPosition = player.position;
            Vector3 triggerCenter = transform.position;

            float xOffset = playerPosition.x - triggerCenter.x;

            Vector3 targetTeleportPosition = new Vector3(secondFloorBaseTeleportPoint.position.x + xOffset,
                secondFloorBaseTeleportPoint.position.y,
                secondFloorBaseTeleportPoint.position.z);

            StartCoroutine(TeleportPlayer(targetTeleportPosition));
        }
    }

    private IEnumerator TeleportPlayer(Vector3 targetTeleportPosition)
    {
        Vector3 startPosition = player.position;
        float elapsedTime = 0;

        while (elapsedTime < teleportDuration)
        {
            player.position = Vector3.Lerp(startPosition, targetTeleportPosition, elapsedTime / teleportDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.position = targetTeleportPosition;
    }
}