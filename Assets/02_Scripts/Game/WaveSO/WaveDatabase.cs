using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveDatabase", menuName = "Wave/WaveDatabase")]
public class WaveDatabase : ScriptableObject
{
    [SerializeField] private List<WaveData> waveDataList;

    public WaveData GetWaveData(int waveNumber)
    {
        if (waveNumber - 1 < waveDataList.Count)
            return waveDataList[waveNumber - 1];

        return waveDataList[^1];
    }
}