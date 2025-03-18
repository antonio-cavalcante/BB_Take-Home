using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateScriptableObjects", menuName = "Scriptable Objects/PlayerStateScriptableObjects")]
public class PlayerStateScriptableObject : ScriptableObject
{
    [SerializeField] private string stateName;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float size = 1f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool steathMode = false;
    [SerializeField] private bool radialAttack = false;

    public string StateName => stateName;
    public float MaxHealth => maxHealth;
    public int AttackDamage => attackDamage;
    public float MovementSpeed => movementSpeed;
    public float Size => size;
    public bool CanAttack => canAttack;
    public bool StealthMode => steathMode;
    public bool RadialAttack => radialAttack;
    public float Mass => mass;
}
