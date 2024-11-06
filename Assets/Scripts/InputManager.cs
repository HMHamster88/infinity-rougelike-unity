using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameSaveManager gameSaveManager;

    private Vector2 walkDirection = Vector2.zero;
    private bool attacking = false;
    private void FixedUpdate()
    {
        if (!uiController.AnyWindowShown)
        {
            playerController.SetWalkDirection(walkDirection);
        }
    }

    void Update()
    {
        var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        if (!uiController.AnyWindowShown)
        {
            uiController.AtCursorObject = Physics2D.OverlapCircleAll(mouseWorldPoint, 0.01f)
                .Where(collider => collider.GetComponent<INamed>() != null)
                .Select(collider => collider.gameObject)
                .FirstOrDefault();
            if (attacking)
            {
                playerController.Attack(mouseWorldPoint);
            }

        }
        else
        {
            uiController.AtCursorObject = null;
        }
    }

    public void OnInventory()
    {
        uiController.HideShowCharacterWindow();
    }

    public void OnMove(InputValue value)
    {
        walkDirection = value.Get<Vector2>().normalized;
    }

    public void OnInteract()
    {
        var mouseWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

        if (!uiController.AnyWindowShown)
        {
            playerController.InteractWithMapObjects(mouseWorldPoint);
        }
    }

    public void OnAttack(InputValue value)
    {
        attacking = value.isPressed;
        Debug.Log("Attacking = " + attacking);
    }

    public void OnEscape()
    {
        uiController.Escape();
    }

    public void OnMap()
    {
        uiController.HideShowMapWindow();
    }

    public void OnSaveGame()
    {
        gameSaveManager.SaveCurrentGame();
    }

    public void OnLoadGame()
    {
        gameSaveManager.LoadCurrentGame();
    }
}