using JetBrains.Annotations;
using UnityEngine;

public enum ColectableType
{
    Trash,
    Candy
}

public class Colectable : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody rb;
    public ColectableType type;
    public int posIndex {  get; private set; }
    public int scoreValue;

    public void Init(ColectableType type, int posIndex, float speed)
    {
        this.type = type;
        this.posIndex = posIndex;
        this.speed = speed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.linearVelocity = new Vector3(0, 0, -speed);

        if (this.transform.position.z <= -2.7)
        {
            EventBus.Emit<ColectableDestroyedEvent>(new ColectableDestroyedEvent(type, posIndex, scoreValue));
            Destroy(this.gameObject);
        }
    }

}

public class ColectableDestroyedEvent
{
    public ColectableType type;
    public int posIndex;
    public int scoreValue;

    public ColectableDestroyedEvent(ColectableType type, int posIndex, int scoreValue)
    {
        this.type = type;
        this.posIndex = posIndex;
        this.scoreValue = scoreValue;
    }
}
