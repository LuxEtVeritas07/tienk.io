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

using System;
using UnityEngine;

namespace Deepio {
    public class BotSpawner : Singleton<BotSpawner> {
        public GameObject bot;
        public Rect fieldBoundary;
        public int botsCount;

        void Start() {
            for (int i = 0; i < botsCount; i++) SpawnBot();
        }

        public void SpawnBot() {
            Vector2 position = new Vector2(
                UnityEngine.Random.Range(fieldBoundary.x, fieldBoundary.width),
                UnityEngine.Random.Range(fieldBoundary.y, fieldBoundary.height)
            );
            Instantiate(bot, position, Quaternion.identity, transform);
        }
    }
}
