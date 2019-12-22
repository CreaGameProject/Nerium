using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.Characters;

public enum CommandEnum
{
    Attack, NormalAttack, Shot, To, From, MoveAttack,  Item, Use, Interact, Myself, Throw, Put, Move, Field, Wait
}

public class Player : Character, IBattleCharacter
{
    public int GetHp => health;

    public int GetAttack
    { // 仮
        get { return level; }
    }

    public int GetDefense
    { // 仮
        get { return level; }
    }

    public int Damage(int damage, params AttackAttribution[] attributions)
    { // 仮
        if (damage > 0)
        {
            var tempHealth = health;
            health -= health > damage ? damage : health;
            return tempHealth - health;
        }
        return 0;
    }

    public TurnAction GetTurnAction { get; } = new TurnAction();

    private int level;
    private int health;

    private bool CommandCheck(IEnumerable<object> command)
    {
        int[,] table = {
            { 1, -1, -1, -1, -1, -1, 2, -1, -1, -1, -1, -1, 3, 4, 5 }
        };
        return false;
    }

    private void SetTurnAction(IEnumerable<object> command)
    { // コマンドを解析してそのターンの行動を生成
        var commandArray = command.ToArray();
        if (!(commandArray[0] is CommandEnum))
        {
            Debug.LogError("構文エラー index:0");
            return;
        }

        switch ((CommandEnum)commandArray[0])
        {
            case CommandEnum.Attack:
                switch ((CommandEnum)commandArray[1])
                {
                    case CommandEnum.NormalAttack: // 通常攻撃
                        GetTurnAction.Behave = NormalAttack();
                        break;

                    case CommandEnum.Shot: // 射撃
                        GetTurnAction.Behave = Shot();
                        break;

                    case CommandEnum.MoveAttack: // 移動攻撃
                        if (!(commandArray[3] is Vector2Int && commandArray[5] is Enemy))
                        {
                            Debug.Log("構文エラー index: 3 or 5");
                            return;
                        }
                        var direction = (Vector2Int) commandArray[3];
                        var from = (Enemy) commandArray[5];
                        GetTurnAction.Behave = MoveAttack(direction, from);
                        break;

                    default:
                        Debug.LogError("構文エラー：存在しない攻撃選択肢");
                        return;
                }
                break;

            case CommandEnum.Item:
                switch (commandArray)
                {
                    
                }
                break;

            case CommandEnum.Move:
                break;

            case CommandEnum.Field:
                break;

            case CommandEnum.Wait:
                break;

            default:
                Debug.LogError("構文エラー：不正なコマンド");
                return;
        }

    }

    private IEnumerator NormalAttack()
    {
        Debug.Log("Player NormalAttack");
        yield return new WaitForSeconds(1);
    }
    private IEnumerator Shot()
    {
        Debug.Log("Player Shot");
        yield return new WaitForSeconds(1);
    }

    private IEnumerator MoveAttack(Vector2Int directionFromEnemy, Enemy enemy)
    {
        Debug.Log("Player MoveAttack to " + directionFromEnemy + " from " + enemy);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator UseItem(Item item, Item interactedItem = null)
    {
        Debug.Log("Use " + item);
        if (interactedItem != null)
        {
            Debug.Log("interact " + interactedItem);
        }
        yield return new WaitForSeconds(1);
    }

    private IEnumerator ThrowItem(Item item)
    {
        Debug.Log("throw " + item);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Put(Item item)
    {
        Debug.Log("put " + item);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Field(Field field)
    {
        Debug.Log("Use " + field);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator Wait()
    {
        Debug.Log("wait");
        yield return new WaitForSeconds(1);
    }
}
