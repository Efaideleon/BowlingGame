using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PinManager : MonoBehaviour
{
    [SerializeField] Pin bowlingPinPrefab;
    private const int NUM_PIN_ROWS = 4;
    private const float PIN_SETTLE_TIME = 1f;
    private readonly float pinSpacing = 0.3f;
    private readonly static float pinsBaseHeight = 1.2f;
    private readonly static Vector2 pinsOriginPosition = new(0, 22);
    private readonly List<Pin> pins = new();
    public event Action OnPinsSettled;

    void Start()
    {
        LoadPins();
    }

    private void InstantiatePins(IEnumerable<Vector3> pinPositions)
    {
        pins.AddRange(pinPositions.Select(position => Instantiate(bowlingPinPrefab, position, Quaternion.identity)));
    }

    private IEnumerable<Vector3> CalculatePositions()
    {
        for (int row = 0; row < NUM_PIN_ROWS; row++)
        {
            float xOffset = -(row * pinSpacing) / 2f;

            for (int col = 0; col <= row; col++)
            {
                float xPos = pinsOriginPosition.x + xOffset + (col * pinSpacing);
                float yPos = pinsOriginPosition.y + (row * pinSpacing);
                yield return new Vector3(xPos, pinsBaseHeight, yPos);
            }
        }
    }

    private void LoadPins()
    {
        if (bowlingPinPrefab != null)
        {
            InstantiatePins(CalculatePositions());
        }
        else
        {
            Debug.LogError("Error: The bowlingPinPrefab has not been assigned!");
        }
    }

    public int CountFallenPins() => pins.Count(pin => pin.IsFallen && pin.gameObject.activeSelf);

    public void RemoveFallenPins()
    {
        foreach (Pin pin in pins)
        {
            if (pin.IsFallen)
            {
                pin.gameObject.SetActive(false);
            }
        }
    }

    public void ResetPins() => pins.ForEach(pin => pin.ResetPin());

    public void CheckForPinsToSettle() => StartCoroutine(WaitForPinsToSettle());

    private IEnumerator WaitForPinsToSettle()
    {
        yield return new WaitForSeconds(PIN_SETTLE_TIME);

        if (pins.All(pin => pin.IsSettled))
        {
            OnPinsSettled.Invoke();
        }
        else
        {
            StartCoroutine(WaitForPinsToSettle());
        }
    }
}
