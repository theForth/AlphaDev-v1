using UnityEngine;
using System.Collections;

public class PokemonObj : MonoBehaviour
{
		public float speed = 5;
		public Pokemon pokemon = null;
		public PokemonObj enemy = null;
		public bool	isWild = false;
		public int level = 5;
		Vector3 velocity = Vector3.zero;
		bool returning = false;
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
								PokemonObj enemyObj = hit.collider.GetComponent<PokemonObj> ();
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
}
