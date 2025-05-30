using UnityEngine;

public enum AugmentType { AttackPower, AttackSpeed, CritChance, CritDamage }

[CreateAssetMenu(fileName = "Augment", menuName = "Augment/AugmentData")]
public class AugmentData : ScriptableObject
{
    [SerializeField] private string augmentname;
    public string AugmentName => augmentname;

    [SerializeField] private AugmentType type;
    public AugmentType Type => type;

    [SerializeField, TextArea] private string description;
    public string Description => description;

    [Header("증가 수치 범위")]
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    [HideInInspector] public float actualValue;

    public void RollValue()
    {
        actualValue = Random.Range(minValue, maxValue) / 100;
    }

    public string GetDisplayValue()
    {
        return type switch
        {
            AugmentType.CritDamage => $"+{actualValue:F1}",
            _ => $"+{actualValue * 100f:F0}%",
        };
    }
}