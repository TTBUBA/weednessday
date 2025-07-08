using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovementBoat : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Position;
    [SerializeField] private GameObject PointFinal;
    [SerializeField] private Light2D LightBlue;
    [SerializeField] private Light2D LightRed;
    [SerializeField] private AudioSource AudioSiren;
    public GameObject CurrentPoint;
    private Vector3 StartPos;
    public bool BoatRight;
    public bool ActiveLight;
    public BoatState stateBoat = BoatState.Idle;
    public float speed = 4f;

    public enum BoatState
    {
        Idle,
        Moving
    }

    private void Awake()
    {
        StartCoroutine(AnimationLight());
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveMovement();
    }

    //Active Boat
    public void ActiveBoat()
    {
        SetRandomDestination();
        stateBoat = BoatState.Moving;
        speed = 4f;
        AudioSiren.Play();
    }

    //Active movement of the boat
    private void ActiveMovement()
    {

        if (stateBoat == BoatState.Moving && CurrentPoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, CurrentPoint.transform.position ,speed * Time.deltaTime);
        }


        if (CurrentPoint != null && Vector2.Distance(transform.position, CurrentPoint.transform.position) < 0.1)
        {
            stateBoat = BoatState.Idle;
            speed = 0;
        }

        // If the boat is moving to the final point, stop it and return to the start position
        if (Vector2.Distance(transform.position, PointFinal.transform.position) < 0.1f)
        {
            CurrentPoint = null;
            stateBoat = BoatState.Idle;
            speed = 0;
            transform.position = StartPos;
            AudioSiren.Stop();
        }
    }

    //return boat to the final point
    public void ReturnBase()
    {
        stateBoat = BoatState.Moving;
        CurrentPoint = PointFinal;
        speed = 4f;
        AudioSiren.Play();
    }

    //Set a random destination from the array of positions
    private void SetRandomDestination()
    {
        if (m_Position.Length == 0) return;
        int randomIndex = Random.Range(0, m_Position.Length);
        CurrentPoint = m_Position[randomIndex];
    }

    //Animation of the lights on the boat
    IEnumerator AnimationLight()
    {
        while (ActiveLight)
        {
            yield return new WaitForSeconds(0.5f);
            LightBlue.enabled = true;
            LightRed.enabled = false;
            yield return new WaitForSeconds(0.5f);
            LightBlue.enabled = false;
            LightRed.enabled = true;
        }
    }

}
