using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	Default,
	HasPollen
}

public class PlayerData : MonoBehaviour
{
	public PlayerState state = PlayerState.Default;
}
