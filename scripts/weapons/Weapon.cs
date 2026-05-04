using AITUJAM2026.scripts.unit;
using Godot;

namespace AITUJAM2026.scripts;

public partial class Weapon : Node2D
{
    public enum AttackType
    {
        Pike,
        Bow,
        Sword
    }

    private AttackType type;
    [Export] private Unit Parent;
    [Export] public WeaponResource WeaponInstance;
    [Export] AudioStreamPlayer2D Audio;

    private float attackTime;
    public override void _Process(double delta)
    {
        attackTime += (float)delta;
        
    }

    public void Execute(float angle)
    {
        Audio.VolumeDb = GameManager.Instance.Game.Audio.GetVolumeDb();
        
        type = WeaponInstance.AttackType;
        if (attackTime < WeaponInstance.AttackSpeed) return;
        Audio.Stream = WeaponInstance.Sound;
        Audio.Play();
        attackTime =  0.0f;
        Parent.Body.OnAttack(angle, WeaponInstance.AttackDistance, WeaponInstance.AttackSpeed*0.66f, type, WeaponInstance.Bullet.SpawnAngle);
        for (int i = 0; i < WeaponInstance.Count; i++) {
            var bullet = WeaponInstance.Bullet.BulletResourceScene.Instantiate<Bullet>();
            bullet.BulletResource = WeaponInstance.Bullet;
            bullet.hozyain = Parent.UnitFaction;
            bullet.Rotation = angle + i * WeaponInstance.BetWeenAngle;
            bullet.GlobalPosition = Parent.Body.Weapon.GlobalPosition + Vector2.FromAngle(bullet.Rotation) * WeaponInstance.SpawnOffset;
            bullet.Damage = WeaponInstance.Damage + GameManager.Instance.AddictiveDamage;
            GameManager.Instance.Pausable.AddChild(bullet);
        }
    }

    public void Execute(Unit unit)
    {
        var angle = (unit.GlobalPosition - GlobalPosition).Angle();
        Execute(angle);
    }
}