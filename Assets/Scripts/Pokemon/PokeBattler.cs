using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class PokeBattler : PokeCore
{   //*****AnimatorVariables*************** 
    private Animator anim;
    //*****Pokeball Variables***************
   
    //*****Pokemon Variables *************
    RaycastHit hit;
    Vector3 velocity = Vector3.zero;
    bool returning = false;
    PokemonController pokemonController;
    //************Move effect variables*************
   
    private Dictionary<int, Action<MoveData,int>> currentMoveMethods = new Dictionary<int, Action<MoveData,int>>();   //These will updated from a spell manager
    //Dictionary<int, Action> MoveList = new Dictionary<int, Action>(){};
    private bool isAimingMove = false;

    void Awake()
    {
        PlayerControlManager.CastMove += CastMove;
    
    }

    /*void Init("PokemonStatus)
{

}
  */
    void Start()
    {
      
        pokemonController = GetComponent<PokemonController>();
       
        moves = pokemon.moves;
        base.Start();
        currentMoveMethods[85] = CastThunder;
        currentMoveMethods[98] = CastQuickAttack;
        currentMoveMethods[45] = Growl;
        for (int i = 0; i < moves.Count; i++)
        {
            Debug.Log(moves[i].Name);
        //currentMoveMethods.Add(moves[i].Id, MoveMethodManager.MoveMethods[moves[i].Id]);
        }

        anim = GetComponent<Animator>();//Speed the process a bit by caching reference to moves
    }

    public override void Update()
    {
        if (pokemon.currentPP <= pokemon.PP)
        {
            pokemon.currentPP += Time.deltaTime*0.1f;
        }
      
    }


    public void CastMove(int selectedMoveSlot)
    {
      
        if (currentMoveMethods.ContainsKey(moves[selectedMoveSlot].Id))   //OptionalCheck
        {


            currentMoveMethods[moves[selectedMoveSlot].Id](moves[selectedMoveSlot], selectedMoveSlot);
            

        }

    }


    private void CastQuickAttack(MoveData moveData, int selectedMoveSlot)
    {
        StartCoroutine(QuickAttack(moveData,selectedMoveSlot));
        StartCooldownProcesses(selectedMoveSlot);
        Debug.Log("Casting " + moveData.Name + ' ' + transform.name);
    }
    IEnumerator QuickAttack(MoveData moveData,int selectedMoveSlot)
    {
        var layerMask = 1<<9;
        float runSpeedModifier = 1.5f;
        Vector3 moveDirection = new Vector3();
        yield return new WaitForSeconds(0.05f);
        pokemonController.movementControlState = MovementControlState.AIControl;
        pokemon.currentPP -= moveData.PP;
        CharacterController charContr = GetComponent<CharacterController>();
        Vector3 p1 = transform.position + charContr.center + Vector3.up * -charContr.height * 0.5F;
        Vector3 p2 = p1 + Vector3.up * charContr.height;
        while(!Input.GetMouseButtonDown(0) )
        {
            if (Physics.CapsuleCast(p1, p2, charContr.radius+2, transform.forward, out hit, 2 ,layerMask))
            {
               
                Debug.Log("Quick Attack Hit " + hit.collider.gameObject.name);
                break;
            }
            //pokemonController.Controllable = false;
            moveDirection = Camera.main.transform.forward;
            moveDirection.y = 0;
            Vector3.Normalize(moveDirection);
            pokemonController.Velocity = new Vector3(moveDirection.x, -pokemonController.gravity * Time.deltaTime, moveDirection.z);
            pokemonController.Velocity = pokemonController.Velocity * pokemonController.runSpeed * runSpeedModifier;
            //runSpeedModifier += 0.002f;
            yield return 0;
        }
        pokemonController.movementControlState = MovementControlState.PlayerControl;
        pokemonController.Controllable = true;


    }
    private void CastThunder(MoveData moveData, int selectedMoveSlot)
    {
        if (!isAimingMove)
        {
            StartCoroutine(Thunder(moveData, selectedMoveSlot));
        }
       
        
    }
    IEnumerator Thunder(MoveData moveData,int selectedMoveSlot)
    {
        GameObject AOE = (GameObject)Instantiate(moveData.MoveUI, transform.position, Quaternion.Euler(90, 0, 0));
        yield return new WaitForSeconds(0.05f);
        isAimingMove = true;
        while (isAimingMove == true)
        {
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                AOE.transform.position = hit.point + new Vector3(0, 5, 0);
              //  Debug.DrawLine(target.position, hit.point, Color.red);
                // Create a prefab if hit something
                
                //tempCursor.transform.position = hit.point;
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                AOE.transform.position = hit.point + new Vector3(0, 5, 0);
                if(Input.GetMouseButtonDown(1))
                {
                    Collider[] hitColliders = Physics.OverlapSphere(hit.point, 5,1<<9);
                    for (int i = 0; i < hitColliders.Count(); i++ )
                    {
                         if(hitColliders[i].gameObject.GetComponent<PokeCore>())
                        {
                            enemy = hitColliders[i].gameObject.GetComponent<PokeCore>();
                            enemy.pokemon.currentHealth -= 10f;
                            Debug.Log(enemy.pokemon.name + " was attacked!");
                            break;
                        }
                    }
                       // Debug.Log(hitColliders[i].gameObject.name);
                 
                    anim.SetTrigger(HashIDs.thunderTrigger);
                    AOE.AddComponent<TimedDestroy>().Init(1f);
                    //AkSoundEngine.PostEvent("Play_PokemonSFX_Thunder", gameObject);

                    StartCooldownProcesses(selectedMoveSlot);
                    yield return new WaitForSeconds(0.2f);
                    GameObject thunderBolt = (GameObject)Instantiate(moveData.VFXPrefab, hit.point, Quaternion.identity);
                    thunderBolt.transform.position += new Vector3(0, 1.5f, 0);
                    thunderBolt.AddComponent<TimedDestroy>().Init(0.5f);
                    //UsePP(moveData.PP);

                    isAimingMove = false;
                }

            } 
            yield return 0;
        }
    }
    public void Growl(MoveData moveData,int selectedMoveSlot)
    {
        Debug.Log("Casting " + moves[1].Name + ' ' + transform.name);
    }
    void OnDestroy()
    {
        PlayerControlManager.CastMove -= CastMove;
        
    }
}