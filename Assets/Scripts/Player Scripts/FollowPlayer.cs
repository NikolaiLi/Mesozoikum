using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; 
    public Transform cameraHolderTransform; 

    public Vector3 offset;
    public float sensitivity = 2f;
    public float maxLookAngle = 80f;

    private float verticalLookRotation = 0f;

    void LateUpdate()
    {
        
        cameraHolderTransform.position = playerTransform.position + offset;

        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);

        cameraHolderTransform.rotation = Quaternion.Euler(verticalLookRotation, playerTransform.eulerAngles.y, 0f);
    }
}