using System.Collections;
using UnityEngine;
using Utility = Systems.Utility;

namespace Characters
{
    public class SpriteManager : MonoBehaviour
    {
        [SerializeField] private string spriteName;
        public float animSeconds;
        private Sprite[,] sprites = new Sprite[8,3];
        private SpriteRenderer renderer;
        private BattleCharacter battleCharacter;
        private Vector2Int currentDirection;
        private Coroutine coroutine;
    
        void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            battleCharacter = GetComponent<BattleCharacter>();
            InitSprite(Resources.LoadAll<Sprite>("Characters/" + spriteName));
            currentDirection = battleCharacter.Direction;
            coroutine = StartCoroutine(Animation(Utility.GetDirectionIndex(currentDirection)));
        }

        void a()
        {
        
        }

        private IEnumerator Animation(int index)
        {
            while (this != null)
            {
                for (int step = 0; step < 4; step++)
                {
                    renderer.sprite = GetCurrentSprite(index, step);
                    yield return new WaitForSeconds(animSeconds);
                }
            }
        }

        private void FixedUpdate()
        {
            var direction = battleCharacter.Direction;
            if (currentDirection != direction)
            {
                currentDirection = direction;
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(Animation(Utility.GetDirectionIndex(direction)));
            }
        }

        private void InitSprite(Sprite[] rsc)
        {
            int[] spriteOrder = {6, 5, 4, 7, 0, 3, 2, 1};
            if (sprites.Length != 24)
            {
                Debug.LogError("スプライトおかしいよ");
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sprites[spriteOrder[i], j] = rsc[i * 3 + j];
                }
            }
        }

        private Sprite GetCurrentSprite(int index, int step)
        {
            step = step % 4;
            if (step == 3) step = 1;
            return sprites[index, step];
        }
    }
}
