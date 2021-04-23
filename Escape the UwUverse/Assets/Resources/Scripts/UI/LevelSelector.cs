using UnityEngine;

namespace UwUverse
{
    public class LevelSelector : MonoBehaviour
    {
        private GameObject in_levelPrefab;

        // Start is called before the first frame update
        private void Start()
        {
            in_levelPrefab = Resources.Load<GameObject>("Prefabs/Level");
            for (int i = 1; i < GameController.Instance.GetLevelAmount() + 1; i++)
            {
                GameObject level = Instantiate(in_levelPrefab, transform);
                level.GetComponent<SelectLevel>().Setup(i);
            }
        }
    }
}