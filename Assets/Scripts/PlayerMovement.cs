using UnityEngine;

public class SimplePointClickMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minYPosition = -5f;
    private float targetYPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            targetYPosition = mousePosition.y;
            isMoving = true;
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetYPosition, moveSpeed * Time.deltaTime);
        currentPosition.y = Mathf.Max(currentPosition.y, minYPosition);
        transform.position = currentPosition;

        float scaleFactor = 1 - (currentPosition.y + 4) / 10;
        scaleFactor = Mathf.Clamp(scaleFactor, 0.2f, 1f);

        float scaleX = 1f * scaleFactor;
        float scaleY = 1.8f * scaleFactor;

        transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);

        if (Mathf.Abs(currentPosition.y - targetYPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
}


