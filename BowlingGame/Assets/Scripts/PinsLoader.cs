using System.Collections.Generic;
using UnityEngine;

public class PinsLoader : MonoBehaviour
{
    [SerializeField] GameObject bowlingPinPrefab;
    private float spacing = 0.3f;
    private readonly static float heighLevel = 1.2f;
    private readonly static Vector2 origin = new Vector2(-0.85f, 0);
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
            Debug.LogError("Error: The bawlingPinPrefab has not been assigned!");
        }
    }

    List<List<Vector3>> CalculatePositions()
    {
        List<List<Vector3>> positions = new List<List<Vector3>> ();
        float spacingOffset;
        float yPosition = origin.y;
        float xPosition = origin.x;
        float xStart = origin.x;
        for (int i = 0; i < 4; i++)
        {
            List<Vector3> row = new List<Vector3>();
            for (int j = 0; j < i + 1; j++)
            {
                row.Add(new Vector3(xPosition, heighLevel, yPosition));
                xPosition += spacing;
            }
            positions.Add(row);
            spacingOffset = -(spacing / 2);
            yPosition += spacing;
            xStart += spacingOffset;
            xPosition = xStart;
        }

        return positions;
    }
}
