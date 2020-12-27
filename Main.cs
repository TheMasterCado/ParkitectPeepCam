using UnityEngine;

namespace PeepCam
{
    public class Main : AbstractMod
    {
        private GameObject go;

        public Main()
        {
            SetupKeyBinding();
        }

        public override void onEnabled()
        {
            go = new GameObject();
            PeepCam.Instance = go.AddComponent<PeepCam>();
        }

        public override void onDisabled()
        {
            Object.Destroy(go);
        }

        private void SetupKeyBinding()
        {
            KeyGroup group = new KeyGroup(getIdentifier());
            group.keyGroupName = getName();

            InputManager.Instance.registerKeyGroup(group);

            RegisterKey("enterPeepCam", KeyCode.P, "Toggle PeepCam",
                "Use this key to enter/exit PeepCam");
        }

        private void RegisterKey(string identifier, KeyCode keyCode, string name, string description = "")
        {
            KeyMapping key = new KeyMapping(getIdentifier() + "/" + identifier, keyCode, KeyCode.None);
            key.keyGroupIdentifier = getIdentifier();
            key.keyName = name;
            key.keyDescription = description;
            InputManager.Instance.registerKeyMapping(key);
        }

        public override bool isMultiplayerModeCompatible()
        {
            return true;
        }

        public override bool isRequiredByAllPlayersInMultiplayerMode()
        {
            return false;
        }

        public override string getName()
        {
            return "PeepCam";
        }

        public override string getDescription()
        {
            return "Camera mod to become a guest and visit your own park";
        }

        public override string getVersionNumber()
        {
            return "1.2";
        }

        public override string getIdentifier()
        {
            return "TheMasterCado@PeepCam";
        }
    }
}
