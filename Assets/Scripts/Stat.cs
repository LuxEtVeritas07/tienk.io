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
    public class Stat : MonoBehaviour {
        StatsHolder holder;

        [SerializeField, Range(0, 7)]
        protected int _statLevel;
        int lastStatLevel = -1;
        int statLevel {
            get { return _statLevel; }
            set {
                if (value < 0 || value > 7)
                    throw new ArgumentOutOfRangeException("value", value, "value must be >= 0 and <= 7");
                _statLevel = value;
            }
        }

        public float baseValue, holderLevelBonus, statLevelBonus;

        float lastStatValue;
        int lastHolderLevel;
        public float statValue {
            get {
                if (_statLevel != lastStatLevel || holder.level != lastHolderLevel) {
                    lastStatValue = baseValue + holderLevelBonus * holder.level + statLevelBonus * _statLevel;
                    lastStatLevel = _statLevel;
                    lastHolderLevel = holder.level;
                }
                return lastStatValue;
            }
        }

        void Start() {
            holder = StatsHolder.instance;
        }
    }
}