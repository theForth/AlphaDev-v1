using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Linq;

public class ServerConnection : MonoBehaviour {
	
	public UIInput username;
	public UIInput password;
	public UIButton login;
	
	private string loginURL = "http://mjbcdn.com/nxt/login/login.php";
	private string movesURL = "http://mjbcdn.com/nxt/login/getMoveInfo.php";
	private string basicURL = "http://mjbcdn.com/nxt/login/getBasicInfo.php";
	private string userURL = "http://mjbcdn.com/nxt/login/userInfo.php";

	public static List<PokeData> pokeData;
	public static List<MoveData> moveData;
	public static string userName;
	public static string userHash;
	// Use this for initialization
	void Start () {
		Debug.Log ("Here");
	}
	
	IEnumerator logIn(){
		WWWForm login = new WWWForm ();
		login.AddField ("username", username.value);
		login.AddField ("password", password.value);
		
		WWW submitLogin = new WWW (loginURL, login);
		
		yield return submitLogin;
		userName = username.value;
		Debug.Log (submitLogin.text);
	}
	
	// Update is called once per frame
	IEnumerator OnClick () {
		//Debug.Log ("Clicked");
		Debug.Log (username.value);
		
		WWWForm login = new WWWForm ();
		login.AddField ("username", username.value);
		login.AddField ("password", password.value);
		
		WWW submitLogin = new WWW (loginURL, login);
		
		yield return submitLogin;
		userName = username.value;
		StaticGlobalVariables.UserName = userName;
		StaticGlobalVariables.sessionhash = submitLogin.text;

		Debug.Log (submitLogin.text);

		var sesHas = submitLogin.text;

		WWW moves = new WWW (movesURL);
		
		yield return moves;
		
		Debug.Log (moves.text);
		
		JSONObject moveJSON = new JSONObject (moves.text);
		
		moveData = new List<MoveData>{};
		for (int i = 0; i<moveJSON.Count; i++) {
			JSONObject obj = moveJSON [i];
			moveData.Add (new MoveData{
				Id = float.Parse(obj["move_id"].str),
				Name = obj["name"].str,
				ShortEffect = obj["short_effect"].str,
				LongText = obj["text"].str,
				PP = float.Parse(obj["pp"].str),
				Accuracy = float.Parse(obj["accuracy"].str),
				DamageType = obj["identifier"].str,
				Power = float.Parse(obj["power"].str)
			});
		}
		StaticGlobalVariables.moveData = moveData;
		StaticGlobalVariables.pokeData = pokeData;
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
		sesHas = sesHas.Replace("\n", "");
		sesHas = sesHas.Replace("\r", "");
		sesHas = sesHas.Replace("\t", "");
		WWWForm sessionHash = new WWWForm ();
		sessionHash.AddField ("sessionKey", sesHas);
		WWW theUserInfo = new WWW (userURL, sessionHash);
		Debug.Log (sesHas);
		yield return theUserInfo;
		//Debug.Log (theUserInfo.text);
		JSONObject userInfoJSON = new JSONObject (theUserInfo.text);
		StaticGlobalVariables.pokeJSON = userInfoJSON[0]["pokemonJSON"].str;
		StaticGlobalVariables.trainerLevel = int.Parse(userInfoJSON[0]["lvl"].str);
		var pJSON = userInfoJSON [0] ["pokemonJSON"].str;
		pJSON = pJSON.Replace("\n", "");
		pJSON = pJSON.Replace("\r", "");
		pJSON = pJSON.Replace("\t", "");
		pJSON = pJSON.Replace(@"\","");
		Debug.Log (pJSON);
		JSONObject pokeInfo = new JSONObject (pJSON);

		List<simplePokemon> simpleList = new List<simplePokemon> ();
		simplePokemon testPokemon = new simplePokemon ();
		List<int> testMoves = new List<int>{};
		Debug.Log (pokeInfo ["id"].n);

		testPokemon.Id = int.Parse (pokeInfo["id"].n.ToString());
		testPokemon.Level = int.Parse (pokeInfo["level"].n.ToString());
		testMoves.Add(int.Parse(pokeInfo["Moves"][0].n.ToString()));
		testMoves.Add(int.Parse(pokeInfo["Moves"][1].n.ToString()));
		testMoves.Add(int.Parse(pokeInfo["Moves"][2].n.ToString()));
		testMoves.Add(int.Parse(pokeInfo["Moves"][3].n.ToString()));
		testPokemon.Moves = testMoves;
		simpleList.Add (testPokemon);
		StaticGlobalVariables.dbPokemon = simpleList; 
		Application.LoadLevel("NxtSceneTest");
		//getMoves ();
		//doSocket();
		//doSocket ();
		
	}
	
	public void printPokeData(){
		foreach (var i in pokeData) {
			Debug.Log (i.Id.ToString() + " : " + i.Name + " : " + i.HP.ToString());
		}
	}
	
//	public Pokemon getPokemonById(float Id){
//		PokeData pMon = pokeData.Where (e => e.Id == Id).Single ();
//		//List<MoveData> pMoves = moveData.Where (e => e.Id == Id).ToList ();
//		
//		//Pokemon result = new  Pokemon (pMon.Name, pMon.HP, pMon.Attack, pMon.Defense, pMon.Speed, pMon.BaseXP, 0);
//		
//		
//		
//		return result;
//	}
//	
//	public Pokemon getPokemonByIdLevel(float Id, int level){
//		PokeData pMon = pokeData.Where (e => e.Id == Id).Single ();
//		//List<MoveData> pMoves = moveData.Where (e => e.Id == Id).ToList ();
//		
//		float health = pMon.HP;
//		float attack = pMon.Attack;
//		float defence = pMon.Defense;
//		float speed = pMon.Speed;
//		float hp = pMon.HP;
//		//int level2 = level;
//		for(int i = 0; i < level;){
//			health = health + (health * .01f) + (pMon.HP * .02f);  
//			attack = attack + (attack * .01f) + (pMon.Attack * .02f);
//			defence = defence + (defence * .01f) + (pMon.Defense * .02f); 
//			speed = speed + (speed * .01f) + (pMon.Speed * .02f); 
//			hp = hp + (hp * .01f) + (pMon.HP * .02f); 
//		}
//		
//		//Pokemon result = new  Pokemon (name, health, attack, defence, speed, pMon.BaseXP, level);
//		
//		//new Pokemon{
//		//	name = pMon.Name,
//		//	health = health,
//		//	attack = attack,
//		//	defence = defence,
//		//	speed = speed,
//		//	xp = pMon.BaseXP,
//		//	isPlayer = false,
//		//	currentHealth = health,
//		//	level = level
//		//};
//		
//		
//		return result;
//	}
	
	IEnumerator getMoves(){
		Debug.Log ("AtMoves");
		WWW moves = new WWW (movesURL);
		
		yield return moves;
		
		Debug.Log (moves.text);
		
	}
	
	
	
	void doSocket(){
		TcpClient tcpclnt = new TcpClient();
		Debug.Log("Connecting.....");
		
		tcpclnt.Connect("pokemonnxt.com",23323);
		// use the ipaddress as in the server program
		
		Debug.Log("Connected");
		//Debug.Log("Enter the string to be transmitted : ");
		
		var str = "{\"header\":{\"PTYPE\":\"LOGIN\"},\"payload\":{\"Username\":\"User\",\"Password\":\"Password\"}}\r\n";
		var stm = tcpclnt.GetStream();
		
		var asen= new ASCIIEncoding();
		byte[] ba=asen.GetBytes(str);
		Debug.Log("Transmitting.....");
		
		stm.Write(ba,0,ba.Length);
		
		byte[] bb=new byte[100];

		int k=stm.Read(bb,0,100);

		string result = "";

		for (int i=0; i<k; i++) {
			result+=char.ConvertFromUtf32(bb[i]);
		}
		Debug.Log(result);
		
		var movesStr = "{\"header\":{\"PTYPE\":\"DATA_REQUEST\"},\"payload\":{\"Type\":\"ALL\"}";
		byte[] bam =asen.GetBytes(movesStr);
		
		stm.Write(bam,0,bam.Length);
		
		byte[] bbm=new byte[100];
		int km=stm.Read(bbm,0,100);
		
		
		for (int i=0; i<km; i++) {
			result += char.ConvertFromUtf32(bbm[i]);
		}
		Debug.Log(result);
		tcpclnt.Close();
		
	}
}
