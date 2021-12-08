using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Transform landingForward, landingBackward, aircraftCarrier;
    [SerializeField] private float speed = 100;
    [SerializeField] private GameObject planeSpawner;
    private bool caughtAircraftCarrier, inLanding;
    private Transform landingTarget;
    private Vector3 flyTarget;
    private Spawner checkPlanes;
    private PlaneController planeController;
    private Transform plane;
    private void Start()
    {
        checkPlanes = planeSpawner.GetComponent<Spawner>();
        planeController = gameObject.GetComponent<PlaneController>();
        plane = transform;
    }
    void Update()
    {
        if(caughtAircraftCarrier == false)
        {
            float step = speed * Time.deltaTime;
            plane.position = Vector3.MoveTowards(plane.position, flyTarget, step);

        }
        else
        {
            float step = speed * Time.deltaTime;
            plane.position = Vector3.MoveTowards(plane.position, landingTarget.position, step);
            if(landingTarget.position == plane.position)
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
                    plane.parent = other.transform;
                    StartLanding(landingForward);
            }
            }
            if (other.gameObject.tag == "forwardLanding")
            {
                if (inLanding == false)
                    {
                    plane.parent = other.transform;
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
    private void StartLanding(Transform target)
    {
        landingTarget = target;
        caughtAircraftCarrier = true;
        plane.LookAt(target);
        plane.localPosition = transform.right * 0;
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
        flyTarget = plane.position * -10;
        plane.LookAt(aircraftCarrier.position);
        plane.parent = null;
    }
}
