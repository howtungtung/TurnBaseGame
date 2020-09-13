using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDetailUI : MonoBehaviour
{
    public static EnemyDetailUI Instance { private set; get; }
    public Text nameText;
    public Text hpText;
    public Text atkText;
    public Text actionTimeText;
    public GameObject root;
    private Vector3 worldPos;
    public Vector3 offset;
    private void Awake()
    {
        Instance = this;
    }

    public void Show(CharacterData characterData, Vector3 worldPos)
    {
        if (root.activeInHierarchy)
            return;
        root.SetActive(true);
        nameText.text = characterData.name;
        hpText.text = characterData.hp.ToString();
        atkText.text = characterData.atk.ToString();
        actionTimeText.text = characterData.actionTime.ToString();
        this.worldPos = worldPos + offset;
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    private void Update()
    {
        if (root)
            transform.position = Camera.main.WorldToScreenPoint(worldPos);
    }
}
