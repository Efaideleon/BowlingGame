using UnityEngine;

public class GameInitializer : MonoBehaviour{ 
    [SerializeField] BowlingGame m_bowlingGame;

    void Awake () {
        m_bowlingGame.Reset();
    }
}