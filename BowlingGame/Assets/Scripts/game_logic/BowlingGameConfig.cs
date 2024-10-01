using UnityEngine;

[CreateAssetMenu(fileName = "BowlingGameConfig", menuName = "BowlingBall/BowlingGameConfig")]
public class BowlingGameConfig : ScriptableObject, IBowlingGameConfig {
    [Header("Constants")]
    [SerializeField] private int _maxFrames;
    [SerializeField] private int _maxPins;

    public int MaxFrames {
        get { return _maxFrames; }
        set { _maxFrames = value; }
    }
    public int MaxPins {
        get { return _maxPins; }
        set { _maxPins = value; }
    }
}