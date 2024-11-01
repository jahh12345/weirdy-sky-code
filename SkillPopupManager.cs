using UnityEngine;
using UnityEngine.UI;

public class SkillPopupManager : MonoBehaviour
{
    public GameObject skillPopupPanel;
    public Button[] skillButtons; 
    public Button confirmButton; 
    public Button[] skillUseButtons; 
    public SkillData skillData; 
    

    public Skill[] selectedSkills = new Skill[6]; 

    private int maxAttackSkills = 3;
    private int maxDefendSkills = 3;

    void Start()
    {
        UpdateSkillButtons();
        ShowPopup();

        confirmButton.onClick.AddListener(HidePopup);
        UpdateSkillUseButtons();
    }

    void UpdateSkillButtons()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            Button button = skillButtons[i];
            Image buttonImage = button.GetComponent<Image>();
            Skill skill = skillData.skills[i];

            if (skill.unlocked)
            {
                buttonImage.sprite = skill.skillImage;
                button.interactable = true;

                buttonImage.color = skill.selected ? Color.green : Color.white;

                int index = i;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnSkillButtonClicked(index));
            }
            else
            {
                buttonImage.color = Color.black;
                buttonImage.sprite = null;
                button.interactable = false;
            }
        }
    }

    void OnSkillButtonClicked(int index)
    {
        Skill skill = skillData.skills[index];

        if (skill.selected)
        {
            skill.selected = false;
        }
        else
        {
            int selectedAttackSkills = 0;
            int selectedDefendSkills = 0;

            foreach (Skill s in skillData.skills)
            {
                if (s.selected)
                {
                    if (s.type == "attack") selectedAttackSkills++;
                    else if (s.type == "defend") selectedDefendSkills++;
                }
            }

            if (skill.type == "attack" && selectedAttackSkills < maxAttackSkills)
            {
                skill.selected = true;
            }
            else if (skill.type == "defend" && selectedDefendSkills < maxDefendSkills)
            {
                skill.selected = true;
            }
        }

        UpdateSkillButtons();
        UpdateSkillUseButtons();
    }

    void UpdateSkillUseButtons()
    {
        Skill[] tempSelectedSkills = new Skill[6];
        int attackIndex = 0;
        int defendIndex = 0;

        foreach (Skill skill in skillData.skills)
        {
            if (skill.selected)
            {
                if (skill.type == "attack" && attackIndex < maxAttackSkills)
                {
                    tempSelectedSkills[attackIndex] = skill;
                    attackIndex++;
                }
                else if (skill.type == "defend" && defendIndex < maxDefendSkills)
                {
                    tempSelectedSkills[3 + defendIndex] = skill;
                    defendIndex++;
                }
            }
        }

        for (int i = 0; i < skillUseButtons.Length; i++)
        {
            Button button = skillUseButtons[i];
            Image buttonImage = button.GetComponent<Image>();
            if (i < tempSelectedSkills.Length && tempSelectedSkills[i] != null)
            {
                buttonImage.sprite = tempSelectedSkills[i].skillImage;
                buttonImage.color = Color.white;
                button.interactable = true;
            }
            else
            {
                buttonImage.sprite = null;
                buttonImage.color = Color.gray;
                button.interactable = false;
            }
        }

        selectedSkills = tempSelectedSkills;
    }

    public void ShowPopup()
    {
        skillPopupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        skillPopupPanel.SetActive(false);
        Transform canvas = skillPopupPanel.transform.parent;
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
