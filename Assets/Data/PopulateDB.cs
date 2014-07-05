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
		//StartCoroutine(populateDB ());

		//Mock data populate
		mockData ();
	}

	void mockData(){
		moveData = new List<MoveData>{};
		pokeData = new List<PokeData>{};
		moveData.Add (new MoveData{
			Id = 33,
			Name = "Tackle",
			LongText = "Tackle mock textS",
			PP = 35f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 50f,
			PokemonId = 1,
			ShortEffect = "Tackle mock textL"
		});

		moveData.Add (new MoveData{
			Id = 45,
			Name = "Growl",
			LongText = "Growl mock textS",
			PP = 40f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 0f,
			PokemonId = 1,
			ShortEffect = "Growl mock textL"
		});

		moveData.Add (new MoveData{
			Id = 73,
			Name = "Leech Seed",
			LongText = "Leech Seed mock textS",
			PP = 10f,
			Accuracy = 90f,
			DamageType = "Physical",
			Power = 0f,
			PokemonId = 1,
			ShortEffect = "Leech Seed mock textL"
		});

		moveData.Add (new MoveData{
			Id = 22,
			Name = "Vine Whip",
			LongText = "Vine Whip mock textS",
			PP = 25f,
			Accuracy = 100f,
			DamageType = "Grass",
			Power = 45f,
			PokemonId = 1,
			ShortEffect = "Vine Whip mock textL",
            CoolDown = 1

		});

		moveData.Add (new MoveData{
			Id = 98,
			Name = "Quick Attack",
			LongText = "Quick Attack mock textS",
			PP = 30f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 40f,
			PokemonId = 25,
			ShortEffect = "Quick Attack mock textL",
              CoolDown = 1
		});

		moveData.Add (new MoveData{
			Id = 85,
			Name = "Thunder",
			LongText = "Thunderbolt mock textS",
			PP = 15f,
			Accuracy = 100f,
			DamageType = "Electric",
			Power = 90f,
			PokemonId = 25,
			ShortEffect = "Thunderbolt mock textL",
            CoolDown = 1
		});

		moveData.Add (new MoveData{
			Id = 104,
			Name = "Double Team",
			LongText = "Double Team mock textS",
			PP = 15f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 0f,
			PokemonId = 25,
			ShortEffect = "Double Team mock textL"
		});

		moveData.Add (new MoveData{
			Id = 45,
			Name = "Growl",
			LongText = "Growl mock textS",
			PP = 40f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 0f,
			PokemonId = 25,
			ShortEffect = "Growl mock textL"
		});

		moveData.Add (new MoveData{
			Id = 55,
			Name = "Water Gun",
			LongText = "Water Gun mock textS",
			PP = 25f,
			Accuracy = 100f,
			DamageType = "Water",
			Power = 40f,
			PokemonId = 7,
			ShortEffect = "Water Gun mock textL"
		});

		moveData.Add (new MoveData{
			Id = 45,
			Name = "Growl",
			LongText = "Growl mock textS",
			PP = 40f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 0f,
			PokemonId = 7,
			ShortEffect = "Growl mock textL"
		});

		moveData.Add (new MoveData{
			Id = 33,
			Name = "Tackle",
			LongText = "Tackle mock textS",
			PP = 35f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 50f,
			PokemonId = 7,
			ShortEffect = "Tackle mock textL"
		});

		moveData.Add (new MoveData{
			Id = 110,
			Name = "Withdraw",
			LongText = "Withdraw mock textS",
			PP = 40f,
			Accuracy = 100f,
			DamageType = "Defense",
			Power = 0f,
			PokemonId = 7,
			ShortEffect = "Withdraw mock textL"
		});

		moveData.Add (new MoveData{
			Id = 10,
			Name = "Scratch",
			LongText = "Scratch mock textS",
			PP = 35f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 40f,
			PokemonId = 4,
			ShortEffect = "Scratch mock textL"
		});

		moveData.Add (new MoveData{
			Id = 45,
			Name = "Growl",
			LongText = "Growl mock textS",
			PP = 40f,
			Accuracy = 100f,
			DamageType = "Physical",
			Power = 0f,
			PokemonId = 4,
			ShortEffect = "Growl mock textL"
		});

		moveData.Add (new MoveData{
			Id = 52,
			Name = "Ember",
			LongText = "Ember mock textS",
			PP = 25f,
			Accuracy = 100f,
			DamageType = "Fire",
			Power = 40f,
			PokemonId = 4,
			ShortEffect = "Ember mock textL"
		});

		moveData.Add (new MoveData{
			Id = 108,
			Name = "Smokescreen",
			LongText = "Smokescreen mock textS",
			PP = 20f,
			Accuracy = 100f,
			DamageType = "Defense",
			Power = 0f,
			PokemonId = 4,
			ShortEffect = "Smokescreen mock textL"
		});

		pokeData.Add (new PokeData{
			Id = 1f,
			Name = "Bulbasaur",
			HP = 45f,
			Attack = 49f,
			Defense = 49f,
			spAttack = 65f,
			spDefense = 65f,
			Speed = 45f,
			Weight = 69f,
			Height = 7f,
			CaptureRate = 45f,
			BaseXP = 64f
		});

		pokeData.Add (new PokeData{
			Id = 4f,
			Name = "Charmander",
			HP = 39f,
			Attack = 52f,
			Defense = 43f,
			spAttack = 60f,
			spDefense = 50f,
			Speed = 65f,
			Weight = 85f,
			Height = 6f,
			CaptureRate = 45f,
			BaseXP = 62f
		});

		pokeData.Add (new PokeData{
			Id = 7f,
			Name = "Squirtle",
			HP = 44f,
			Attack = 48f,
			Defense = 65f,
			spAttack = 50f,
			spDefense = 64f,
			Speed = 43f,
			Weight = 90f,
			Height = 5f,
			CaptureRate = 45f,
			BaseXP = 63f
		});

		pokeData.Add (new PokeData{
			Id = 25f,
			Name = "Pikachu",
			HP = 35f,
			Attack = 55f,
			Defense = 40f,
			spAttack = 50f,
			spDefense = 50f,
			Speed = 90f,
			Weight = 60f,
			Height = 4f,
			CaptureRate = 190f,
			BaseXP = 105f
		});
		StaticGlobalVariables.pokeData = pokeData;
		StaticGlobalVariables.moveData = moveData;
	}
	
	IEnumerator populateDB(){
		
		WWW moves = new WWW (movesURL);
		
		yield return moves;

		WWW basicInfo = new WWW (basicURL);
		yield return basicInfo;

		Debug.Log (moves.text);
		
		JSONObject moveJSON = new JSONObject (moves.text);
		
		moveData = new List<MoveData>{};
		for (int i = 0; i<moveJSON.Count; i++) {
			JSONObject obj = moveJSON [i];
			string accuracy = obj["accuracy"].str;
			if(accuracy == null){accuracy = "-1";} 
			moveData.Add (new MoveData{
				Id = int.Parse(obj["move_id"].str),
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
				BaseXP = float.Parse(obj["base_experience"].str)
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
