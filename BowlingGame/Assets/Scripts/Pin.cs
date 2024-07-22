using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool isFallen = false;

    void Update()
    {
        if (!isFallen && CheckIfFallen()) 
        {
            isFallen = true;
            GameManager.Instance.PinFallen();
        }
    }

    private bool CheckIfFallen()
    {
        return transform.up.y < 0.5f;
    }
}
