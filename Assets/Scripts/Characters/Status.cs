using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Systems;
using UnityEngine;

namespace Characters
{
    public enum StatusCategory
    {
        MaxHp, Hp, Attack, Dexterity, Defense, Resist
    }

    public class Status{

        public Status(int hp = 0, int attack = 0, int dexterity = 0, int defense = 0, int resist = 0)
        {
            effects = new Dictionary<string, Func<int, StatusCategory, int>>();
            statusValues = new Dictionary<StatusCategory, int>()
            {
                {StatusCategory.MaxHp, hp},
                {StatusCategory.Hp, hp},
                {StatusCategory.Attack, attack},
                {StatusCategory.Dexterity, dexterity},
                {StatusCategory.Defense, defense},
                {StatusCategory.Resist, resist}
            };
        }

        public void AddEffect(string key, Func<int, StatusCategory, int> effect)
        {
            if(!effects.ContainsKey(key))
                effects.Add(key, effect);
        }

        public void RemoveEffect(string key)
        {
            if (effects.ContainsKey(key))
                effects.Remove(key);
        }

        public void ClearEffect()
        {
            effects.Clear();
        }

        public int Hp
        {
            get => statusValues[StatusCategory.Hp];
            set => statusValues[StatusCategory.Hp] = value;
        }

        public int MaxHp => getStatusValue(StatusCategory.MaxHp);
        public int Attack => getStatusValue(StatusCategory.Attack);
        public int Dexterity => getStatusValue(StatusCategory.Dexterity);
        public int Defense => getStatusValue(StatusCategory.Defense);
        public int Resist => getStatusValue(StatusCategory.Resist);

        public int MaxHpOrigin
        {
            get => statusValues[StatusCategory.MaxHp];
            set
            {
                statusValues[StatusCategory.MaxHp] = value;
                statusValues[StatusCategory.Hp] = Mathf.Max(value, statusValues[StatusCategory.Hp]);
            }
        }

        public int AttackOrigin
        {
            get => statusValues[StatusCategory.Attack];
            set => statusValues[StatusCategory.Attack] = value;
        }

        public int DexterityOritin
        {
            get => statusValues[StatusCategory.Dexterity];
            set => statusValues[StatusCategory.Dexterity] = value;
        }

        public int DefenseOrigin
        {
            get => statusValues[StatusCategory.Defense];
            set => statusValues[StatusCategory.Defense] = value;
        }

        public int ResistOrigin
        {
            get => statusValues[StatusCategory.Resist];
            set => statusValues[StatusCategory.Resist] = value;
        }

        private int getStatusValue(StatusCategory cat)
        {
            var result = statusValues[cat];
            foreach (var effect in effects.Values)
            {
                result = effect(result, cat);
            }

            return result;
        }

        private Dictionary<string, Func<int, StatusCategory, int>> effects;
        private Dictionary<StatusCategory, int> statusValues;
    }
}