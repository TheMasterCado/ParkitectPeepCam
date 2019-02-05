using System;
using UnityEngine;

namespace PeepCam
{
    public class PlayableGuest
    {
        private Guest _guest;
        public Guest Guest { get { return _guest; } }

        public PlayableGuest(Guest guest)
        {
            _guest = GameObject.Instantiate(guest);
        }
    }
}
