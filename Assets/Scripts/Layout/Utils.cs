using UnityEngine;

public class Utils : MonoBehaviour
{
    public static void StackWalls(GameObject wallPrefab, GameObject layoutContainer, Vector3 startPosition, Vector3 direction, long num)
    {
        Vector3 defaultDirection = new Vector3(0f, 0f, 1f).normalized;
        Vector3 currentPosition = startPosition;
        for (long i = 0; i < num; i++)
        {
            GameObject newWall = Instantiate<GameObject>(wallPrefab);

            newWall.transform.Translate(currentPosition);
            newWall.transform.Rotate(Quaternion.FromToRotation(defaultDirection, direction).eulerAngles);
            newWall.transform.parent = layoutContainer.transform;
            currentPosition += direction;
        }
    }
}