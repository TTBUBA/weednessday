using UnityEngine;

public class MovementBoat : MonoBehaviour
{
    [SerializeField] private GameObject m_Position;
    public float speed = 4f;
    public bool ActiveBoat;


    // Update is called once per frame
    void Update()
    {
        ActiveMovement();
    }

    private void ActiveMovement()
    {
        if (ActiveBoat)
        {
            transform.position = Vector2.MoveTowards(transform.position,m_Position.transform.position,speed * Time.deltaTime);
        } 
        if(Vector2.Distance(transform.position,m_Position.transform.position) < 0.1)
        {
            ActiveBoat = false;
            speed = 0;
        }
    }

}
