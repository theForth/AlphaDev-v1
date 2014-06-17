using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Linq;

public class PopulateDB : MonoBehaviour {

	

	private string movesURL = "http://mjbcdn.com/nxt/login/getMoveInfo.php";
	private string basicURL = "http://mjbcdn.com/nxt/login/getBasicInfo.php";

	public static List<PokeData> pokeData;
	public static List<MoveData> moveData;
	
	void Start () {
		Debug.Log ("Here");
		StartCoroutine(populateDB ());
	}

	IEnumerator populateDB(){
		
		WWW moves = new WWW (movesURL);
		
		yield return moves;
		
		Debug.Log (moves.text);
		
		JSONObject moveJSON = new JSONObject (moves.text);
		
		moveData = new List<MoveData>{};
		for (int i = 0; i<moveJSON.Count; i++) {
			JSONObject obj = moveJSON [i];
			string accuracy = obj["accuracy"].str;
			if(accuracy == null){accuracy = "-1";} 
			moveData.Add (new MoveData{
				Id = float.Parse(obj["move_id"].str),
				Name = obj["name"].str,
				LongText = obj["text"].str,
				PP = float.Parse(obj["pp"].str),
				Accuracy = float.Parse(accuracy),
				DamageType = obj["identifier"].str,
				Power = float.Parse(obj["power"].str),
				PokemonId = float.Parse (obj["id"].str),
				ShortEffect = ""
			});
		}
		StaticGlobalVariables.moveData = moveData;
		
		WWW basicInfo = new WWW (basicURL);
		yield return basicInfo;
		
		Debug.Log (basicInfo.text);
		JSONObject pokeJson = new JSONObject (basicInfo.text);
		
		
		pokeData = new List<PokeData>{};
		for (int i = 0; i<pokeJson.Count; i++) {
			JSONObject obj = pokeJson [i];
			pokeData.Add (new PokeData{
				Id = float.Parse(obj["id"].str),
				Name = obj["Name"].str,
				HP = float.Parse(obj["HP"].str),
				Attack = float.Parse(obj["Attack"].str),
				Defense = float.Parse(obj["Defense"].str),
				spAttack = float.Parse(obj["spAttack"].str),
				spDefense = float.Parse(obj["spDefense"].str),
				Speed = float.Parse(obj["Speed"].str),
				Weight = float.Parse(obj["height"].str),
				Height = float.Parse(obj["height"].str),
				CaptureRate = float.Parse(obj["capture_rate"].str),
				BaseXP = float.Parse(obj["base_experience"].str),
			});
		}
		StaticGlobalVariables.pokeData = pokeData;
	}

	public static Pokemon getBasicPokemon(float id){
		Pokemon result = new Pokemon();
		
		foreach (var i in StaticGlobalVariables.pokeData.Where (e=>e.Id == id)) {
			result.attack = i.Attack;
			result.currentHealth = i.HP;
			result.currentXP = i.BaseXP;
			result.damage = i.Attack;
			result.defence = i.Defense;
			result.health = i.HP;
			result.hp = i.HP;
			result.name = i.Name;
			result.number = int.Parse(i.Id.ToString());
			result.speed = i.Speed;
			result.xp = i.BaseXP;
		}
		int baseMoveCounter = 0;
		foreach(var i in StaticGlobalVariables.moveData.Where (e=>e.PokemonId == id)){
			if(baseMoveCounter < 4){
				result.moves.Add(i);
			}
			else{
				break;
			}
			baseMoveCounter++;
		}
		return result;
	}

}
