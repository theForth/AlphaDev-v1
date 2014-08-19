

using UnityEngine;

//Responsible for handling all UI events occusing only when a pokemon engages in a battle with an ENEMY
// Verification of enemy pokemon and sending data to relevant handlers!!
/// <summary>
/// - CHECK if trainer has an active pokemon
/// -   Check if any enemy exsits
/// -       Check if the active bar is not initialized with any enemyes
/// -               INTIALIZE!!
/// -   Check if enemy is still the enemy
///         Dont intialize
///     if new enemy hit while another enemy is being hit
///          INTIALIZE new enemy
///          
/// -   if enemy doesnt exist
/// -   RESET ERRYTHING!
/// </summary>
public class EnemyStatusPanel : MonoBehaviour
{
    /// <summary>
    /// Cache in the enemy bar
    /// </summary>
    public EnemyProgressBar enemyProgressBar;
    /// <summary>
    /// Keep checking of enemy changes and send values to the progress bar handler
    /// </summary>
    private PokeCore enemy;
   
    private bool _enabled;
    public int windowID;
    void Start()
    {

    }

    void Update()
    {
        if (_enabled)
        {
            if (Trainer.ActivePokemon != null)
            {
                if (Trainer.ActivePokemon.GetEnemy(out enemy))                  //TODO Set an OUT variable
                {
                    if (enemyProgressBar.ActivePokemon == null)
                    {
                        UIWindow.GetWindow(this.windowID).Show(true);
                        enemyProgressBar.EnableInit(enemy);
                    }
                    else if (enemyProgressBar.ActivePokemon != enemy)
                    {
                        UIWindow.GetWindow(this.windowID).Show(true);
                        enemyProgressBar.EnableInit(enemy);
                    }


                }
                else
                {
                    enemyProgressBar.DisableReset();
                    UIWindow.GetWindow(this.windowID).Hide(true);
                    enemy = null;
                }
            }
        }
    }
    
    public void Enable()
    {
        _enabled = true;
    }
    
   public void Disable()
    {
        enemyProgressBar.DisableReset();
        UIWindow.GetWindow(this.windowID).Hide(true);
        enemy = null;
        _enabled = false;

    }
    
}

