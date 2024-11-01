using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public int baseHealth = 100; 
    public int levelHealth; 
    public int currentHealth;
    public Image healthBarForeground; 
    public GameObject winCanvas; 
    public Transform witchTransform; 
    public Transform wizardTransform; 
    public Collider2D boundary; 
    public float teleportInterval = 3f; 

    private WizardController wizardController; 

    private void Start()
    {
        int levelIndex = GetCurrentLevelIndex();
        levelHealth = baseHealth * levelIndex; 
        currentHealth = levelHealth;
        winCanvas.SetActive(false); 
        InvokeRepeating("TeleportEnemy", teleportInterval, teleportInterval); 

        wizardController = wizardTransform.GetComponent<WizardController>();
    }

    private void Update()
    {
        float distanceToWitch = Vector3.Distance(transform.position, witchTransform.position);
        float distanceToWizard = Vector3.Distance(transform.position, wizardTransform.position);

        if (Input.GetKeyDown(KeyCode.Q) && distanceToWitch <= 500f)
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.P) && distanceToWizard <= 500f)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, levelHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Win();
        }
    }

    private void UpdateHealthBar()
    {
        float healthRatio = (float)currentHealth / levelHealth;
        healthBarForeground.fillAmount = healthRatio; 
    }

    private void Win()
    {
        winCanvas.SetActive(true); 
        if (wizardController != null)
        {
            wizardController.StopHealthDecrease(); 
        }
    }

    private void TeleportEnemy()
    {
        Vector2 newPosition = new Vector2(
            Random.Range(boundary.bounds.min.x, boundary.bounds.max.x),
            Random.Range(boundary.bounds.min.y, boundary.bounds.max.y)
        );

        transform.position = newPosition;
    }

    private int GetCurrentLevelIndex()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.StartsWith("level"))
        {
            string levelNumberString = sceneName.Substring(5); 
            if (int.TryParse(levelNumberString, out int levelNumber))
            {
                return levelNumber;
            }
        }
        return 1; 
    }
}
