using UnityEngine;

public class PinsLoader : MonoBehaviour
{
    [SerializeField] GameObject bowlingPinPrefab;
    private readonly static float pinsHeight = 1.2f;
    private readonly Vector3[][] pinsPositions = new Vector3[][] {
        new Vector3[] {
            new(-3f, pinsHeight, 0),
            new(-1f, pinsHeight, 0),
            new(1f, pinsHeight, 0),
            new(3f, pinsHeight, 0)
        },
        new Vector3[] {
            new(-2, pinsHeight, -1),
            new(0, pinsHeight, -1),
            new(2, pinsHeight, -1)
        },
        new Vector3[] {
            new(-1, pinsHeight, -2),
            new(1, pinsHeight, -2)
        },
        new Vector3[] {
            new(0, pinsHeight, -3)
        }
    };

    void Start()
    {
        if (bowlingPinPrefab != null) {
            foreach (Vector3[] row in pinsPositions)
            {
                foreach (Vector3 position in row)
                {
                    Instantiate(bowlingPinPrefab, position, Quaternion.identity);
                }
            }
        } else {
            Debug.LogError("Error: The bawlingPinPrefab has not been assigned!");
        }
    }
}
