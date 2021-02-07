using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dungeon;
using Characters;
using Systems;
using Dungeon;
using UnityEngine;

namespace Systems
{
    /// <summary>
    /// ターンを管理するクラス。GameManagerに保持され、floorの情報を読み取ってfloorのオブジェクトに指令を出す
    /// </summary>
    public class TurnManager
    {
        public int Turn = 0;
        private Floor floor;
        private Coroutine turnLoop;
        public bool Enable;

        public TurnManager(Floor floor)
        {
            this.floor = floor;
            Enable = true;
        }

        // ターンのメインループを開始
        public void SetTurnLoop()
        {
            turnLoop = GameManager.Instance.StartCoroutine(TurnLoop());
        }

        // ターンループの終了
        public void BreakTurnLoop()
        {
            GameManager.Instance.StopCoroutine(turnLoop);
        }
        
        // ターンのメインループ
        private IEnumerator TurnLoop()
        {
            for (Turn = 0; Turn < floor.MaxTurn; Turn++)
            {
                yield return PlayTurn();
            }
        }

        // 1ターン分のコルーチン
        private IEnumerator PlayTurn()
        {
            var player = floor.Player;
            // このターン行動を行う敵キャラ
            var acts = new List<IDungeonCharacter>();
            // このターン移動を行う敵キャラ
            var moves = new List<IDungeonCharacter>();
            var cat = ActCategory.Move; // 仮代入
            
            // プレイヤーのアクションカテゴリを呼び出し
            yield return new WaitUntil(()=>player.Command(ref cat));
            
            // プレイヤーが行動か移動かによる分岐
            if (cat == ActCategory.Move) // プレイヤーが"移動"を選択した場合
            {
                // 敵の行動カテゴリ(移動, 行動)を決定
                foreach (var enemy in floor.Enemies)
                    (enemy.RequestActCategory() == ActCategory.Action ? acts : moves).Add(enemy);
                
                // プレイヤー・敵の移動
                player.Move();
                foreach (var character in moves) 
                    character.Move();
                yield return new WaitForSeconds(DynamicParameter.StepTime);
                yield return new WaitUntil(()=>Enable);

                // 敵の行動
                foreach (var character in acts)
                    yield return character.PlayAction();
            }
            else // プレイヤーが"行動"を選択した場合
            {
                // プレイヤーの行動
                yield return player.PlayAction();

                // 敵の行動カテゴリ(移動, 行動)を決定
                foreach (var enemy in floor.Enemies)
                    (enemy.RequestActCategory() == ActCategory.Action ? acts : moves).Add(enemy);

                // 敵の行動
                foreach (var character in acts)
                    yield return character.PlayAction();
                
                // 敵の移動
                foreach (var character in moves) 
                    character.Move();;
                yield return new WaitForSeconds(DynamicParameter.StepTime);
            }
            
            // ターン終了処理
            // 各キャラの状態異常効果を呼び出す。
            foreach (var character in floor.Characters)
                foreach (var state in character.GetStates)
                    state.TurnEnd();
            yield return new WaitUntil(()=>Enable);
        }
    }
}
