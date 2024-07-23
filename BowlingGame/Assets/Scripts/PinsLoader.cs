using System.Collections.Generic;
using UnityEngine;

public class PinsLoader : MonoBehaviour
{
    [SerializeField] Pin bowlingPinPrefab;
    private const int NUM_PIN_ROWS = 4;
    private readonly float pinSpacing = 0.3f;
    private readonly static float pinsBaseHeight = 1.2f;
    private readonly static Vector2 pinsOriginPosition = new(0, 22);
    private List<Pin> activePins = new();

    void Start()
    {
        List<List<Vector3>> pinPositions = CalculatePositions();
        if (bowlingPinPrefab != null)
        {
            InstantiatePins(pinPositions);
        }
        else
        {
            Debug.LogError("Error: The bowlingPinPrefab has not been assigned!");
        }
    }

    private void InstantiatePins(List<List<Vector3>> pinPositions)
    {
        foreach (List<Vector3> row in pinPositions)
        {
            foreach (Vector3 position in row)
            {
                Pin newPin = Instantiate(bowlingPinPrefab, position, Quaternion.identity);
                activePins.Add(newPin);
            }
        }
    }

    private List<List<Vector3>> CalculatePositions()
    {
        List<List<Vector3>> positions = new();

        for (int row = 0; row < NUM_PIN_ROWS; row++)
        {
            List<Vector3> rowPositions = new();
            float xOffset = -(row * pinSpacing) / 2f;

            for (int col = 0; col <= row; col++)
            {
                float xPos = pinsOriginPosition.x + xOffset + (col * pinSpacing);
                float yPos = pinsOriginPosition.y + (row * pinSpacing);
                rowPositions.Add(new Vector3(xPos, pinsBaseHeight, yPos));
            }

            positions.Add(rowPositions);
        }

        return positions;
    }

    public void ResetPins()
    {
        foreach (Pin pin in activePins)
        {
            pin.ResetPin();
        }
    }
}
