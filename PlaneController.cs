using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private GameObject aircraftCarrier, landingForward, landingBackward;
    [SerializeField] private float speed = 100;
    [SerializeField] private GameObject planeSpawner;
    private bool caughtAircraftCarrier, inLanding;
    private GameObject landingTarget;
    private Vector3 flyTarget;
    private Spawner checkPlanes;
    private PlaneController planeController;
    private void Start()
    {
        checkPlanes = planeSpawner.GetComponent<Spawner>();
        planeController = gameObject.GetComponent<PlaneController>();
    }
    void Update()
    {
        if(caughtAircraftCarrier == false)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, flyTarget, step);

        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, landingTarget.transform.position, step);
            if(landingTarget.transform.position == gameObject.transform.position)
            {
                RemovePlane();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "backwardLanding")
            {
                if (inLanding == false)
                {
                    gameObject.transform.parent = other.transform;
                    StartLanding(landingForward);
            }
            }
            if (other.gameObject.tag == "forwardLanding")
            {
                if (inLanding == false)
                    {
                    gameObject.transform.parent = other.transform;
                    StartLanding(landingBackward);
                }
            }
            if(landingTarget != null)
            {
                if (other.gameObject.name == landingTarget.name)
                {
                    checkPlanes.ChangeScore();
                    RemovePlane();
                }
            }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "exit")
        {
            RemovePlane();
        }
    }
    private void StartLanding(GameObject target)
    {
        landingTarget = target;
        caughtAircraftCarrier = true;
        gameObject.transform.LookAt(target.transform);
        gameObject.transform.localPosition = transform.right * 0;
        inLanding = true;
    }
    private void RemovePlane()
    {
        gameObject.SetActive(false);
        checkPlanes.CheckAvaiblePlanes(planeController);
    }
    public void ResetPlaneData()
    {
        caughtAircraftCarrier = false;
        inLanding = false;
        landingTarget = null;
        flyTarget = gameObject.transform.position * -10;
        gameObject.transform.LookAt(aircraftCarrier.transform.position);
        gameObject.transform.parent = null;
    }
}
