using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyBehavior : MonoBehaviour
{
    public Enemy enemy { get; private set; }
    public float duration;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }

}
