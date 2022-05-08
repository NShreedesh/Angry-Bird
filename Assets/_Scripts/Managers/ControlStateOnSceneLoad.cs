using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlStateOnSceneLoad : MonoBehaviour
{
    [SerializeField] private GameManager.GameState gameState;

    private void Awake()
    {
        GameManager.Instance.UpdateGameState(gameState);
    }
}
