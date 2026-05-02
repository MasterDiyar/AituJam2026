using Godot;

namespace AITUJAM2026.scripts.unit;
using UA = UnitActions;
public static class Decks
{
    
    public static Godot.Collections.Array<UA>[] PreMadeActions =
    [
        [UA.GoForward, UA.Attack, UA.Attack, UA.Idle],
        [UA.Rush, UA.GoBackward, UA.Heal],
        [UA.Attack, UA.Heal, UA.GoForward, UA.Attack, UA.GoBackward],
        [UA.GoForward, UA.Attack],
        [UA.Attack, UA.Heal],
        [UA.Rush, UA.GoBackward]
    ];

    public static UnitBuilder[] PreMadeUnits =
    [
        
    ];
    
    public static Godot.Collections.Array<UnitBuilder>[] PreMadeUnitDecks= [
    []
    ];
}