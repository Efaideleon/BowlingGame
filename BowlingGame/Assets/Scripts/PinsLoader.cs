using System.Collections.Generic;
using UnityEngine;

public class PinsLoader : MonoBehaviour
{
    [SerializeField] GameObject bowlingPinPrefab;
    private const int NUM_PIN_ROWS = 4;
    private readonly float spacing = 0.3f;
    private readonly static float heightLevel = 1.2f;
    private readonly static Vector2 origin = new(0, 22);
    private List<List<Vector3>> positions;

    void Start()
    {
        positions = CalculatePositions();
        if (bowlingPinPrefab != null)
        {
            foreach (List<Vector3> row in positions)
            {
                foreach (Vector3 position in row)
                {
                    Instantiate(bowlingPinPrefab, position, Quaternion.identity);
                }
            }
        }
        else
        {
            Debug.LogError("Error: The bowlingPinPrefab has not been assigned!");
        }
    }

    List<List<Vector3>> CalculatePositions()
    {
        List<List<Vector3>> positions = new();

        for (int row = 0; row < NUM_PIN_ROWS; row++)
        {
            List<Vector3> rowPositions = new();
            float xOffset = -(row * spacing) / 2f;

            for (int col = 0; col <= row; col++)
            {
                float xPos = origin.x + xOffset + (col * spacing);
                float yPos = origin.y + (row * spacing);
                rowPositions.Add(new Vector3(xPos, heightLevel, yPos));
            }

            positions.Add(rowPositions);
        }

        return positions;
    }
}
