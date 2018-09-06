using System.Collections.Generic;

using UnityEngine;

using Base;
using Battle;

namespace Debuggers
{
    public class PokemonDebugger : MonoBehaviour
    {
        [Header("Team Information")]
        public PokemonTeam curTeam = PokemonTeam.redTeam;

        public int index = 0;
        public bool matchCurIndex;

        [Header("Pokemon Info")]
        public string pokeName;

        public int m_ID;
        public int m_Level;
        public int m_CurHP;

        [Header("Pokemon Stats")]
        public int m_Attack;

        public int m_Defenese;
        public int m_SpecialAttack;
        public int m_SpecialDefense;
        public int m_Speed;

        [Header("Attack Info")]
        public int critRatio;

        public List<string> atkMoves = new List<string>();
        public dmgMult damageMultiplier;

        [Header("Cached Info")]
        public string nextAttack;

        public float cachedDamage;

        [Header("")]
        public nonVolitileStatusEffects status_A;

        public pokemonPosition position;
        public attackStatus atkStatus;

        private BattleSimulator battlesim;
        private List<PokemonBase> pokemonTeamData = new List<PokemonBase>();

        // Use this for initialization
        private void Start()
        {
            battlesim = BattleSimulator.Instance;
            updateStats();
        }

        // Update is called once per frame
        private void Update()
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
            cachedDamage = p.cachedDamage;

            damageMultiplier = p.damageMultiplier;
            nextAttack = p.nextAttack;

            critRatio = p.critRatio_stage;
        }
    }
}