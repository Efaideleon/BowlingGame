using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PinManager : MonoBehaviour, IPinManager {
    [Header("References")]
    [SerializeField] Pin bowlingPinPrefab;

    [Header("Settings")]
    [SerializeField] float pinSettleTime = 9f;

    const int NUM_PIN_ROWS = 4;
    readonly float _pinSpacing = 0.3f;
    readonly static float _pinsBaseHeight = 1.2f;
    readonly static Vector2 _pinsOriginPosition = new(0, 22);

    readonly List<Pin> _pins = new();
    public event Action<int> OnPinsSettled;

    void Start() {
        LoadPins();
    }

    void InstantiatePins(IEnumerable<Vector3> pinPositions) {
        _pins.AddRange(pinPositions.Select(position => Instantiate(bowlingPinPrefab, position, Quaternion.identity)));
    }

    IEnumerable<Vector3> CalculatePositions() {
        for (int row = 0; row < NUM_PIN_ROWS; row++) {
            float xOffset = -(row * _pinSpacing) / 2f;

            for (int col = 0; col <= row; col++) {
                float xPos = _pinsOriginPosition.x + xOffset + (col * _pinSpacing);
                float yPos = _pinsOriginPosition.y + (row * _pinSpacing);
                yield return new Vector3(xPos, _pinsBaseHeight, yPos);
            }
        }
    }

    private void LoadPins() {
        if (bowlingPinPrefab != null) {
            InstantiatePins(CalculatePositions());
        }
        else {
            Debug.LogError("Error: The bowlingPinPrefab has not been assigned!");
        }
    }

    int CountFallenPins() => _pins.Count(pin => pin.IsFallen && pin.gameObject.activeSelf);

    void RemoveFallenPins() {
        foreach (Pin pin in _pins) {
            if (pin.IsFallen) {
                pin.gameObject.SetActive(false);
            }
            else {
                pin.ResetPin();
            }
        }
    }

    public void ResetPins(bool all) {
        if (all) {
            _pins.ForEach(pin => pin.ResetPin());
        }
        else {
            RemoveFallenPins();
        }
    }

    public void CheckForPinsToSettle() => StartCoroutine(WaitForPinsToSettle());

    private IEnumerator WaitForPinsToSettle() {
        yield return new WaitForSeconds(pinSettleTime);

        if (_pins.All(pin => pin.IsSettled)) {
            OnPinsSettled.Invoke(CountFallenPins());
        }
        else {
            StartCoroutine(WaitForPinsToSettle());
        }
    }
}
