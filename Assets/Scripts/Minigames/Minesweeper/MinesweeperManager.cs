using System.Collections.Generic;
using UnityEngine;

public class MinesweeperManager : MonoBehaviour
{
    public static MinesweeperManager Instance;

    [SerializeField] MineTile[] tiles; //Default array of tiles

    public List<MineTile> tilesList = new();

    public bool bBombHit;


    private void Awake()
    {
        Instance = this; //Set the instance
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //Setup the tile list
    public void SetupTileList(MineTile inStartTile)
    {
        //Loop through the array and add the tiles appropriately
        for (int tile = 0; tile < tiles.Length; tile++)
        {
            tilesList.Add(tiles[tile]); //Add the tile to the list
        }

        SetupBoard(inStartTile); //Setup the board to play
    }


    //Randomize Bombs 9x9 board [10 bombs]
    public void SetupBoard(MineTile inStartTile)
    {

        for (int bombSet = 0; bombSet < 10; bombSet++)
        {
            int chosenTile = Random.Range(0, tilesList.Count); //Grab a random tile from the list

            //If it lands on the starting tile, redo
            if (tilesList[chosenTile] == inStartTile)
            {
                bombSet--;
                continue;
            }

            tilesList[chosenTile].bHasBomb = true; //Set this tile to have a bomb
            tilesList.RemoveAt(chosenTile);
        }

        inStartTile.TileInteraction();
    }


    //Public function to remove a tile from the list
    public void RemoveTileFromList(MineTile inTileToRemove)
    {
        tilesList.Remove(inTileToRemove);

        CheckForCompletion();
    }

    //Function to clear the board when enough tiles are displayed
    public void CheckForCompletion()
    {
        if (tilesList.Count is 0)
        {
            Debug.Log("Board Clear");
            PlayerScript.instance.RemoveSleepTime(30); //Wake the player back up

            Invoke("ResetBoard", 1); //Reset the board after a second
        }
    }

    //Function to reset the board
    void ResetBoard()
    {
        //Loop through every tile and reset it
        foreach (var tile in tiles)
        {
            tile.ResetTile();
        }

        bBombHit = false; //Reset bomb hit

        tilesList.Clear(); //Clear the list
    }

    //Display all bombs
    public void DisplayBombs()
    {
        //Set the bomb hit state to true
        bBombHit = true;

        //Loop through the array and display which are bombs
        foreach (var tile in tiles)
        {
            if (tile.bHasBomb) tile.UpdateText("BOMB");
        }

        Invoke("ResetBoard", 1); //Reset the board after a second
    }
}
