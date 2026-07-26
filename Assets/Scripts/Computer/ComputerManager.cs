using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    [Header("Tabs")]

    [SerializeField] GameObject[] ComputerTabs; //Array of tabs the computer can cycle through


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    #region Tab Functionality

    //" Hide " all the tabs / Swap to the specified one
    public void TabSwap(int tabToOpen)
    {
        for (int tab = 0; tab < ComputerTabs.Length; tab++)
        {
            if(tab == tabToOpen) ComputerTabs[tab].gameObject.transform.localScale = Vector3.one; //Scale the object to one

            else ComputerTabs[tab].gameObject.transform.localScale = Vector3.zero; //Scale the object to zero
        }
    }

    //Function to enable and siable certain tabs
    public void EnableSpecifiedTab(int tabToOpen)
    {
        ComputerTabs[tabToOpen].gameObject.transform.localScale = Vector3.one;
    }

    public void DisableSpecifiedTab(int tabToClose)
    {
        ComputerTabs[tabToClose].gameObject.transform.localScale = Vector3.zero;
    }


    //Quit the application
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    #endregion
}
