// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMaster : IInputActionCollection
{
    private InputActionAsset asset;
    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9b3ba862-df0e-41ed-94e8-8700916dc86c"",
            ""actions"": [
                {
                    ""name"": ""Place"",
                    ""type"": ""Button"",
                    ""id"": ""b1b8192a-952f-432c-bf00-72edefe5f737"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pan"",
                    ""type"": ""Button"",
                    ""id"": ""b0967025-b230-4272-88b7-08b71721feb8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ce0f29e1-7c06-41a7-b0f1-454a6ee6a9a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""529c8a20-0d2a-4c97-8a2f-2713a7ca2fbd"",
                    ""path"": ""<Touchscreen>/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a433597b-5903-4186-bd71-24de3fc18f43"",
                    ""path"": ""<Touchscreen>/touch0/delta"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55e35026-a2fc-45f7-90fc-f34ab276c220"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Place = m_Player.GetAction("Place");
        m_Player_Pan = m_Player.GetAction("Pan");
    }

    ~InputMaster()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Place;
    private readonly InputAction m_Player_Pan;
    public struct PlayerActions
    {
        private InputMaster m_Wrapper;
        public PlayerActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Place => m_Wrapper.m_Player_Place;
        public InputAction @Pan => m_Wrapper.m_Player_Pan;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Place.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlace;
                Place.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlace;
                Place.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlace;
                Pan.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPan;
                Pan.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPan;
                Pan.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPan;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Place.started += instance.OnPlace;
                Place.performed += instance.OnPlace;
                Place.canceled += instance.OnPlace;
                Pan.started += instance.OnPan;
                Pan.performed += instance.OnPan;
                Pan.canceled += instance.OnPan;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnPlace(InputAction.CallbackContext context);
        void OnPan(InputAction.CallbackContext context);
    }
}
