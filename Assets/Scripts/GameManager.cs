using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Character player;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private int enemyMultiplier = 1;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].ResetState();
        }

        player.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].gameObject.SetActive(false);
        }

        player.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void CharacterEaten()
    {
        player.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void EnemyEaten(Enemy enemy)
    {
        int points = enemy.points * enemyMultiplier;
        SetScore(score + points);

        enemyMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        //SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            player.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetEnemyMultiplier));
        Invoke(nameof(ResetEnemyMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetEnemyMultiplier()
    {
        enemyMultiplier = 1;
    }

}
