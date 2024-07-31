using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PinManager : MonoBehaviour
{
    [SerializeField] Pin bowlingPinPrefab;
    private const int NUM_PIN_ROWS = 4;
    private readonly float _pinSpacing = 0.3f;
    private readonly static float _pinsBaseHeight = 1.2f;
    private readonly static Vector2 _pinsOriginPosition = new(0, 22);

    public readonly float pinSettleTime = 1f;
    private readonly List<Pin> _pins = new();
    public event Action OnPinsSettled;

    void Start()
    {
        LoadPins();
    }

    private void InstantiatePins(IEnumerable<Vector3> pinPositions)
    {
        _pins.AddRange(pinPositions.Select(position => Instantiate(bowlingPinPrefab, position, Quaternion.identity)));
    }

    private IEnumerable<Vector3> CalculatePositions()
    {
        for (int row = 0; row < NUM_PIN_ROWS; row++)
        {
            float xOffset = -(row * _pinSpacing) / 2f;

            for (int col = 0; col <= row; col++)
            {
                float xPos = _pinsOriginPosition.x + xOffset + (col * _pinSpacing);
                float yPos = _pinsOriginPosition.y + (row * _pinSpacing);
                yield return new Vector3(xPos, _pinsBaseHeight, yPos);
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

    public int CountFallenPins() => _pins.Count(pin => pin.IsFallen && pin.gameObject.activeSelf);

    public void RemoveFallenPins()
    {
        foreach (Pin pin in _pins)
        {
            if (pin.IsFallen)
            {
                pin.gameObject.SetActive(false);
            }
            else {
                pin.ResetPin();
            }
        }
    }

    public void ResetPins() => _pins.ForEach(pin => pin.ResetPin());

    public void CheckForPinsToSettle() => StartCoroutine(WaitForPinsToSettle());

    private IEnumerator WaitForPinsToSettle()
    {
        yield return new WaitForSeconds(pinSettleTime);

        if (_pins.All(pin => pin.IsSettled))
        {
            OnPinsSettled.Invoke();
        }
        else
        {
            StartCoroutine(WaitForPinsToSettle());
        }
    }
}
