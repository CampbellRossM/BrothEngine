using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Broth.Engine.Util
{
    public class GameTime
    {
        private readonly Clock _clock = new Clock();
        private Time
            _fixedTimeStep,
            _previousFrameStartTime,
            _currentFrameStartTime;

        /// <summary> The amount of time to advance the game world between frames. </summary>
        public Time FixedDeltaTime { get { return _fixedTimeStep; } }

        /// <summary> The amount of time that passed during the last frame. </summary>
        public Time DeltaTime { get { return _currentFrameStartTime - _previousFrameStartTime; } }

        /// <summary> The amount of time that has passed since the clock started. </summary>
        public Time TotalTimeElapsed { get { return _clock.ElapsedTime; } }

        /// <summary> The amount of time that has passed since the start of the current frame. </summary>
        public Time TimeElapsedSinceFrameStart { get { return _clock.ElapsedTime - _currentFrameStartTime; } }

        public GameTime(uint fixedStepsPerSecond)
        {
            _fixedTimeStep = Time.FromSeconds(1f / fixedStepsPerSecond);
            _previousFrameStartTime = _clock.ElapsedTime;
            _currentFrameStartTime = _clock.ElapsedTime;
        }

        public void NextFrame()
        {
            _previousFrameStartTime = _currentFrameStartTime;
            _currentFrameStartTime = _clock.ElapsedTime;
        }

    }
}
