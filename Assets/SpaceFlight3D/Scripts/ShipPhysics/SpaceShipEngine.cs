using UnityEngine;

public class SpaceShipEngine : MonoBehaviour
{
    #region Access Fields
    public float MaxPower { get { return maxPower; } set { maxPower = value; } }
    public float FuelUsage { get { return fuelUsagePerSecond; } set { fuelUsagePerSecond = value; } }
    public bool Push { get; set; }
    #endregion

    [SerializeField] float maxPower = 0.5f;
    protected float power;
    [SerializeField] float topBorder = 10; //How high on y engine will be still working
    [SerializeField] float fuelUsagePerSecond = 10;

    [SerializeField]FuelTank tank;
    EngineParticleController particleController;
    Rigidbody rb;

    public string number;

    #region Unity Callbacks
    protected virtual void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        particleController = GetComponentInChildren<EngineParticleController>();
    }

    protected virtual void Update()
    {
        //if((Camera.current.cullingMask(1 << gameObject.layer)) > 0)
            Debug.DrawRay(transform.position, transform.up * power / maxPower , Color.green);

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
        if(Push && transform.position.y < topBorder)
        {
            power = maxPower * InputHandler.Instance.Upwards;
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
        float availableFuel = tank? tank.UseFuel(fuelUsagePerSecond * Time.deltaTime * power/MaxPower) : fuelUsagePerSecond * Time.deltaTime * power / MaxPower;

        if (availableFuel > 0)
        {
            // Available Fuel will always be less or equal to FuelUsage over time, which means that actualPower is in (0-1) range
            float actualPower = availableFuel / (fuelUsagePerSecond * Time.deltaTime);
            rb.AddForce(transform.up * power * rb.mass * actualPower);
            //Debug.Log("Ship " + number + ": " + actualPower + " * " + power);
        }
    }

    private void HandleParticle()
    {
        if (!particleController || !particleController.isActiveAndEnabled) return;

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
