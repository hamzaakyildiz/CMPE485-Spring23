using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseState
{

	public JumpingState(PlayerStateMachine psm) : base("Jumping", psm) { }

	public override void Enter()
	{
		playerController.Jump();
	}

	public override void UpdateLogic()
	{
		if (playerController.IsGrounded() && playerController.IsMoving())
		{
			playerStateMachine.ChangeState(playerStateMachine.movingState);
		}
		else if (playerController.IsGrounded())
		{
			playerStateMachine.ChangeState(playerStateMachine.groundedState);
		}
	}

	public override void UpdatePhysics()
	
	{
		if (playerController.GetVerticalVelocity() < 0) //falling, so accelerate faster for better gamefeel
		{
			playerController.AddGravity(3f);
		} else
		{
			playerController.AddGravity(0.5f);
		}
		
	}
}
