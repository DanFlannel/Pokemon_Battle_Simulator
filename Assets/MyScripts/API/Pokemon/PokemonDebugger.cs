using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;
using FBG.Battle;

namespace FBG.Debuggers
{
    public class PokemonDebugger : MonoBehaviour
    {
        [Header("Team Information")]
        public PokemonTeam curTeam = PokemonTeam.redTeam;
        public int index = 0;
        public bool matchCurIndex;

        [Header("Pokemon Data")]

        public string pokeName;
        public int m_ID;
        public int m_Level;
        public int m_CurHP;

        public int m_Attack;
        public int m_Defenese;
        public int m_SpecialAttack;
        public int m_SpecialDefense;
        public int m_Speed;

        public List<string> atkMoves = new List<string>();
        public dmgMult damageMultiplier;

        public string nextAttack;
        public nonVolitileStatusEffects status_A;
        public pokemonPosition position;
        public attackStatus atkStatus;

        public int critRatio;

        private BattleSimulator battlesim;
        private List<PokemonBase> pokemonTeamData = new List<PokemonBase>();

        // Use this for initialization
        void Start()
        {
            battlesim = BattleSimulator.Instance;
            updateStats();
        }

        // Update is called once per frame
        void Update()
        {
            updateStats();
        }

        private void updateStats()
        {
            if (curTeam == PokemonTeam.redTeam)
            {
                pokemonTeamData = battlesim.redTeam.pokemon;
                if (matchCurIndex)
                {
                    index = battlesim.redIndex;
                }
            }
            else
            {
                pokemonTeamData = battlesim.blueTeam.pokemon;
                if (matchCurIndex)
                {
                    index = battlesim.blueIndex;
                }
            }

            if (index < 0)
            {
                index = pokemonTeamData.Count - 1;
            }

            if (index >= pokemonTeamData.Count)
            {
                index = 0;
            }

            if (pokemonTeamData.Count == 0) return;

            PokemonBase p = pokemonTeamData[index];
            pokeName = p.Name;
            m_ID = p.ID;
            m_Level = p.Level;
            m_CurHP = p.curHp;

            m_Attack = p.Attack;
            m_Defenese = p.Defense;
            m_SpecialAttack = p.Special_Attack;
            m_SpecialDefense = p.Special_Defense;
            m_Speed = p.Speed;

            atkMoves = p.atkMoves;

            status_A = p.status_A;
            position = p.position;
            atkStatus = p.atkStatus;

            damageMultiplier = p.damageMultiplier;
            nextAttack = p.nextAttack;

            critRatio = p.critRatio_stage;
        }
    }
}
