using UnityEngine;

public class SpaceShipEngine : MonoBehaviour
{
    #region Access Fields
    public float MaxPower { get { return maxPower; } set { maxPower = value; } }
    public float FuelUsage { get { return FuelUsagePerSecond; } set { FuelUsagePerSecond = value; } }
    public bool Push { get; set; }
    #endregion

    [SerializeField] float maxPower = 0.5f;
    protected float power;
    [SerializeField] float topBorder = 10;
    [SerializeField] float FuelUsagePerSecond = 10;

    Rigidbody rb;
    EnginesParticleController particleController;
    [SerializeField]FuelTank tank;

    #region Unity Callbacks
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleController = GetComponentInChildren<EnginesParticleController>();
    }

    protected virtual void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up * power / rb.mass, Color.green);

        AccumulateForce();
    }

    private void FixedUpdate()
    {
        Thrust();
        HandleParticle();
    }
    #endregion

    #region Adjustable Overidable Force Accumulating methods
    protected virtual void AccumulateForce()
    {
        if(Push)
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
        float availableFuel = tank.UseFuel(FuelUsagePerSecond * Time.deltaTime * power/MaxPower);

        if (availableFuel > 0)
        {
            // Available Fuel will always be less or equal to FuelUsage over time
            float actualPower = availableFuel / (FuelUsagePerSecond * Time.deltaTime);
            rb.AddForce(transform.up * power * rb.mass * actualPower);
            //particleController.Thrust();
        }
    }

    private void HandleParticle()
    {
            if (power > 1)
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
