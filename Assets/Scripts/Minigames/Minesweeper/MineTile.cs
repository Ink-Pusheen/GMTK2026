using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MineTile : MonoBehaviour, iInteraction
{
    [Header("Sprite Related")]

    private SpriteRenderer render; //Sprite renderer of this tile

    [SerializeField] Sprite unknownSprite; //Sprite for a tile that is unknown
    [SerializeField] Sprite flagSprite; //Sprite for flagging a tile
    [SerializeField] Sprite bombSprite; //Sprite if the player hit a bomb


    [Header("Tile Variables")]

    public bool bHasBomb; //Does this tile have a bomb?
    [SerializeField] bool bIsFlagged; //Is this tile currently flagged?

    [SerializeField] bool bIsRevealed; //is this tile revealed?

    [SerializeField] TMP_Text tileValue; //Value to display if uncovered to be something that isn't a bomb

    #region Interfaces

    public void LeftClickInteraction()
    {
        TileInteraction();
    }

    public void RightClickInteraction()
    {
        FlagTile();
    }

    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileValue.text = ""; //Clear the number view
        ResetTile(); //Reset the tile prematurely
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Reveal this tile
    public void TileInteraction()
    {
        //Check if the game needs to be reset first
        if (MinesweeperManager.Instance.bBombHit) return;

        //Check if the board wasn't started yet
        if (MinesweeperManager.Instance.tilesList.Count is 0)
        {
            MinesweeperManager.Instance.SetupTileList(this); //Setup the board, excluding this tile

            return;
        }

        if (bIsRevealed || bIsFlagged) return; //Return if this tile is already revealed or is currently flagged

        bIsRevealed = true; //Ensure this tile is marked as revealed

        //Debug.Log("Tile interaction");

        if (bIsFlagged) return; //Don't interact with it if the tile is already flagged

        if (bHasBomb)
        {
            Debug.Log("Bomb detected, display all other tiles");

            tileValue.text = "BOMB";

            MinesweeperManager.Instance.DisplayBombs();

            return;
        }

        Collider[] foundtiles = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity);

        int bombCount = 0;

        foreach (Collider col in foundtiles)
        {
            MineTile tile = col.GetComponent<MineTile>();

            if (tile is null || col.gameObject == this.gameObject) continue;

            if(tile.bHasBomb) bombCount++;
        }

        if (bombCount is 0)
        {
            tileValue.text = "0";

            foreach (Collider col in foundtiles)
            {
                MineTile tile = col.GetComponent<MineTile>();

                if (tile is null || col.gameObject == this.gameObject) continue;

                tile.TileInteraction();
            }
        }
        else
        {
            tileValue.text = bombCount.ToString(); //Display how many bombs there are
        }

        MinesweeperManager.Instance.RemoveTileFromList(this); //Remove this tile from the list
    }


    //Flag function
    void FlagTile()
    {
        if (bIsRevealed) return; //Don't interact with it if it's already shown

        bIsFlagged = !bIsFlagged; //Flip the flag state

        if (bIsFlagged)
        {
            //Swap to the flag sprite
            tileValue.text = "Flag";
        }
        else
        {
            //Swap to the undisovered sprite
            tileValue.text = "";
        }
    }

    //Reset the tile
    public void ResetTile()
    {
        bHasBomb = false; //Reset bomb state
        bIsRevealed = false;
        bIsFlagged = false;

        //Reset sprite and text

        tileValue.text = "";
    }


    //Function for the tile to have a display
    public void UpdateText(string inText)
    {
        tileValue.text = inText;
    }

    
}
