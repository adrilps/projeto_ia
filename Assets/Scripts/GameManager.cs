using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enemy[] enemies;
    public character character;
    public Transform pellets;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();        
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

    private void SetScore (int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }
}
