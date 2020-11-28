using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnFood : MonoBehaviour
{

    public GameObject foodPrefab;
    public static int foodCount = 0;
    private const int maxFoodCountInScene = 4;
    private Vector2 bounds;

    private void Start()
    {
        Transform [] borders = transform.GetComponentsInChildren <Transform> ();

        foreach (Transform child in borders) {
            bounds = Vector2.Max (bounds, child.position);
        }
        bounds = (bounds.x - 0.5f) * Vector2.right + (bounds.y - 0.5f) * Vector2.up;
        InvokeRepeating("Spawn", 3, 4);
    }

    public static void EatOne() {
        if (foodCount >= 1) {
            foodCount--;
        }
    }

    private void Spawn() {
        if (GameController.isPaused) return;
        int x = (int)Random.Range(-bounds.x, bounds.x);
        int y = (int)Random.Range(-bounds.y, bounds.y);

        if (foodCount < maxFoodCountInScene) {
            Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
            foodCount++;
        }
    }
}
