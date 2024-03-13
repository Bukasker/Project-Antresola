using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveSpeed = 5f;

    [Header("Zoom Settings")]
    public float scrollSpeed = 0.5f;
    public float minZoom = 2f;
    public float maxZoom = 20f;
    public float currentZoomPercentage;
    public Camera mainCamera;

    [Header("Moving Camera Keys")]
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    [Header("Alternative Moving Camera Keys")]
    public KeyCode moveUpKeyAlt = KeyCode.UpArrow;
    public KeyCode moveDownKeyAlt = KeyCode.DownArrow;
    public KeyCode moveLeftKeyAlt = KeyCode.LeftArrow;
    public KeyCode moveRightKeyAlt = KeyCode.RightArrow;


    private void FixedUpdate()
    {
        MoveCamera();
    }
    private void Update()
    {
        UpdateZoomPercentage();
        ZoomCameraWithMouseScrollWheel();
    }

    private void MoveCamera()
    {
        if (Input.GetKey(moveUpKey) || Input.GetKey(moveUpKeyAlt))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(moveDownKey) || Input.GetKey(moveDownKeyAlt))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(moveLeftKey) || Input.GetKey(moveLeftKeyAlt))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(moveRightKey) || Input.GetKey(moveRightKeyAlt))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void ZoomCameraWithMouseScrollWheel()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0 && mainCamera.orthographicSize < maxZoom)
        {
            mainCamera.orthographicSize += scrollSpeed;
        }
        else if (scrollInput < 0 && mainCamera.orthographicSize > minZoom)
        {
            mainCamera.orthographicSize -= scrollSpeed;
        }
    }
    private void UpdateZoomPercentage()
    {
        currentZoomPercentage = Mathf.Round(((mainCamera.orthographicSize - minZoom) / (maxZoom - minZoom)) * 100f);
    }

}
