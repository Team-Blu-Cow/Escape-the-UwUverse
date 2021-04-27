// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input/MasterInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MasterInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MasterInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MasterInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""5d58fab7-eb4c-4d3c-bc0b-1cdb68622cfd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""84d5ff4b-edb4-437e-96ad-43ce52cf21f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Idle"",
                    ""type"": ""Button"",
                    ""id"": ""9fa310f3-5585-4df6-9290-74cf90e036a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a83de74f-628f-4510-8cc3-900192d9d9aa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""83d8fef2-bb0c-4d33-99ca-9d8cc85252a4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f1c5f102-ec3d-4ed0-b571-0afb35748b33"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9e571fcb-1a45-4797-8073-ee44ec908337"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c1678b83-652d-435d-ba3d-c4f5a45022f9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""ae604c37-a06f-454e-85f7-304aba19b169"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6737f556-6263-4947-895a-f5a8c8434e95"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""33b039dd-2851-47f3-b13d-0927a9008841"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""974d2adc-d597-4852-94d4-587669fd3d05"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c4933b58-e32d-43ac-ac6e-1b02575c6ad7"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d5345743-102e-4579-ac44-60dc5344a04f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Idle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerShoot"",
            ""id"": ""c034949e-a027-403d-926d-9528558a9401"",
            ""actions"": [
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""632e7911-c6de-4ea3-bef1-eacffa300a4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Direction"",
                    ""type"": ""Button"",
                    ""id"": ""d42260aa-1d7c-40e5-9e16-4534597fd2bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Undo"",
                    ""type"": ""Button"",
                    ""id"": ""459bd737-cc02-4e4d-af93-082b22f6ffe1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""55dda2f6-6644-4fcd-bb59-51fe85cf5fb8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6fad83ed-3b2a-4eeb-aefb-d2073f3570fc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f26eca50-8f33-42ab-be36-41543fdda687"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d47c9d5c-16e1-4f5a-b4ed-63a6daa2524f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""67c7113b-8311-4e2c-bb19-619a1e5b9da4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""c2ee025e-e6b6-4517-a3bb-e8b1164da12a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b7f7742c-24fa-4560-9dfb-1efc7cb0bad6"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f4fa6a93-ea44-4c51-a644-7e87647accff"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ec3fdf5-f492-44b4-a890-53c5daa9bf96"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dde9d5d5-67f8-4567-8f57-a9c5ab0f92a7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c8df8990-f9c3-40fc-8100-7c5e13dcc527"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2e80ddf-4e3b-4fc0-bec1-6c7b32e19aec"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Undo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interact"",
            ""id"": ""df719fd2-3e12-4a89-8da5-946115c4a126"",
            ""actions"": [
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""7a0d39bb-7cbe-43be-a793-3bc309a90d71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""9ffd60c8-9a11-407d-a53c-417205493fb1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d3212281-dcbb-4a31-be42-c1ffc6088088"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""787a5a24-4e6c-4553-9bc3-22822ef401ac"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2168459f-ed5e-4685-b5b4-9079c358b373"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5f32f13-74fa-4bb9-b715-04128c195623"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""889248e0-8f54-43c1-b5b1-6205ba22ca2c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""153b67ae-0b96-4c5b-a342-408de35706c5"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Move = m_PlayerMovement.FindAction("Move", throwIfNotFound: true);
        m_PlayerMovement_Idle = m_PlayerMovement.FindAction("Idle", throwIfNotFound: true);
        // PlayerShoot
        m_PlayerShoot = asset.FindActionMap("PlayerShoot", throwIfNotFound: true);
        m_PlayerShoot_Mouse = m_PlayerShoot.FindAction("Mouse", throwIfNotFound: true);
        m_PlayerShoot_Direction = m_PlayerShoot.FindAction("Direction", throwIfNotFound: true);
        m_PlayerShoot_Undo = m_PlayerShoot.FindAction("Undo", throwIfNotFound: true);
        // Interact
        m_Interact = asset.FindActionMap("Interact", throwIfNotFound: true);
        m_Interact_Cancel = m_Interact.FindAction("Cancel", throwIfNotFound: true);
        m_Interact_Confirm = m_Interact.FindAction("Confirm", throwIfNotFound: true);
        m_Interact_Pause = m_Interact.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Move;
    private readonly InputAction m_PlayerMovement_Idle;
    public struct PlayerMovementActions
    {
        private @MasterInput m_Wrapper;
        public PlayerMovementActions(@MasterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerMovement_Move;
        public InputAction @Idle => m_Wrapper.m_PlayerMovement_Idle;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMove;
                @Idle.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnIdle;
                @Idle.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnIdle;
                @Idle.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnIdle;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Idle.started += instance.OnIdle;
                @Idle.performed += instance.OnIdle;
                @Idle.canceled += instance.OnIdle;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerShoot
    private readonly InputActionMap m_PlayerShoot;
    private IPlayerShootActions m_PlayerShootActionsCallbackInterface;
    private readonly InputAction m_PlayerShoot_Mouse;
    private readonly InputAction m_PlayerShoot_Direction;
    private readonly InputAction m_PlayerShoot_Undo;
    public struct PlayerShootActions
    {
        private @MasterInput m_Wrapper;
        public PlayerShootActions(@MasterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse => m_Wrapper.m_PlayerShoot_Mouse;
        public InputAction @Direction => m_Wrapper.m_PlayerShoot_Direction;
        public InputAction @Undo => m_Wrapper.m_PlayerShoot_Undo;
        public InputActionMap Get() { return m_Wrapper.m_PlayerShoot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerShootActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerShootActions instance)
        {
            if (m_Wrapper.m_PlayerShootActionsCallbackInterface != null)
            {
                @Mouse.started -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnMouse;
                @Direction.started -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnDirection;
                @Undo.started -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnUndo;
                @Undo.performed -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnUndo;
                @Undo.canceled -= m_Wrapper.m_PlayerShootActionsCallbackInterface.OnUndo;
            }
            m_Wrapper.m_PlayerShootActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
                @Undo.started += instance.OnUndo;
                @Undo.performed += instance.OnUndo;
                @Undo.canceled += instance.OnUndo;
            }
        }
    }
    public PlayerShootActions @PlayerShoot => new PlayerShootActions(this);

    // Interact
    private readonly InputActionMap m_Interact;
    private IInteractActions m_InteractActionsCallbackInterface;
    private readonly InputAction m_Interact_Cancel;
    private readonly InputAction m_Interact_Confirm;
    private readonly InputAction m_Interact_Pause;
    public struct InteractActions
    {
        private @MasterInput m_Wrapper;
        public InteractActions(@MasterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cancel => m_Wrapper.m_Interact_Cancel;
        public InputAction @Confirm => m_Wrapper.m_Interact_Confirm;
        public InputAction @Pause => m_Wrapper.m_Interact_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Interact; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractActions set) { return set.Get(); }
        public void SetCallbacks(IInteractActions instance)
        {
            if (m_Wrapper.m_InteractActionsCallbackInterface != null)
            {
                @Cancel.started -= m_Wrapper.m_InteractActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_InteractActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_InteractActionsCallbackInterface.OnCancel;
                @Confirm.started -= m_Wrapper.m_InteractActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_InteractActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_InteractActionsCallbackInterface.OnConfirm;
                @Pause.started -= m_Wrapper.m_InteractActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_InteractActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_InteractActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_InteractActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public InteractActions @Interact => new InteractActions(this);
    public interface IPlayerMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnIdle(InputAction.CallbackContext context);
    }
    public interface IPlayerShootActions
    {
        void OnMouse(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
        void OnUndo(InputAction.CallbackContext context);
    }
    public interface IInteractActions
    {
        void OnCancel(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
