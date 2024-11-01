using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WizardController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public BoxCollider2D boundaryCollider; 
    public Image healthBarForeground; 

    private Rigidbody2D rb;
    private float maxHealth = 100f; 
    public float currentHealth; 
    private float healthDecreaseInterval = 1f; 
    private bool isHealthDecreasing = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; 
        InvokeRepeating("DecreaseHealth", healthDecreaseInterval, healthDecreaseInterval); 
    }

    void Update()
    {
        Move();
        UpdateHealthBar();
    }

    void Move()
    {
        float moveX = Input.GetAxis("HorizontalWizard");
        float moveY = Input.GetAxis("VerticalWizard");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;

        if (IsWithinBoundary(newPosition))
        {
            rb.MovePosition(newPosition);
        }
    }

    bool IsWithinBoundary(Vector2 position)
    {
        Bounds boundaryBounds = boundaryCollider.bounds;
        return boundaryBounds.Contains(position);
    }

    void DecreaseHealth()
    {
        if (isHealthDecreasing)
        {
            currentHealth -= 1f;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
            if (currentHealth <= 0)
            {
                CancelInvoke("DecreaseHealth"); 
                LoadPlayScene(); 
            }
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            float healthRatio = currentHealth / maxHealth;
            healthBarForeground.fillAmount = healthRatio;
        }
    }

    void LoadPlayScene()
    {
        SceneManager.LoadScene("home");
    }

    public void StopHealthDecrease()
    {
        isHealthDecreasing = false; 
    }

    public void AddHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        UpdateHealthBar(); 
    }
}
