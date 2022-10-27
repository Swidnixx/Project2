using UnityEngine;

[RequireComponent(typeof(FuelTank))]
public class SpaceShipThruster : MonoBehaviour
{
    #region Access Fields
    public float MaxPower { get { return maxPower; } set { maxPower = value; } }
    public float FuelUsage { get { return FuelUsagePerSecond; } set { FuelUsagePerSecond = value; } }
    #endregion

    [SerializeField] float maxPower = 0.5f;
    protected float power;
    [SerializeField] float topBorder = 10;
    [SerializeField] float FuelUsagePerSecond = 10;

    Rigidbody rb;
    EnginesParticleController particleController;
    FuelTank tank;

    #region Unity Callbacks
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleController = GetComponentInChildren<EnginesParticleController>();
        tank = GetComponentInChildren<FuelTank>();
    }

    protected virtual void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up * power / rb.mass, Color.green);

        if (InputHandler.Instance == null) return;

        AccumulateForce();
    }

    private void FixedUpdate()
    {

        //if (transform.position.y < topBorder)
        //{
        //    if (InputHandler.Instance.MouseHold)
        //    {
        //        Thrust();
        //    }
        //    else
        //    {
        //        Steady();
        //    }

        //}

        Thrust();
        HandleParticle();
    }
    #endregion

    #region Adjustable Overidable Force Accumulating methods
    protected virtual void AccumulateForce()
    {
        if(InputHandler.Instance.MouseHold)
        {
            power = maxPower;
        }
        else
        {
            power = 0;
        }
    }
    #endregion

    #region Mechanics Methods
    private void Thrust()
    {
        float availableFuel = tank.UseFuel(FuelUsagePerSecond * Time.fixedDeltaTime);

        if (availableFuel > 0)
        {
            // Available Fuel will always be less or equal to FuelUsage over time
            float actualPower = availableFuel / (FuelUsagePerSecond * Time.fixedDeltaTime);
            rb.AddForce(transform.up * power * rb.mass * actualPower);
            //particleController.Thrust();
        }
    }

    private void Steady()
    {
        //particleController.Steady();
    }

    private void HandleParticle()
    {
        if(power > 1)
        {
            particleController.Thrust();
        }
        else
        {
            particleController.Steady();
        }
    }
    #endregion
}
