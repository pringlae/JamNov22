using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigame : Minigame
{
    [SerializeField] private Fish[] _fishPrefabs;
    [SerializeField] private int _spawnFishCount;
    [SerializeField] private Hook _hook;

    private static int _catched = 0;

    private List<Fish> _spawnedFishes;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _spawnFishCount - _catched; i++)
        {
            var newFish = _fishPrefabs[Random.Range(0, _fishPrefabs.Length)];
            newFish = Object.Instantiate(newFish, new Vector3(Random.value * 30 - 8, Random.value * 20 - 7, 0), Quaternion.identity, transform);
            newFish.transform.localScale = Vector3.one * (Random.value + 1);
        }
        Camera.main.orthographicSize = 8;
    }

    public override void Close()
    {
        if (_hook.Deactivated && _hook.HookedFish != null)
        {
            _catched++;
            _success = _catched >= _spawnFishCount;
        }
        base.Close();
    }
}
