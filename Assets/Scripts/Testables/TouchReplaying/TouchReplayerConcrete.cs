using System.Collections.Generic;
using UnityEngine;

namespace TouchReplaying
{
    public class TouchReplayerConcrete : TouchReplayer, ITouchReplayer
    {
        public TouchReplayerConcrete(
            List<TouchLogEntry> log,
            Canvas uiCanvas,
            Sprite indicatorSprite,
            Color indicatorColor)
            : base(log, uiCanvas, indicatorSprite, indicatorColor) { }
    }
}