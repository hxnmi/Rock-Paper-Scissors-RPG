
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
	[SerializeField] Character selectedCharacter;
	[SerializeField] List<Character> characterList;
	[SerializeField] Transform atkRef;

	public Character SelectedCharacter { get => selectedCharacter; }

	public void Prepare()
	{
		selectedCharacter = null;
	}

	public void SelectCharacter(Character character)
	{
		selectedCharacter = character;
	}

	public void SetPlay(bool value)
	{
		foreach (var character in characterList)
		{
			character.Button.interactable = value;
		}
	}
	
	public void Attack()
	{
		selectedCharacter.transform
			.DOMove(atkRef.position, 1f);
	}

	public bool IsAttacking()
	{
		return DOTween.IsTweening(selectedCharacter.transform);
	}
	
	public void TakeDamage(int damageValue)
	{
		selectedCharacter.ChangeHP(-damageValue);
		var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
		spriteRend.DOColor(Color.red, 0.1f).SetLoops(6, LoopType.Yoyo);
		
	}
	
	public bool IsDamaging()
	{
		var spriteRend = selectedCharacter.GetComponent<SpriteRenderer>();
		return DOTween.IsTweening(spriteRend);
	}
	
	public void Remove(Character character)
	{
		if(characterList.Contains(character) == false)
		 	return;
		character.Button.interactable = false;
		character.gameObject.SetActive(false);
		characterList.Remove(character);
	}
}
