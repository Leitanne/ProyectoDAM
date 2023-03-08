using UnityEngine;

public enum MovementDirection
{
    Horizontal,
    Vertical
}
public class WaypointMovement : MonoBehaviour
{
    [SerializeField] protected float speed;

    public Vector3 PointToMove => _waypoint.GetMovementPosition(currentPointIndex);

    protected Waypoint _waypoint;
    protected Animator _animator;
    protected int currentPointIndex;
    protected Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPointIndex = 0;
        _animator = GetComponent<Animator>();
        _waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        RotateCharacter();
        RotateVertical();

        if(CheckPointReached())
        {
            UpdateMovementIndex();
        }
    }

    private void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointToMove, 
            speed * Time.deltaTime);
    }

    private bool CheckPointReached()
    {
        float distanceToCurrentPoint = (transform.position - PointToMove).magnitude;
        
        if(distanceToCurrentPoint < 0.1f)
        {
            lastPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateMovementIndex()
    {
        if(currentPointIndex == _waypoint.Points.Length - 1)
        {
            currentPointIndex = 0;
        }
        else
        {
            if (currentPointIndex < _waypoint.Points.Length - 1)
            {
                currentPointIndex++;
            }
        }
    }

    protected virtual void RotateCharacter()
    {

    }

    protected virtual void RotateVertical()
    {

    }
}
