//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
namespace Utilities.PokeUtils
{
    public static class PokeUtils
    {

        public static void InflictDamage()
        {
        }

        //Case Return 1 : Not Enough PP
        public static int CanCastMove(PokeCore pokeCore, int selectedIndex)
        {
            if (pokeCore.pokemon.currentPP < pokeCore.pokemon.moves[selectedIndex].PP)
            {
                return 0;
            }
            if (pokeCore.pokemon.moves[selectedIndex].IsOnCooldown)
            {
                return 3;
            }
            return 2;
        }

    }


}