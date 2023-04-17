using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	[SerializeField]
	State state;

	[SerializeField]
	Player player1;

	[SerializeField]
	Player player2;

	[SerializeField]
	bool isPlayer1DoneSelecting;

	[SerializeField]
	bool isPlayer2DoneSelecting;

	[SerializeField]
	bool isAttackDone;

	[SerializeField]
	bool isDamagingDone;

	[SerializeField]
	bool isReturningDone;

	[SerializeField]
	bool isPlayerEliminated;

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
				player1.Prepare();
				player2.Prepare();

				player1.SetPlay(true);
				player2.SetPlay(false);
				state = State.Player1Select;
				break;

			case State.Player1Select:
				if (player1.SelectedCharacter != null)
				{
					player1.SetPlay(false);
					player2.SetPlay(true);
					state = State.Player2Select;
				}
				break;

			case State.Player2Select:
				if (player2.SelectedCharacter != null)
				{
					player1.Attack();
					player2.Attack();
					state = State.Attacking;
				}
				break;

			case State.Attacking:
				if (player1.IsAttacking() == false && player2.IsAttacking() == false)
				{
					CalculateBattle(player1, player2, out Player winner, out Player loser);

					state = State.Damaging;
				}
				break;

			case State.Damaging:
				if (isDamagingDone)
				{
					state = State.Returning;
				}
				break;

			case State.Returning:
				if (isReturningDone)
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
	
	private void CalculateBattle(Player player1, Player player2,out Player winner, out Player loser)
	{
		var type1 = player1.SelectedCharacter.Type;
		var type2 = player2.SelectedCharacter.Type;
		
		if(type1 == CharacterType.Paper && type2 == CharacterType.Rock)
		{
			winner = player1;
			loser = player2;
		}
		else if(type1 == CharacterType.Rock && type2 == CharacterType.Paper)
		{
			winner = player2;
			loser = player1;
		}
		else if(type1 == CharacterType.Rock && type2 == CharacterType.Scissors)
		{
			winner = player1;
			loser = player2;
		}
		else if(type1 == CharacterType.Scissors && type2 == CharacterType.Rock)
		{
			winner = player2;
			loser = player1;
		}
		else if(type1 == CharacterType.Scissors && type2 == CharacterType.Paper)
		{
			winner = player1;
			loser = player2;
		}
		else if(type1 == CharacterType.Paper && type2 == CharacterType.Scissors)
		{
			winner = player2;
			loser = player1;
		}
		else
		{
			winner = null;
			loser = null;
		}
	}
}
