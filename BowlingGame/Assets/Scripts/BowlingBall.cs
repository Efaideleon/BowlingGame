using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    [SerializeField] float forceMultiplier = 50f;
    [SerializeField] float spinMultiplier = 10f;
    [SerializeField] float maxChargeTime = 2f;
    private Rigidbody rb;
    private float chargeStartTime;
    private bool isCharging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Time.time - chargeStartTime >= maxChargeTime)
        {
            if (isCharging)
            {
                ReleaseBall();
            }
        }
    }

    void ReleaseBall()
    {
        isCharging = false;

        float chargeDuration = Time.time - chargeStartTime;
        float power = Mathf.Clamp01(chargeDuration / maxChargeTime);

        Vector3 throwForce = Vector3.forward * power * forceMultiplier;

        float spin = Input.GetAxis("Horizontal");
        Vector3 spinTorque = Vector3.up * spin * spinMultiplier;

        rb.AddForce(throwForce, ForceMode.Impulse);
        rb.AddTorque(spinTorque);
    }
}
