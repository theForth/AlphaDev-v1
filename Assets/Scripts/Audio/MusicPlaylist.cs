using UnityEngine;
using System.Collections;

public class MusicPlaylistManager : MonoBehaviour {


    void Start() {
        PlayerControlManager.eventPokemonRelease += OnPokemonRelease;
        PlayerControlManager.eventPokemonReturn += OnPokemonReturn;
    }

    private void OnPokemonRelease(int selectedIndex, PokeCore pokeCore) {
        PlayerControlManager.eventPokemonRelease -= OnPokemonRelease;
        ChangePlaylist("Battle Music", true);

    }

    private void OnPokemonReturn() {
        PlayerControlManager.eventPokemonRelease += OnPokemonRelease;
        ChangePlaylist("Background Music", true);
    }




    public void ChangePlaylist(string playlistName, bool startPlaylist) {
        MasterAudio.ChangePlaylistByName(playlistName, startPlaylist);
    }
}