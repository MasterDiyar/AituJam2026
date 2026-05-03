using Godot;
using System;
using System.Collections.Generic;
using AITUJAM2026.scripts.unit;

public partial class TestControll : Node
{
    private Deck deck;
    [Export] Button forward, backward, attack, heal;
    
    private List<Button> _allButtons;
    private Random _rng = new();

    private Dictionary<Button, UnitActions> _buttonActions = new();

    public override void _Ready()
    {
        deck = GetParent<Deck>();
        _allButtons = new List<Button> { forward, backward, attack, heal };
        foreach (var btn in _allButtons)
            RandomizeButton(btn);

        forward.Pressed += () => OnButtonPressed(forward);
        backward.Pressed += () => OnButtonPressed(backward);
        heal.Pressed += () => OnButtonPressed(heal);
        attack.Pressed += () => OnButtonPressed(attack);
    }

    private void RandomizeButton(Button btn)
    {
        var allActions = Enum.GetValues<UnitActions>();
        UnitActions newAction = allActions[_rng.Next(allActions.Length)];
        
        _buttonActions[btn] = newAction;
        btn.Text = newAction.ToString();
    }

    private void OnButtonPressed(Button pressedButton)
    {
        UnitActions currentAction = _buttonActions[pressedButton];
        
        deck.SetAction(currentAction);
        GD.Print($"Выполнено: {currentAction}");

        foreach (var btn in _allButtons)
            btn.Disabled = true;

        Tween timerTween = CreateTween();
        timerTween.TweenInterval(2.0f);
        timerTween.Finished += () => {
            foreach (var btn in _allButtons) {
                btn.Disabled = false;
                if (btn == pressedButton) 
                    RandomizeButton(btn);
                
            }
        };
    }
}