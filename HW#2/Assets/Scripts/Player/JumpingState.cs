namespace Player
{
	public class JumpingState : BaseState
	{

		public JumpingState(PlayerStateMachine psm) : base("Jumping", psm) { }

		public override void Enter()
		{
			//playerController.playerAnimations.ChangeAnimation("Jumping");

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
			playerController.Move();
			playerController.AddGravity(playerController.GetVerticalVelocity() < 0 ? 3f : 0.5f); //falling, so accelerate faster for better game feel
		}
	}
}
