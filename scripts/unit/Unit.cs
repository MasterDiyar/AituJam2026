using AITUJAM2026.scripts.interfaces;
using Godot;

namespace AITUJAM2026.scripts.unit;

public partial class Unit : CharacterBody2D, IDamagable
{
    [Export] public UnitBody Body;
    [Export] public AboutResource Stats;
    [Export] public Faction UnitFaction { get; set; }
    [Export] public UnitController Controller;
    [Export] public Weapon Weapon;
    [Export] AudioStreamPlayer2D Audio;

    
    public float MaxHp, MaxArmor, MaxAbsorbtion, DefaultDamage, KritChance, KritDamage, MaxSpeed;
    public float Hp, Armor, Absorbtion, Damage, Speed;

    public override void _Ready()
    {
        Stats?.SetStats(this);
        InputEvent += OnInputEvent;
    }

    private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (@event.IsActionPressed("lm"))
            GD.Print("Hp: " + Hp);
        
    }

    public void TakeDamage(float damage)
    {
        var armorMultiplier = 100f / (100f + Mathf.Max(Armor, 0));
        damage -= Absorbtion;
        Hp -= damage * armorMultiplier;
        GD.Print("Hp: " + Hp);
        
        if (Hp <= 0) ExecuteDeath();
    }

    public void Heal(float heal)
    {
        Hp = Mathf.Min(Hp + heal, MaxHp);
    }

    public override void _Process(double delta)
    {
        Audio.VolumeDb = GameManager.Instance.Game.Audio.GetVolumeDb();
        switch (Velocity.Length() > 0.3f) {
            case true when !Audio.Playing: Audio.Play(); break;
            case false when Audio.Playing: Audio.Stop(); break;
        }
        
    }

    public void ExecuteDeath()
    {
        var gay = GetParent().GetParent<Game>();
        if (UnitFaction == Faction.Enemy) gay.enemyUnitCount--;
        if (UnitFaction == Faction.Player) gay.playerUnitCount--;
        gay.OneDie();
        QueueFree();
    }
}