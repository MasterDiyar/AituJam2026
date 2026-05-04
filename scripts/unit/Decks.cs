using Godot;

namespace AITUJAM2026.scripts.unit;
using UA = UnitActions;
public static class Decks
{
    public static UnitBuilder
            kopie_ton = GD.Load<UnitBuilder>("res://scenes/prefabs/kopie_ton_builder.tres"),
            bowm_an = GD.Load<UnitBuilder>("res://scenes/prefabs/bowm_an_builder.tres"),
            topo_ric = GD.Load<UnitBuilder>("res://scenes/prefabs/topo_ric_builder.tres"),
            mech_nick = GD.Load<UnitBuilder>("res://scenes/prefabs/mech_nick_builder.tres"),
            farmberg = GD.Load<UnitBuilder>("res://scenes/prefabs/fer_mer.tres"),
            zweih_an = GD.Load<UnitBuilder>("res://scenes/prefabs/zweiHandler.tres");
    
    public static UnitBuilder WithCount(UnitBuilder builder, int count)
    {
        var newBuilder = (UnitBuilder)builder.Duplicate();
        newBuilder.count = count;
        return newBuilder;
    }
    
    public static readonly Godot.Collections.Array<UA>[] PreMadeActions =
    [
        [UA.GoForward, UA.Attack, UA.Attack, UA.Idle],
        [UA.Rush, UA.GoBackward, UA.Heal],
        [UA.Attack, UA.Heal, UA.GoForward, UA.Attack, UA.GoBackward],
        [UA.GoForward, UA.Attack],
        [UA.Attack, UA.Heal],
        [UA.Rush, UA.GoBackward],
        [UA.GoForward, UA.Rush, UA.GoForward]
    ];
    
    public static readonly Godot.Collections.Array<UnitBuilder>[] PreMadeUnitDecks= [
        [WithCount(kopie_ton, 4)], 
        [WithCount(bowm_an, 2), WithCount(kopie_ton, 3)],
        [WithCount(bowm_an, 3), WithCount(topo_ric, 3)],
        [WithCount(topo_ric, 5), WithCount(mech_nick, 4)],
        [WithCount(farmberg, 4), WithCount(farmberg, 4), WithCount(farmberg, 4), WithCount(farmberg, 4)],
        [WithCount(bowm_an, 6),WithCount(zweih_an, 3)]
    ];
}