using UnityEngine;

namespace PeepCam
{
    public class Main : IMod
    {
        private GameObject go;

        public Main()
        {
            SetupKeyBinding();
        }

        public void onEnabled()
        {
            go = new GameObject();
            PeepCam.Instance = go.AddComponent<PeepCam>();
        }

        public void onDisabled()
        {
            Object.Destroy(go);
        }

        private void SetupKeyBinding()
        {
            KeyGroup group = new KeyGroup(Identifier);
            group.keyGroupName = Name;

            InputManager.Instance.registerKeyGroup(group);

            RegisterKey("enterPeepCam", KeyCode.P, "Toggle PeepCam",
                "Use this key to enter/exit PeepCam");
        }

        private void RegisterKey(string identifier, KeyCode keyCode, string name, string description = "")
        {
            KeyMapping key = new KeyMapping(Identifier + "/" + identifier, keyCode, KeyCode.None);
            key.keyGroupIdentifier = Identifier;
            key.keyName = name;
            key.keyDescription = description;
            InputManager.Instance.registerKeyMapping(key);
        }

        public string Name { get { return "PeepCam"; } }
        public string Description { get { return "Camera mod to become a guest and visit your own park"; } }
        public string Identifier { get { return "TheMasterCado@PeepCam"; } }
    }
}
