using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Items;
using Assets.Scripts.States;

namespace Assets.Scripts.Characters
{
    public enum Force
    {

    }

    public interface IDungeonCharacter
    {
        string Name { get; }
        Force Force { get; }
        IEnumerator Turn();

        bool Attacked(int power, bool isShot, BattleCharacter character = null, IItem item = null);

        bool Healed(int power, BattleCharacter character = null, IItem item = null);

        bool AddState(State state);

        bool HealStates(params StateID[] states);
    }
}
