﻿using UnityEngine;
using System.Collections.Generic;

namespace FatBobbyGaming {
    public class FBG_DebugPokemon : MonoBehaviour {

        [Header("Team Information")]
        public PokemonTeam curTeam = PokemonTeam.redTeam;
        public int index = 0;

        [Header("Pokemon Data")]

        public string pokeName;
        public int m_ID;
        public int m_Level;

        public int m_Attack;
        public int m_Defenese;
        public int m_SpecialAttack;
        public int m_SpecialDefense;

        public List<string> attacks = new List<string>();

        public nonVolitileStatusEffects status_A;
        public pokemonPosition position;
        public attackStatus atkStatus;

        private PokemonTeam storedTeam;
        private int storedIndex;
        private FBG_BattleSimulator battlesim;
        private List<FBG_Pokemon> pokemonTeamData = new List<FBG_Pokemon>();

        // Use this for initialization
        void Start() {
            battlesim = this.GetComponent<FBG_BattleSimulator>();
            storedTeam = curTeam;
            storedIndex = index;
            updateStats();
        }

        // Update is called once per frame
        void Update() {
            if(checkTeam() || checkIndex())
            {
                updateStats();
            }
        }

        private bool checkIndex()
        {
            if (storedIndex == index)
            {
                return false;
            }
            storedIndex = index;
            return true;

        }

        private bool checkTeam()
        {
            if (storedTeam == curTeam)
            {
                return false;
            }
            storedTeam = curTeam;
            return true;
        }

        private void updateStats()
        {
            if(curTeam == PokemonTeam.redTeam)
            {
                pokemonTeamData = battlesim.redTeam;
            }
            else
            {
                pokemonTeamData = battlesim.blueTeam;
            }

            if (index < 0)
            {
                index = pokemonTeamData.Count - 1;
            }

            if (index >= pokemonTeamData.Count)
            {
                index = 0;
            }

            FBG_Pokemon p = pokemonTeamData[index];
            pokeName = p.Name;
            m_ID = p.ID;
            m_Level = p.Level;

            m_Attack = p.Attack;
            m_Defenese = p.Defense;
            m_SpecialAttack = p.Special_Attack;
            m_SpecialDefense = p.Special_Defense;

            attacks.Clear();
            attacks = p.attackMoves;

            status_A = p.status_A;
            position = p.position;
            atkStatus = p.atkStatus;
        }
    }
}
