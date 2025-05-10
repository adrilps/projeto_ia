using System.Collections;
using UnityEngine;

public class EnemyHome : EnemyBehavior
{
    public Transform inside;
    public Transform outside;
    private bool isInitialized = false;

    private void Start()
    {
        // Inicialização acontece no Start, que é chamado após todos os Awake
        StartCoroutine(InitializeAfterEnemy());
    }

    private IEnumerator InitializeAfterEnemy()
    {
        // Aguardar um frame para garantir que tudo foi inicializado
        yield return null;

        if (enemy == null)
        {
            Debug.LogError("Componente Enemy não encontrado!", this);
            yield break;
        }

        if (enemy.movement == null)
        {
            Debug.LogError("Componente Movement não encontrado no Enemy!", this);
            yield break;
        }

        if (enemy.movement.rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no Movement!", this);
            yield break;
        }

        isInitialized = true;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        // Só executa a transição se tudo estiver inicializado
        if (isInitialized && gameObject.activeInHierarchy) {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction everytime the enemy hits a wall to create the
        // effect of the enemy bouncing around the home
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            enemy.movement.SetDirection(-enemy.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
{
    if (enemy == null || enemy.movement == null || enemy.movement.rb == null || inside == null || outside == null)
    {
        Debug.LogError("Algum componente necessário está nulo!", this);
        yield break;
    }
    
    // Turn off movement while we manually animate the position
    enemy.movement.SetDirection(Vector2.up, true);
    enemy.movement.rb.bodyType = RigidbodyType2D.Kinematic; // Alterado de isKinematic = true
    enemy.movement.enabled = false;

    Vector3 position = transform.position;

    float duration = 0.5f;
    float elapsed = 0f;

    // Animate to the starting point
    while (elapsed < duration)
    {
        enemy.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
        elapsed += Time.deltaTime;
        yield return null;
    }

    elapsed = 0f;

    // Animate exiting the enemy home
    while (elapsed < duration)
    {
        enemy.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
        elapsed += Time.deltaTime;
        yield return null;
    }

    // Pick a random direction left or right and re-enable movement
    enemy.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
    enemy.movement.rb.bodyType = RigidbodyType2D.Dynamic; // Alterado de isKinematic = false
    enemy.movement.enabled = true;
}

}
