using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(ToggleGroup))]
public class BallTexturePicker : MonoBehaviour
{
    private ToggleGroup ballTexturesTG;
    private Toggle[] toggles;
    private void Start()
    {
        ballTexturesTG = GetComponent<ToggleGroup>();

        // Last selected toggle in the session
        toggles = ballTexturesTG.GetComponentsInChildren<Toggle>();
        toggles[PlayerData.Instance.currentBall].isOn = true;
    }

    public void SelectBallTexture(bool isOn)
    {
        if (isOn)
        {
            Debug.Log(PlayerData.Instance.ballMaterial.name);

            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    PlayerData.Instance.currentBall = i;
                    PlayerData.Instance.ballMaterial = toggles[i].GetComponentInChildren<Image>().material;
                    Debug.Log(PlayerData.Instance.ballMaterial.name);

                    break;
                }
            }

            /*
            Toggle currentSelection = ballTexturesTG.ActiveToggles().FirstOrDefault();
            Material ballMaterial = currentSelection.GetComponentInChildren<Image>().material;

            PlayerData.Instance.ballMaterial = ballMaterial;
            */
        }
    }
}
