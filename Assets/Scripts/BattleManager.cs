using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] State state;
    [SerializeField] bool isPlayer1DoneSelecting;
    [SerializeField] bool isPlayer2DoneSelecting;
    [SerializeField] bool isAttackDone;
    [SerializeField] bool isDamagingDone;
    [SerializeField] bool isReturningDone;
    [SerializeField] bool isPlayerEliminated;

    enum State
    {
        Preparation,
        Player1Select,
        Player2Select,
        Attacking,
        Damaging,
        Returning,
        BattleOver
    }

    void Update()
    {
        switch (state)
        {
            case State.Preparation:
                state = State.Player1Select;
                break;

            case State.Player1Select:
                if (isPlayer1DoneSelecting)
                {
                    state = State.Player2Select;
                }
                break;

            case State.Player2Select:
                if (isPlayer2DoneSelecting)
                {
                    state = State.Attacking;
                }
                break;

            case State.Attacking:
                if (isAttackDone)
                {
                    state = State.Damaging;
                }
                break;

            case State.Damaging:
                if(isDamagingDone)
                {
                    state = State.Returning;
                }
                break;

            case State.Returning:
                if(isReturningDone)
                {
                    if (isPlayerEliminated)
                        state = State.BattleOver;
                    else
                        state = State.Preparation;
                }
                break;

            case State.BattleOver: 
                break;
        }
    }
}
