using UnityEngine;
using System.Collections.Generic;

namespace environment
{
    public class RollLightsController : MonoBehaviour
    {
        [Tooltip("The current lane the player is in.")]
        [SerializeField] private int _currentLaneIndex = 0;

        [Header("Model")]
        [Tooltip("Reference to the BowlingGame Model")]
        [SerializeField] private BowlingGame _bowlingGame;

        [Header("Configuration")]
        [SerializeField] private float emissionIntensity = 1500;

        [Header("Roll Lights")]
        [SerializeField] private GameObject[] _laneOneLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneTwoLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneThreeLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneFourLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneFiveLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneSixLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneSevenLights = new GameObject[3];
        [SerializeField] private GameObject[] _laneEightLights = new GameObject[3];

        private List<GameObject[]> _lights = new(8);
        private Color _initialColor;
        private int _currentRollIndex => (int)_bowlingGame.CurrentFrame.CurrentRollNumber - 1;
        private Material CurrentLightMaterial => GetCurrentLightMaterial();

        void OnEnable()
        {
            _bowlingGame.OnRollCompleted += UpdateLights;
        }

        void OnDisable()
        {
            _bowlingGame.OnRollCompleted -= UpdateLights;
        }

        void Start()
        {
            _lights.Add(_laneOneLights);
            _lights.Add(_laneTwoLights);
            _lights.Add(_laneThreeLights);
            _lights.Add(_laneFourLights);
            _lights.Add(_laneFiveLights);
            _lights.Add(_laneSixLights);
            _lights.Add(_laneSevenLights);
            _lights.Add(_laneEightLights);
            _initialColor = _lights[0][0].GetComponent<MeshRenderer>().materials[1].color;
            UpdateLights();
        }

        private void UpdateLights()
        {
            if (_currentRollIndex < 1)
            {
                ResetLights();
            }

            TurnOnCurrentLight();
        }

        private void ResetLights()
        {
            foreach(GameObject light in _lights[_currentLaneIndex])
            {
                Material lightMaterial = light.GetComponent<MeshRenderer>().materials[1];
                ChangeMaterialBaseAndEmssiveColor(lightMaterial, _initialColor, _initialColor);
            }
        }

        private void TurnOnCurrentLight()
        {
            ChangeMaterialBaseAndEmssiveColor(CurrentLightMaterial, Color.green, Color.green);
        }

        private void ChangeMaterialBaseAndEmssiveColor(Material material, Color emmisiveColor, Color baseColor)
        {
            // HDRP Update Emission color
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissiveColor", baseColor * emissionIntensity);
            material.SetColor("_BaseColor", emmisiveColor * emissionIntensity);
        }

        private Material GetCurrentLightMaterial()
        {
            var currentLights = _lights[_currentLaneIndex];
            var currentLight = currentLights[_currentRollIndex];
            return currentLight.GetComponent<MeshRenderer>().materials[1];
        }
    }
}