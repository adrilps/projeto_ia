 using System;
using UnityEngine;
using UnityEngine.SceneManagement

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    public Character character;
    public Transform pellets;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();        
    }

    private void Update()
    {
        //if (this.lives >= 0 && Input.GetKeyDown(KeyCode.Space))
        //{
            //NewGame();
        //}
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(true);
        }

        this.character.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(false);
        }

        this.character.gameObject.SetActive(false);
    }

    public void EnemyEaten(Enemy Enemy)
    {
        SetLives(this.lives + 1);
    }

    public void CharacterEaten()
    {
        this.character.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(score + 1);

        if (!HasRemainingPellets())
        {
            this.character.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);

        }
    }

    public void PowerPelletEaten(Pellet pellet)
    {
        PelletEaten(pellet);
    }

    private void SetScore (int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
