using UnityEngine;

[CreateAssetMenu(fileName = "BowlingGameConfig", menuName = "BowlingBall/BowlingGameConfig")]
public class BowlingGameConfig : ScriptableObject, IBowlingGameConfig {
    [Header("Constants")]
    [SerializeField] private int _maxFrames; 
    [SerializeField] private int _maxPins; 

    public int MaxFrames => _maxFrames;
    public int MaxPins => _maxPins;
}