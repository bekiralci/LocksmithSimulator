using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private IMovement movement;

    private void Awake()
    {
        movement = GetComponent<IMovement>();

        // Mouse imlecini gizle ve kilitle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Hareket giriþlerini al
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Move(moveInput);

        // Zýplama kontrolü
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }

        // ESC ile Mouse'u geri getir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
