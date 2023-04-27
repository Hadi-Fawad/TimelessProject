using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothTime = 0.3f;
    public Vector3 offset;
    public float distance = 5.0f;
    private Vector3 velocity = Vector3.zero;

    private BoxCollider2D backgroundBounds;

    void Start()
    {
        // Get the bounds of the background
        backgroundBounds = GameObject.Find("Sky").GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = playerTransform.position + offset;

        // Restrict camera movement to the boundaries of the background
        float cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
        float cameraHalfHeight = Camera.main.orthographicSize;
        float minX = backgroundBounds.bounds.min.x + cameraHalfWidth;
        float maxX = backgroundBounds.bounds.max.x - cameraHalfWidth;
        float minY = backgroundBounds.bounds.min.y + cameraHalfHeight;
        float maxY = backgroundBounds.bounds.max.y - cameraHalfHeight;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Move camera towards target position smoothly
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Set camera distance
        transform.position -= transform.forward * distance;
    }
}
