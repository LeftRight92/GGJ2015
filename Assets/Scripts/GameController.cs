using UnityEngine;
using System.Collections;

public static class GameController {

	private static PlayerController playerController;


	public static void registerPlayer(PlayerController player)
	{
		playerController = player;
	}

	public static PlayerController getPlayer()
	{
		return playerController;	
	}

}
