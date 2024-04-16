using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private PlayerController playerController;

    private void FixedUpdate()
    {
        if (!uiController.AnyWindowShown)
        {
            playerController.SetWalkDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            uiController.HideShowCharacterWindow();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiController.Escape();
        }

        var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!uiController.AnyWindowShown)
        {
            uiController.AtCursorObject = Physics2D.OverlapCircleAll(mouseWorldPoint, 0.01f)
                .Where(collider => collider.GetComponent<INamed>() != null)
                .Select(collider => collider.gameObject)
                .FirstOrDefault();

            if (Input.GetMouseButton(1))
            {
                playerController.Attack(mouseWorldPoint);
            }

            if (Input.GetMouseButtonDown(0))
            {
                playerController.InteractWithMapObjects(mouseWorldPoint);
            }
        }
        else
        {
            uiController.AtCursorObject = null;
        }
    }
}
