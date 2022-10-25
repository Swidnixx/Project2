using UnityEngine;

[RequireComponent(typeof(FuelTank))]
public class SpaceShipThruster : MonoBehaviour
{
    #region Access Fields
    public float Power { get { return power; } set { power = value; } }
    public float FuelUsage { get { return FuelUsagePerSecond; } set { FuelUsagePerSecond = value; } }
    #endregion

    [SerializeField] float power = 0.5f;
    [SerializeField] float topBorder = 10;
    [SerializeField] float FuelUsagePerSecond = 10;

    Rigidbody rb;
    EnginesParticleController particleController;
    FuelTank tank;

    #region Unity Callbacks
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleController = GetComponentInChildren<EnginesParticleController>();
        tank = GetComponentInChildren<FuelTank>();
    }

    private void FixedUpdate()
    {
        if (InputHandler.Instance == null) return;

        if (transform.position.y < topBorder)
        {
            if (InputHandler.Instance.MouseHold)
            {
                float availableFuel = tank.UseFuel(FuelUsagePerSecond * Time.fixedDeltaTime);

                if (availableFuel > 0)
                {
                    float actualPower = availableFuel / (FuelUsagePerSecond * Time.fixedDeltaTime);
                    rb.AddForce(transform.up * power * rb.mass * actualPower);
                    particleController.Thrust(); 
                }
            }
            else
            {
                particleController.Steady();
            }

        }
    }
    #endregion
}
