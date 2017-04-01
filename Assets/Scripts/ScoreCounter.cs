﻿//
//  Copyright (c) 2017  FederationOfCoders.org
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using UnityEngine;
using System;

namespace Deepio {
    [Serializable]
    public class Level {
        public int index;
        public int neededScore;
        public int scoreToNextLevel;
        public int respawnLevel;
        public bool givesUpgradePoint;
    }

    public class ScoreCounter : MonoBehaviour {
        public StatsHolder stats;

        [Space]
        public int levelIndex;
        public int score;
        public int upgradePoints;
        public Level[] levels;
        public Level currentLevel;

        int lastScore;

        void Start() {
            currentLevel = ComputeLevel();
            lastScore = score;
        }

        void Update() {
            if (lastScore != score) {
                currentLevel = ComputeLevel();
                lastScore = score;
            }
        }

#if UNITY_EDITOR
        void OnValidate() {
            if (levels.Length > 0) {
                for (int i = 0; i < levels.Length; i++) {
                    Level level = levels[i];
                    level.index = i;
                }

                for (int i = 0; i < levels.Length - 1; i++) {
                    Level level = levels[i];
                    Level nextLevel = levels[i + 1];
                    level.scoreToNextLevel = Mathf.Max(nextLevel.neededScore - level.neededScore, 0);
                }

                levels[levels.Length - 1].scoreToNextLevel = 0;
            }

            currentLevel = ComputeLevel();
        }
#endif

        Level ComputeLevel() {
            for (int i = 0; i < levels.Length - 1; i++) {
                Level level = levels[i];
                Level nextLevel = levels[i + 1];
                if (level.neededScore <= score) {
                    if (level.index > levelIndex && level.givesUpgradePoint) upgradePoints++;
                    if (score < nextLevel.neededScore) {
                        levelIndex = level.index;
                        return level;
                    }
                }
            }

            Level lastLevel = levels[levels.Length - 1];
            if (lastLevel.index > levelIndex && lastLevel.givesUpgradePoint) upgradePoints++;
            levelIndex = lastLevel.index;
            return levels[levels.Length - 1];
        }
    }
}
