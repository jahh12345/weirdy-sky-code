using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Image levelImage;
    public TMP_Text levelDescription;
    public Button nextButton;
    public Button reset;
    public Button backButton;
    public Button playButton;
    public Image levelImagePrev;
    public Image levelImageNext;
    public TMP_Text completeText;
    public SkillData skillData; 
    public TMP_Text[] skillNameTexts; 
    public Image[] skillImages; 
    public TMP_Text[] skillAbilityTexts; 

    private int currentLevel = 0;
    private int totalLevels = 12;

    private string[] levelDescriptions = new string[12]
    {
        "easy",
        "easy",
        "easy",
        "easy",
        "medium",
        "medium",
        "medium",
        "medium",
        "medium",
        "hard",
        "hard",
        "hard"
    };

    private string[] levelImagePaths = new string[12]
    {
        "level1",
        "level2",
        "level3",
        "level4",
        "level5",
        "level6",
        "level7",
        "level8",
        "level9",
        "level10",
        "level11",
        "level12"
    };

    void Start()
    {
        UpdateLevelDisplay();
        nextButton.onClick.AddListener(NextLevel);
        backButton.onClick.AddListener(PreviousLevel);
        playButton.onClick.AddListener(PlayLevel);
        reset.onClick.AddListener(resetall); 

    }

    void resetall(){
        for (int i = 0; i <12; i++){
            ResetLevelClearStatus(i); 
        }
        ResetAllSkills();
    }
    void NextLevel()
    {
        currentLevel = (currentLevel + 1) % totalLevels;
        UpdateLevelDisplay();
    }

    void PreviousLevel()
    {
        currentLevel = (currentLevel - 1 + totalLevels) % totalLevels;
        UpdateLevelDisplay();
    }

    void PlayLevel()
    {
        SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
    }

    void UpdateLevelDisplay()
    {
        levelImage.sprite = Resources.Load<Sprite>(levelImagePaths[currentLevel]);
        levelImagePrev.sprite = Resources.Load<Sprite>(levelImagePaths[(currentLevel - 1 + totalLevels) % totalLevels]);
        levelImageNext.sprite = Resources.Load<Sprite>(levelImagePaths[(currentLevel + 1) % totalLevels]);
        levelDescription.text = levelDescriptions[currentLevel];

        if (PlayerPrefs.GetInt("LevelClear" + (currentLevel + 1), 0) == 1)
        {
            playButton.gameObject.SetActive(false);
            if (completeText != null)
            {
                completeText.gameObject.SetActive(true);
                completeText.text = "Complete";
            }

            UnlockSkill(currentLevel);
        }
        else
        {
            playButton.gameObject.SetActive(true);
            if (completeText != null)
            {
                completeText.gameObject.SetActive(false);
            }
        }
    }

    public void ResetLevelClearStatus(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelClear" + levelIndex, 0);
        PlayerPrefs.Save();
        UpdateLevelDisplay(); 
    }

    public void SetLevelClearStatus(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelClear" + levelIndex, 1);
        PlayerPrefs.Save();
        UpdateLevelDisplay(); 
    }

    void UpdateSkills()
    {
        for (int i = 0; i < skillData.skills.Length; i++)
        {
            if (i < skillNameTexts.Length && i < skillImages.Length && i < skillAbilityTexts.Length)
            {
                if (skillData.skills[i].unlocked)
                {
                    skillNameTexts[i].text = skillData.skills[i].skillName;
                    skillImages[i].sprite = skillData.skills[i].skillImage;
                    skillAbilityTexts[i].text = skillData.skills[i].skillAbility;
                }
                else
                {
                    skillNameTexts[i].text = "Locked";
                    skillImages[i].sprite = null; 
                    skillAbilityTexts[i].text = "";
                }
            }
        }
    }

    void UnlockSkill(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < skillData.skills.Length)
        {
            skillData.skills[levelIndex].unlocked = true;
            PlayerPrefs.SetInt("SkillUnlocked" + levelIndex, 1);
            PlayerPrefs.Save();
            UpdateSkills();
        }
    }

    void LoadSkillUnlockStatus()
    {
        for (int i = 0; i < skillData.skills.Length; i++)
        {
            if (PlayerPrefs.GetInt("SkillUnlocked" + i, 0) == 1)
            {
                skillData.skills[i].unlocked = true;
            }
        }
        UpdateSkills();
    }
    void ResetAllSkills()
{
    for (int i = 0; i < skillData.skills.Length; i++)
    {
        skillData.skills[i].unlocked = false; 
        PlayerPrefs.SetInt("SkillUnlocked" + i, 0); 
    }
    PlayerPrefs.Save();
    UpdateSkills(); 
}
}
