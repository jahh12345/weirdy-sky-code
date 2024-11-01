using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public Sprite skillImage;
    public string skillAbility;
    public int damage; 
    public string type; 
    public bool unlocked;
    public bool selected; 
    public float cooldown; 
    public float cooldownRemaining; 
}
