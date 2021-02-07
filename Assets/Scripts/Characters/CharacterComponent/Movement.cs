using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Systems;
using Characters;
using UnityEngine;

public class Movement
{
    private Func<TerrainType, Characters.MovingAbility> movingAbility;

    public Movement(System.Func<TerrainType, Characters.MovingAbility> movingAbility)
    {
        throw new System.NotImplementedException();
    }

    public virtual MovingAbility GetMovingAbility(TerrainType terrain)
    {
        switch (terrain)
        {
            case TerrainType.Floor:
                return MovingAbility.Walkable;
            case TerrainType.WaterWay:
                return MovingAbility.Corner;
            default:
                return MovingAbility.UnWalkable;
        }
    }


    /// <summary>
    /// 一マス隣の座標に移動可能であるかを判定する
    /// </summary>
    /// <param name="to"></param>
    /// <param name="from"></param>
    /// <returns></returns>
    public bool CanMoveTo(Vector2Int to, Vector2Int from)
    {
        // directionが2マス以上の移動の場合false
        if (Mathf.Abs(to.x) > 1 || Mathf.Abs(to.y) > 1)
        {
            return false;
        }
        // 判定マスがフロア範囲外の場合false
        if (!GameManager.CurrentFloor.InRange(Position + to))
        {
            return false;
        }
        var nextCell = GameManager.CurrentFloor[Position + to];
        // 判定マスが壁であるor判定マスにキャラが存在する場合false

        if (GetMovingAbility(nextCell.terrain) != MovingAbility.Walkable || nextCell.character != null)
        {
            return false;
        }
        // 斜め移動かつコーナーが移動不可マスの場合false
        if (to.magnitude > 1)
        {
            var corner = to;
            corner.x = 0;
            if (GetMovingAbility(GameManager.CurrentFloor[Position + corner].terrain) == MovingAbility.UnWalkable)
            {
                return false;
            }

            corner = to;
            corner.y = 0;
            if (GetMovingAbility(GameManager.CurrentFloor[Position + corner].terrain) == MovingAbility.UnWalkable)
            {
                return false;
            }
        }

        return true;
    }
}