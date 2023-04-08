using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using TMPro;

public class PlayerStateMachine : MonoBehaviour
{
	BaseState currentState;
	PlayerController playerController;

	public JumpingState jumpingState;
	public GroundedState groundedState;
	public MovingState movingState;
	
	public TextMeshProUGUI screenText;

	private void Awake()
	{
		playerController = GetComponent<PlayerController>();
		jumpingState = new JumpingState(this);
		groundedState = new GroundedState(this);
		movingState = new MovingState(this);
	}


	// Start is called before the first frame update
	void Start()
	{
		currentState ??= GetInitialState();
		currentState.Enter();
	}

    // Update is called once per frame
    void Update()
    {
		if (currentState != null)
			currentState.UpdateLogic();

		Debug.Log(currentState.StateName);
    }

	void LateUpdate()
	{
		if (currentState != null)
			currentState.UpdatePhysics();
	}


	// ReSharper disable Unity.PerformanceAnalysis
	public void ChangeState(BaseState newState)
	{
		currentState.Exit();

		currentState = newState;

		currentState.Enter();

	}

	public BaseState GetInitialState()
	{
		return groundedState;
	}

	public PlayerController GetPlayerController()
	{
		return playerController;
	}
}
