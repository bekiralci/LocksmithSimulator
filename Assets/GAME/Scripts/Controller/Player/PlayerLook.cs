using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public Transform playerBody;

    private Camera playerCamera;
    [SerializeField] private float interactionRange = 8f;

    void Start()
    {
        playerCamera = Camera.main;

    }

    void Update()
    {
        HandleMouseLook();

        if (Input.GetMouseButtonDown(0)) // Sol fare tu�u
        {
            HandleLeftClick();
        }

        if (Input.GetMouseButtonDown(1)) // Sa� fare tu�u
        {
            HandleRightClick();
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void HandleLeftClick()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        // Raycast ile objeye �arpma kontrol�
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.TryGetComponent(out ILeftClickListener leftClickListener))
            {
                leftClickListener.OnLeftClick();
            }
        }
    }

    private void HandleRightClick()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            IRightClickListener rightClickListener = hit.collider.GetComponent<IRightClickListener>();

            if (rightClickListener != null)
            {
                rightClickListener.OnRightClick();
                Debug.Log("Sa� t�klama: " + hit.collider.name);
            }
            else
            {
                Debug.Log("Sa� t�klama yap�ld� fakat sa� t�klanabilir obje bulunamad�");
            }
        }
    }
}
