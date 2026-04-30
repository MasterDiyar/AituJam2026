using Godot;

[GlobalClass]
public partial class UnitResource : Resource
{
    [Export] public Texture2D Body, Cloth, Head, Hat, Weapon, Secondary;
    public void SetUnitBody(UnitBody unitBody)
    {
        unitBody.Body.Texture = Body;
        unitBody.Cloth.Texture = Cloth;
        unitBody.Head.Texture = Head;
        unitBody.Weapon.Texture = Weapon;
        unitBody.Secondary.Texture = Secondary;
        unitBody.Hat.Texture = Hat;
    }
}