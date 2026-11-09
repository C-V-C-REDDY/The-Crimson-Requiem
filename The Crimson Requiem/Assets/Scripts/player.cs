using UnityEngine;


public class player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private bool IsMoving;

    private void Awake()
    {
        transform.position = new Vector3(10, 1, -15);
    }

    public void Start()
    {
        BoundaryLine.Instance.OnBoundaryBroken += OnBoundaryBroken;
    }
    private void Update()
    {
        if(PlayerManager.Instance.isDead) return;
        
        Vector2 inputVector = new Vector2(0,0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector.Normalize();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * speed * Time.deltaTime;
        IsMoving = moveDirection != Vector3.zero;
        float rotationSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public bool GetIsMoving()
    {
        return IsMoving;
    }

    private void OnBoundaryBroken()
    {
        Debug.Log("Boundary Broken! Player is dead.");
        speed = speed * 0.5f; // Reduce speed by half
    }




}

