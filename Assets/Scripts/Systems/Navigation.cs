using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Dungeon;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class Navigator
    {
        private Floor floor;

        public Navigator(Floor floor)
        {
            this.floor = floor;
        }
    }

    public static class Path
    {

    }
}
