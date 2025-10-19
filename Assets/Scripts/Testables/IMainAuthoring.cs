using UnityEngine;
using TouchReplaying;

namespace EthanKennerly.SaveLife
{
    public interface IMainAuthoring
    {
        Transform Parent { get; }
        IAgeUpAuthoring AgeUp { get; }
        GameObject DeathPopupPrefab { get; }
        ITouchRecordingAuthoring TouchRecording { get; }
        ITouchReplayAuthoring TouchReplay { get; }
    }
}
