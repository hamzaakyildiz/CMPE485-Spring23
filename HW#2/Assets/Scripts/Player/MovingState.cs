using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{

    public MovingState(PlayerStateMachine psm) : base("Moving", psm) { }
    

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (playerController.IsJumping())
        {
            playerStateMachine.ChangeState(playerStateMachine.jumpingState);
        }
        else if (playerController.IsGrounded() && !playerController.IsMoving())
        {
            playerStateMachine.ChangeState(playerStateMachine.groundedState);
        }
    }

    public override void UpdatePhysics()
    {
        playerController.Move();
    }
}