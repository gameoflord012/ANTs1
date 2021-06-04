using UnityEngine;
using Game.Global;
namespace Game.UI
{
    public class LevelBar : MonoBehaviour {
        public void SetBarSprite(int levelId)
        {
            Transform sprite = transform.Find("Sprite");
            sprite.GetComponent<SpriteRenderer>().sprite = LevelBarManager.Instance.levelSprite[levelId];
        }
    }
}