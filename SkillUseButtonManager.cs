using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SkillUseButtonManager : MonoBehaviour
{
    public Button[] skillUseButtons; 
    public TMP_Text[] skillCooldownTexts; 
    public SkillPopupManager skillPopupManager; 
    public EnemyController enemyController; 
    public WizardController wizardController; 

    void Start()
    {
        if (skillPopupManager == null || enemyController == null || wizardController == null)
        {
            Debug.LogError("SkillPopupManager, EnemyController or WizardController is not assigned in SkillUseButtonManager");
            return;
        }

        for (int i = 0; i < skillUseButtons.Length; i++)
        {
            int index = i;
            skillUseButtons[i].onClick.AddListener(() => OnSkillUseButtonClicked(index));
        }

        ResetAllSkillCooldowns();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) OnSkillUseButtonClicked(0); 
        if (Input.GetKeyDown(KeyCode.Alpha2)) OnSkillUseButtonClicked(1); 
        if (Input.GetKeyDown(KeyCode.Alpha3)) OnSkillUseButtonClicked(2); 
        
        if (Input.GetKeyDown(KeyCode.Alpha8)) OnSkillUseButtonClicked(3); 
        if (Input.GetKeyDown(KeyCode.Alpha9)) OnSkillUseButtonClicked(4); 
        if (Input.GetKeyDown(KeyCode.Alpha0)) OnSkillUseButtonClicked(5); 

        for (int i = 0; i < skillPopupManager.selectedSkills.Length; i++)
    {
        Skill skill = skillPopupManager.selectedSkills[i];
        if (skill != null)
        {
            skill.cooldownRemaining -= Time.deltaTime;
            if (skill.cooldownRemaining < 0)
            {
                skill.cooldownRemaining = 0;
            }

            
            if (skillCooldownTexts[i] != null)
            {
                skillCooldownTexts[i].text = skill.cooldownRemaining > 0 ? Mathf.CeilToInt(skill.cooldownRemaining).ToString() : "-";
            }
        }
    }
    }

    private void OnSkillUseButtonClicked(int index)
    {
        if (index >= 0 && index < skillPopupManager.selectedSkills.Length)
        {
            Skill selectedSkill = skillPopupManager.selectedSkills[index];
            if (selectedSkill != null)
            {
                if (selectedSkill.cooldownRemaining <= 0)
                {
                    Debug.Log("Skill used: " + selectedSkill.skillName);
                    if (selectedSkill.type == "attack")
                    {
                        enemyController.TakeDamage(selectedSkill.damage); 
                    }
                    else if (selectedSkill.type == "defend")
                    {
                        wizardController.AddHealth(selectedSkill.damage); 
                    }
                    selectedSkill.cooldownRemaining = selectedSkill.cooldown; 
                }
                else
                {
                    Debug.Log("Skill " + selectedSkill.skillName + " is on cooldown. " +
                              "Time remaining: " + selectedSkill.cooldownRemaining + " seconds");
                }
            }
            else
            {
                Debug.LogWarning("No skill assigned to this button.");
            }
        }
        else
        {
            Debug.LogWarning("Button index out of range.");
        }
    }

    public void ResetAllSkillCooldowns()
    {
        foreach (var skill in skillPopupManager.selectedSkills)
        {
            if (skill != null)
            {
                skill.cooldownRemaining = 0f; 
                Debug.Log($"Skill cooldown reset: {skill.skillName}");
            }
        }
    }
}
