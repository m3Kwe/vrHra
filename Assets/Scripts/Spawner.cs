using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] beltSpawnPoints;
    [SerializeField] private GameObject trash;
    [SerializeField] private GameObject candy;

    [SerializeField] private float SPAWN_SPEED_INTERVAL;
    [SerializeField] private float SPAWN_SPEED_INTERVAL_INCREASE;
    [SerializeField] private float START_SPEED;
    [SerializeField] private float TRASH_WEIGHT;
    [SerializeField] private float CANDY_WEIGHT;

    [SerializeField] private int GAME_TIMER;
    [SerializeField] private float CURRENT_GAME_TIMER;

    private bool gameRunning = false;
    private float timer = 0f;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameRunning = true;
        timer = 0f;
        CURRENT_GAME_TIMER = GAME_TIMER;
    }

    public void StopGame()
    {
        gameRunning = false;
    }

    public void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            if (timer >= SPAWN_SPEED_INTERVAL)
            {
                Spawn();
                timer = 0f;
            }

            CURRENT_GAME_TIMER += Time.deltaTime;
            if (CURRENT_GAME_TIMER >= 1)
            {
                GAME_TIMER--;
                CURRENT_GAME_TIMER = 0;
                SPAWN_SPEED_INTERVAL *= SPAWN_SPEED_INTERVAL_INCREASE;
            }
            if (GAME_TIMER <= 0)
            {
                StopGame();
                //***************************************KONIEC HRY*****************************************
                //Tu si loadni dalsiu scenu)
            }
        }
    }

    private void Spawn()
    {
        float type = Random.value;
        int spawnPoint = Random.Range(0, beltSpawnPoints.Length);
        GameObject g;
        if (type <= 1 / (TRASH_WEIGHT + CANDY_WEIGHT) * TRASH_WEIGHT)
        {
            g = SpawnObject(trash, beltSpawnPoints[spawnPoint].transform.position);
            g.GetComponent<Colectable>().Init(ColectableType.Trash, spawnPoint, START_SPEED);
        }
        else
        {
            g = SpawnObject(candy, beltSpawnPoints[spawnPoint].transform.position);
            g.GetComponent<Colectable>().Init(ColectableType.Candy, spawnPoint, START_SPEED);
        }

    }

    private GameObject SpawnObject(GameObject obj, Vector3 position)
    {
        GameObject g = Instantiate(obj, this.transform);
        g.transform.position = position;
        return g;
    }
}
