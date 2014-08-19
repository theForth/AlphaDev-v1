using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class PokeCore : MonoBehaviour
{         //*****AnimatorVariables*************** 
    private Animator anim;
    //*****Pokeball Variables***************
    public bool canReturn = true;
    //*****Pokemon Variables *************
    public float speed = 5;
    public Pokemon pokemon = null;
    public PokeCore enemy = null;
    public PokeCoreType pokeCoreType;
    public List<MoveData> moves = new List<MoveData>();
    public bool isWild = false;
    public int level = 5;
    Vector3 velocity = Vector3.zero;
    bool returning = false;
    public int layerMask = 1 << 5 | 1 << 6;

    //Dictionary<int, Action> MoveList = new Dictionary<int, Action>(){};

    void Awake()
    {

    }


    public void Start()
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].IsOnCooldown)
                StartCooldownProcesses(i);
        }
        //Speed the process a bit by caching reference to moves
    }

    public virtual void Update()
    {
        if (pokemon.currentPP <= pokemon.PP)
        {
            pokemon.currentPP += Time.deltaTime;
        }
    }
    public bool GetEnemy(out PokeCore enemy)
    {
        if (this.enemy != null)
        {
            enemy = this.enemy;
            return true;
        }
        else
        {
            enemy = null;
            return false;
            
        }
    }
    public void StartCooldownProcesses(int selectedMoveSlot)
    {
        MoveSlot.GetSlot(selectedMoveSlot).CastMoveUI();
        StartCooldown(selectedMoveSlot);
    }
    public void StartCooldown(int selectedMoveSlot)
    {

        StartCoroutine("_StartCooldown", moves[selectedMoveSlot]);
    }

    public void UsePP(float PP)
    {

        pokemon.PP -= PP;
    }

    /// <summary>
    /// Starts a cool down with the referenced MoveData Info
    /// </summary>
    /// <param name="moveData"></param>
    /// <returns></returns>
    IEnumerator _StartCooldown(MoveData moveData)
    {
        Debug.Log("Casting Cooldown for -> " + moveData.Name);

        float cooldownDuration = moveData.IsOnCooldown ? moveData.CooldownTimer : moveData.Cooldown;
        moveData.IsOnCooldown = true;
        // Go back in time if we're resuming a cooldown
        //float startTime = Time.time - (cooldownDuration - duration);

        while (moveData.CooldownTimer >= 0)
        {
            moveData.CooldownTimer -= Time.deltaTime;


            yield return 0;
        }
        RefreshCooldown(moveData);

        moveData.IsOnCooldown = false;                    //Flag the move as Finished Cooldown
    }

    public void RefreshCooldown(MoveData moveData)
    {
        moveData.CooldownTimer = moveData.Cooldown;
    }


    /*
    void Update ()
    { 
    if(PokemonHasLevelled up)
    {
    pokemon.UpdateStats(level)
    CheckifEvolve
    CheckifnewMove
     if new move(
      gameManager.NewMoveControl = true;
      startCoroutine(gui);
    }
            velocity -= rigidbody.velocity;
            velocity.y = 0;
            if (velocity.sqrMagnitude > speed * speed)
                    velocity = velocity.normalized * speed;
            rigidbody.AddForce (velocity, ForceMode.VelocityChange);
            velocity = Vector3.zero;
		
            if (pokemon != null) {
                    foreach (Move move in pokemon.moves) {
                            move.cooldown += Time.deltaTime;
                            move.cooldown = Mathf.Clamp01 (move.cooldown);
                    }
			
                    if (pokemon.hp <= 0) {
                            Return ();
                    }
            }
    }
	
    public void SetVelocity (Vector3 vel)
    {
            velocity = vel;
    }
	
    public void Return ()
    {
            if (returning)
                    return;
            if (Player.pokemon == pokemon) {
                    Player.pokemonActive = false;
            }
            returning = true;
            GameObject effect = (GameObject)Instantiate (Resources.Load ("ReturnEffect"));
            effect.transform.position = transform.position;
            effect.transform.parent = transform;
            Destroy (gameObject, 1);
            pokemon.thrown = false;
    }
	
    public bool UseMove (Vector3 direction, Move move)
    {
            if (move.GetPPCost () > pokemon.pp)
                    return false;
            string attackChat = "";
            if (pokemon.isPlayer) {
                    attackChat = "Your ";
            } else {
                    attackChat = "Enemy ";
            }
            attackChat += pokemon.name + " used " + move.moveType + "!";
		
            switch (move.moveType) {
			
            case MoveNames.Growl:
                    {
                            if (move.cooldown < 1)
                                    return false;
                            const float range = 10;
                            Attack ("Effects/Debuff", false, range, direction, move);
                            audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/Growl"));
                            return true;}
			
            case MoveNames.TailWhip:
                    {
                            if (move.cooldown < 1)
                                    return false;
                            const float range = 10;
                            Attack ("Effects/Debuff", false, range, direction, move);
                            return true;}
			
            case MoveNames.Tackle:
                    {
                            if (move.cooldown < 1)
                                    return false;
                            const float range = 2;
                            Attack ("Effects/Bash", true, range, direction, move);
                            rigidbody.AddForce (direction * range * rigidbody.mass * 500);
                            return true;}
			
            case MoveNames.Scratch:
                    {
                            if (move.cooldown < 1)
                                    return false;
                            const float range = 2;
                            Attack ("Effects/Scratch", true, range, direction, move);
                            return true;}
            }
            return false;
    }
	
    private void Attack (string effectResource, bool costHP, float range, Vector3 direction, Move move)
    {	
            move.cooldown = 0;
            pokemon.pp -= move.GetPPCost ();
            RaycastHit[] hits = Physics.SphereCastAll (transform.position + Vector3.up, 1, direction, range, 1 << 10);
		
            foreach (RaycastHit hit in hits) {
                    if (hit.collider.gameObject != gameObject) {
                            GameObject newEffect = (GameObject)Instantiate (Resources.Load (effectResource));
                            PokeBattler enemyObj = hit.collider.GetComponent<PokeBattler> ();
                            if (isWild && enemyObj.isWild) //make sure wild pokemon don't attack each other.
                                    return;
                            if (costHP) {
					
                                    newEffect.transform.position = hit.point;
                            } else {
                                    if ((enemyObj.transform.position - transform.position).sqrMagnitude < range * range) {
						
                                            newEffect.transform.position = enemyObj.transform.position + Vector3.up * 0.2f;
                                            newEffect.transform.parent = enemyObj.transform;
                                    }
                            }
                            if (enemyObj) {
                                    if (enemyObj.pokemon != null)	
                                    if (costHP)
                                            enemyObj.pokemon.Damage (pokemon, move);
                                    else
                                            enemyObj.pokemon.DeBuff (pokemon, move);
                                    enemy = enemyObj;
                                    enemyObj.enemy = this;
                            }
				
                    }
            }
*/
    void OnDestroy()
    {
    }
}
