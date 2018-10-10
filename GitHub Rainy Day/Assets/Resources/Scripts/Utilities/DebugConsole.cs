using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DebugConsole : MonoBehaviour
{
	public static DebugConsole instance;

	[SerializeField]
	private Player player;
	public bool isPauseGame;
	public bool isStartGame;

	void Awake ()
	{
		instance = this;
	}

	void Update()
	{
		if(player == null)
		player = Player.instance;
	}

    void Start()
    {
		isStartGame = false;
    }

	#region Game State
	public void StartGame()
    {
        print("Start Game!");
		isStartGame = true;
		isPauseGame = false;
       
		if (LevelManager.instance != null)
        {
			LevelManager.instance.ResetAllEnemy();
			LevelManager.instance.ResetLevel();
        }
        else
        {
            print("waveManager is null!");
        }

		if (player != null)
		{
			player.Reset();
		}
		else
		{
			print("player is null!");
		}

		if (Inventory.instance != null)
		{
			Inventory.instance.radialMenu.gameObject.SetActive(true);
			Inventory.instance.ClearAllRadialMenuButton ();
			Inventory.instance.AddSpearRadialMenuButton();
			Inventory.instance.RemoveBowRadialMenuButton();
		}
		else
		{
			print("Inventory is null!");
		}

		UIConsole.instance.ShowMainMenu(false);
		UIConsole.instance.ShowGameOverMenu(false);
		UIConsole.instance.ShowTutorialMenu(false);
		UIConsole.instance.ShowHUD(true);

    }

	public void PauseGame(object sender, ControllerInteractionEventArgs e)
    {
		if (!isStartGame) {
			return;
		}

		if (!isPauseGame) {
			print("Pause Game!");
			UIConsole.instance.InitUI ();
			if (player != null)
			{
				player.ShowVRPointer(true);
				player.spear.enabled = false;
				player.DropAllHandGrab();
			}
			else
			{
				print("player is null!");
			}


			if (LevelManager.instance != null)
			{
				LevelManager.instance.Stop();
				LevelManager.instance.PauseAllEnemy();
			}
			else
			{
				print("LevelManager is null!");
			}
		}
    }

	public void Released(object sender, ControllerInteractionEventArgs e)
	{
		if (!isStartGame) {
			return;
		}

		if (isPauseGame) {
			isPauseGame = false;
		} else {
			isPauseGame = true;
		}
	}

	public void ResumeGame(object sender, ControllerInteractionEventArgs e)
	{
		if (!isStartGame) {
			return;
		}

		if (isPauseGame) {
			print("Resume Game!");
			if (player != null)
			{
				player.ShowVRPointer(false);
				player.spear.enabled = true;
			}
			else
			{
				print("player is null!");
			}

			if (LevelManager.instance != null)
			{
				LevelManager.instance.ResumeLevel();
				LevelManager.instance.ResumeAllEnemy();
			}
			else
			{
				print("LevelManager is null!");
			}

			if (UIConsole.instance != null)
			{
				UIConsole.instance.ShowMainMenu(false);
				UIConsole.instance.ShowHUD(true);
			}
			else
			{
				print("UIConsole is null!");
			}
		}

	}

	public void GameOver()
	{
		UIConsole.instance.ShowGameOverMenu(true);
		UIConsole.instance.ShowMainMenu (false);

		if (player != null)
		{
			player.GetComponent<Player> ().enabled = false;
			player.spear.enabled = false;
			player.DropAllHandGrab();
		}
		else
		{
			print("player is null!");
		}

		if (LevelManager.instance != null)
		{
			LevelManager.instance.GetEnemyCheerWorlds ();
			LevelManager.instance.Stop();
		}
		else
		{
			print("LevelManager is null!");
		}
			
		player.ShowVRPointer(true);
		player.ShowColliderHeadQuiver(false);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
	#endregion

	#region UI Control
    public void ReturnToMainMenu()
    {
		UIConsole.instance.ShowTutorialMenu(false);
        UIConsole.instance.ShowMainMenu(true);
		UIConsole.instance.ShowGameOverMenu(false);
    }
		
	public void ToTutorial()
	{
		UIConsole.instance.ShowTutorialMenu(true);
		UIConsole.instance.ShowMainMenu(false);
	}
	#endregion

}
