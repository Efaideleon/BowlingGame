using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Controller in the MVP architecture between the model (BowlingBall) and Views.
/// </summary>
public class UIController : MonoBehaviour {
    [SerializeField] List<UIElement> m_UIElements;
    // The model in the MVP pattern
    [SerializeField] BowlingGame m_Game;

    void Awake() {
        m_Game.OnRollCompleted += UpdateUI;
    }

    void OnDestroy() {
        m_Game.OnRollCompleted -= UpdateUI;
    }

    void UpdateUI() {
        foreach (var uiElement in m_UIElements) {
            uiElement.UpdateUI(m_Game);
        }
    }
}