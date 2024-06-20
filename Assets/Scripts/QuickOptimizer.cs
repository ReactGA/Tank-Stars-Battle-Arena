using UnityEngine;

public class QuickOptimizer : MonoBehaviour
{
    [SerializeField] GameObject winPanel, Player, Enemy, SpawnedParent;
    void Update()
    {
        if (winPanel.activeInHierarchy && Player.activeInHierarchy)
        {
            Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            var rbs = Player.GetComponentsInChildren<Rigidbody2D>();
            var sprs = Player.GetComponentsInChildren<SpriteRenderer>();
            Player.transform.GetChild(Player.transform.childCount - 1).gameObject.SetActive(false);
            for (int i = 0; i < rbs.Length; i++)
            {
                rbs[i].bodyType = RigidbodyType2D.Static;
            }
            for (int i = 0; i < sprs.Length; i++)
            {
                sprs[i].enabled = false;
            }

            Enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            var rbs1 = Enemy.GetComponentsInChildren<Rigidbody2D>();
            var sprs1 = Enemy.GetComponentsInChildren<SpriteRenderer>();
            Enemy.transform.GetChild(Enemy.transform.childCount - 1).gameObject.SetActive(false);

            for (int i = 0; i < rbs1.Length; i++)
            {
                rbs1[i].bodyType = RigidbodyType2D.Static;
            }
            for (int i = 0; i < sprs1.Length; i++)
            {
                sprs1[i].enabled = false;
            }

            SpawnedParent.SetActive(false);
        }
    }
}
