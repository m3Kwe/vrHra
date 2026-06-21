using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int currentPosIndex = 0;
    [SerializeField] private Transform[] positions;

    private void Start()
    {
        this.transform.position = positions[currentPosIndex].position;
        EventBus.On<ColectableDestroyedEvent>(Collect);
    }

    public void Collect(ColectableDestroyedEvent colectableDestroyedEvent)
    {
        if (colectableDestroyedEvent.posIndex == currentPosIndex)
        {
            EventBus.Emit(new OnCollectableCollected(colectableDestroyedEvent.type, colectableDestroyedEvent.posIndex, colectableDestroyedEvent.scoreValue));
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        Debug.Log("MoveLeft called");
        if (context.performed)
        {
            if (currentPosIndex < positions.Length - 1)
            {
                currentPosIndex++;
                this.transform.position = positions[currentPosIndex].position;
            }
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentPosIndex > 0)
            {
                currentPosIndex--;
                this.transform.position = positions[currentPosIndex].position;
            }
        }
    }
}

public class OnCollectableCollected
{
    public ColectableType type;
    public int posIndex;
    public int scoreValue;
    public OnCollectableCollected(ColectableType type, int posIndex, int scoreValue)
    {
        this.type = type;
        this.posIndex = posIndex;
        this.scoreValue = scoreValue;
    }
}
