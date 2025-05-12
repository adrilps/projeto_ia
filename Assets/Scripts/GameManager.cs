using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Character player;
    [SerializeField] private Transform pellets;
    //[SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;
    public static int currentLevel = 1;

    private int enemyMultiplier = 1;
    private bool isResetting = false;
    private bool isDead = false;
    private float resetDelay = 2f;

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
        // Vidas são resetadas ao entrar em uma nova fase
        SetLives(3);
        NewRound();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            SceneManager.LoadScene(0); // Volta para o menu principal
        }
    }

    private void NewRound()
    {
        isResetting = false;
        isDead = false;

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        if (isResetting) return;
        isResetting = true;

        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].ResetState();
        }
        player.ResetState();
        isResetting = false;
        isDead = false;
    }

    private void GameOver()
    {
        SceneManager.LoadScene(8); // Game Over
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        //scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void CharacterEaten()
    {
        if (isResetting || isDead) return;
        isDead = true;
        player.gameObject.SetActive(false);
        SetLives(lives - 1);
        if (lives > 0) {
            Invoke(nameof(ResetState), resetDelay);
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
        if (!HasRemainingPellets())
        {
            player.gameObject.SetActive(false);
            // Avança para o tutorial da próxima fase
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                SceneManager.LoadScene(0); // Volta para o menu principal se não houver próxima fase
            }
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
