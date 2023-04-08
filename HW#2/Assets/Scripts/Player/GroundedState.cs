using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GroundedState : BaseState
{

	public GroundedState(PlayerStateMachine psm) : base("Grounded", psm) { }
	
	public override void Enter()
	{
		playerController.playerAnimations.ChangeAnimation("Grounded");
	}

	public override void UpdateLogic()
	{
		if (playerController.IsJumping())
		{
			playerStateMachine.ChangeState(playerStateMachine.jumpingState);
		}
		else if(playerController.IsMoving())
		{
			playerStateMachine.ChangeState(playerStateMachine.movingState);
		}
	}

}
