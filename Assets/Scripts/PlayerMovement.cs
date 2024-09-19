using UnityEngine;

public class SimplePointClickMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float minYPosition = -4.5f;

    private float targetYPosition;
    private float targetXPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            if (CheckMouseClickPosition(mousePosition))
            {
                targetYPosition = mousePosition.y;
                targetXPosition = mousePosition.x;
                isMoving = true;
            }
            else
            {
                Debug.Log("Click position is not valid.");
            }
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    bool CheckMouseClickPosition(Vector3 mousePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        return hit.collider != null;
    }

    void MoveToTarget()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetYPosition, moveSpeed * Time.deltaTime);
        currentPosition.y = Mathf.Max(currentPosition.y, minYPosition);

        currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetXPosition, moveSpeed * Time.deltaTime);

        transform.position = currentPosition;

        float scaleFactor = 1 - (currentPosition.y + 4) / 10;
        scaleFactor = Mathf.Clamp(scaleFactor, 0.1f, 1f);

        float scaleX = 1f * scaleFactor;
        float scaleY = 1.8f * scaleFactor;

        transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);

        if (Mathf.Abs(currentPosition.y - targetYPosition) < 0.1f &&
            Mathf.Abs(currentPosition.x - targetXPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
}

