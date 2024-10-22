using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Controller in the MVP architecture between the model (BowlingBall) and Views.
/// </summary>
public class UIController : MonoBehaviour {
    [Header("UI Elements")]
    [Tooltip("UI Elements that implement the IUIElement interface")]
    [SerializeField] List<UIElement> m_UIElements;

    [Header("Data Model")]
    [Tooltip("Store the Bowling Game data")]
    [SerializeField] BowlingGame m_Game;

    void Awake() {
        if (m_Game == null) {
            throw new NullReferenceException("m_Game is not initialized");
        }
        UpdateUI();
        m_Game.OnRollCompleted += UpdateUI;
        m_Game.OnReset += UpdateUI;
    }

    void OnDestroy() {
        m_Game.OnRollCompleted -= UpdateUI;
        m_Game.OnReset -= UpdateUI;
    }

    /// <summary>
    /// Updates the data for each UI Element.
    /// </summary>
    void UpdateUI() {
        foreach (var uiElement in m_UIElements) {
            uiElement.UpdateUI(m_Game);
        }
    }
}