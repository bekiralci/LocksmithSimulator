using UnityEngine;

// Hareket Arayüzü (SOLID - Interface Segregation)
public interface IMovement
{
    void Move(Vector2 input);
    void Jump();
}

// Karakter Hareketi (SOLID - Single Responsibility, Dependency Inversion)
public class PlayerMovement : MonoBehaviour, IMovement
{
    private CharacterController controller;
    private Vector3 velocity;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 input)
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Yere daha iyi oturmasý için
        }

        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
