# State Machine Pattern

## Purpose and Objective

The purpose of implementing this State Machine in our project was to establish an efficient and flexible system for managing the various states and behaviors of game entities. Initially inspired by the State Machine tutorial on the [iHeartGameDev](https://youtu.be/qsIiFsddGV4?si=-U1lZttXKdjt2RXs) YouTube channel, this implementation has been further refined and customized to better suit the specific needs and dynamics of our game. 

## Advantages

**Modularity:** Each state is a separate class, promoting clean code organization and making it easier to manage and extend individual states.

**Scalability:** The design allows for easy addition of new states, making the system highly scalable and adaptable to growing project needs.

**Encapsulation:** State-specific logic is encapsulated within each state class, reducing complexity and improving code readability.

**Reusability:** Generic design of the StateMachine and BaseState classes allows for reuse across different contexts and projects, enhancing code maintainability.

**Separation of Concerns:** By isolating state behaviors from each other and from the main game logic, the architecture supports a clear separation of concerns.

**Flexibility in State Transitions:** Supports dynamic and conditional state transitions, providing robust control over how and when state changes occur.
code.

**Consistent Interface:** The abstract BaseState class ensures a consistent interface for all states, simplifying the integration with the StateMachine.

**Reduced Coupling:** Low coupling between states and the rest of the application promotes a more maintainable and testable codebase.

## State Machine Architecture

### BaseState
The BaseState\<EState> class serves as the foundation of our State Machine architecture. It is an abstract generic class designed to define the basic structure and behavior of various states within the system. Each state in our State Machine is derived from this base class, ensuring a consistent and unified framework for state management. 

``` csharp 
public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState key)
    {
        StateKey = key;
    }
    public EState StateKey { get; private set; }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
    public abstract void OnTriggerExit(Collider other);
}
```
<br>

**Generic Type Parameter (EState):**
The BaseState class is generic, allowing it to be flexible and reusable for different types of states. The EState type parameter, constrained to be an Enum, represents the unique identifier for each state, facilitating easy recognition and transition between states.

``` csharp 
public abstract class BaseState<EState> where EState : Enum
```
<br>

**Constructor and StateKey Property:**
The constructor of the BaseState class takes an EState enum value, which is used to uniquely identify the state. This value is stored in the StateKey property, providing a reference to the specific state instance.

``` csharp 
public BaseState(EState key)
{
    StateKey = key;
}
public EState StateKey { get; private set; }
```
<br>

**Abstract Methods:**
The class defines several abstract methods – EnterState, ExitState, UpdateState, OnTriggerEnter, OnTriggerStay, and OnTriggerExit. These methods are intended to be overridden in derived state classes to implement the specific behavior and actions to be performed when entering a state (EnterState), exiting a state (ExitState), during each frame the state is active (UpdateState), and during collision or trigger events in Unity (OnTriggerEnter, OnTriggerStay, OnTriggerExit).

``` csharp 
public abstract void EnterState();
public abstract void ExitState();
public abstract void UpdateState();
public abstract void OnTriggerEnter(Collider other);
public abstract void OnTriggerStay(Collider other);
public abstract void OnTriggerExit(Collider other);
```

### StateMachine

The StateMachine\<EState> class is a central component of our state management system, designed to handle the lifecycle and transitions of various states in a unified and organized manner.

**Generic Type Parameter (EState):**
The generic nature of the StateMachine class allows it to work with any enum type, making it versatile and adaptable for various state identifiers. The EState type parameter represents the different states the machine can handle.

``` csharp 
public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
```
<br>

**State Management:**
The class maintains a dictionary (States) of states, where each state is an instance of a class derived from BaseState\<EState>. This dictionary maps each state identifier to its corresponding state instance, facilitating efficient state retrieval and management.

``` csharp 
protected Dictionary<EState, BaseState<EState>> States = new();

//Example of Usage
States.Add(GameState.State1, new FirstState());
States.Add(GameState.State2, new SecondState());
States.Add(GameState.State3, new ThirdState());
```
<br>

**Current State Handling:**
The CurrentState property holds a reference to the currently active state. This allows the state machine to perform operations based on the current state and manage transitions seamlessly.

``` csharp 
public BaseState<EState> CurrentState { get; protected set; }
```
I made this public. In an example scenario, if you run past an Enemy, the enemy can easily access your CurrentState but cannot change it.

<br>

**State Transition Logic:**
The StateMachine class provides methods like TransitionToState and QueueNextState to manage state transitions. These methods ensure that state changes are handled smoothly, with proper execution of exit and enter behaviors of respective states.

``` csharp 
private EState queuedState;
private bool hasQueuedState = false;

protected virtual void Update()
{
    CurrentState.UpdateState();

    if (hasQueuedState)
    {
        TransitionToState(queuedState);
        hasQueuedState = false;
    }
}

protected void TransitionToState(EState stateKey)
{
    if (CurrentState.StateKey.Equals(stateKey)) return;

    CurrentState.ExitState();
    CurrentState = States[stateKey];
    CurrentState.EnterState();
}

public void QueueNextState(EState stateKey)
{
    queuedState = stateKey;
    hasQueuedState = true;
}
```
<br>

**Lifecycle Methods:**
The class overrides Unity's lifecycle methods such as Start and Update. In Start, the initial state is entered, and in Update, the machine checks for and executes any queued state transitions. It also calls the UpdateState method of the current state, ensuring that the state's logic is processed each frame.

``` csharp 
protected virtual void Start()
{
    CurrentState.EnterState();
}

protected virtual void Update()
{
    CurrentState.UpdateState();

    if (hasQueuedState)
    {
        TransitionToState(queuedState);
        hasQueuedState = false;
    }
}
```
<br>

**Collision and Trigger Event Handling:**
The StateMachine class includes virtual methods for handling Unity's collision and trigger events (OnTriggerEnter, OnTriggerStay, OnTriggerExit). These methods delegate the events to the current state, allowing states to respond to collision and trigger events in a context-specific manner.

``` csharp 
protected virtual void OnTriggerEnter(Collider other)
{
    CurrentState.OnTriggerEnter(other);
}

protected virtual void OnTriggerStay(Collider other)
{
    CurrentState.OnTriggerStay(other);
}

protected virtual void OnTriggerExit(Collider other)
{
    CurrentState.OnTriggerExit(other);
}
```
If you wish, you can use OnCollisionEnter, OnCollisionStay, OnCollisionExit events instead of OnTriggerEnter, OnTriggerStay, OnTriggerExit events or at the same time.

## Example of Usage
The PlayerStateMachine class is a specialized implementation of the generic StateMachine<EState> framework, tailored specifically for managing the states of a player character in a game environment. 
``` csharp 
public class PlayerStateMachine : StateMachine<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        Idle,
        Walk
    }

    void Awake()
    {
        States.Add(PlayerState.Idle, new IdleState());
        States.Add(PlayerState.Walk, new WalkState());

        CurrentState = States[PlayerState.Idle];
    }

    // Other methods and Unity event handlers...
}
```
This class exemplifies how to extend the StateMachine framework to suit specific character behaviors and state transitions.
``` csharp 
protected override void Update()
{
    if (Input.GetKey(KeyCode.W))
    {
        QueueNextState(PlayerState.Walk);
    }
    else
    {
        QueueNextState(PlayerState.Idle);
    }

    base.Update();
}
```
<br>
The State class, derived from BaseState, represents the state of the player character when they are not engaged in active movement or actions. This state is typically the default state for the player and is crucial for depicting the character’s passive behavior.

``` csharp 
public class WalkState : BaseState<PlayerStateMachine.PlayerState>
{
    public WalkState() : base(PlayerStateMachine.PlayerState.Walk)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entered Walk State");
    }

    public override void ExitState()
    {
        Debug.Log("Exited Walk State");
    }

    public override void UpdateState()
    {
        Debug.Log("Walking");
    }

    // Other methods and Unity event handlers...
}
```

## Customizing the State Machine for Your Project

Every game or application has unique requirements and challenges. Our State Machine and associated state classes are designed with flexibility and extensibility in mind, allowing you to adapt and extend them to meet the specific needs of your project.
<br>

If you prefer to handle state transitions within the state classes themselves, as seen in the iHeartGameDev's video, you can easily modify the structure to allow for this. Instead of managing all transitions through the PlayerStateMachine, each state can have the capability to initiate its own transitions.

``` csharp 
public class RunState : BaseState<PlayerStateMachine.PlayerState>
{
    private PlayerStateMachine _stateMachine;

    public RunState() : base(PlayerStateMachine.PlayerState.Run)
    {
        _stateMachine = stateMachine;
    }

    // Other methods and Unity event handlers...
}
```
<br>
For states like WalkState and RunState, where the only difference is a parameter such as the player's speed, you can utilize parameterized constructors to create more versatile state classes. This approach allows you to pass in specific parameters (like speed) when creating the state instance, making the state more dynamic and adaptable.

``` csharp 
public class MoveState : BaseState<PlayerStateMachine.PlayerState>
{
    private PlayerStateMachine _stateMachine;
    private float _speed;

    public MoveState(PlayerStateMachine stateMachine,
                     PlayerStateMachine.PlayerState stateKey,
                     float speed) : base(stateKey)
    {
        _stateMachine = stateMachine;
        _speed = speed;
    }

    // Other methods and Unity event handlers...
}
```
``` csharp 
States.Add(PlayerState.Walk, new MoveState(this, PlayerState.Walk, walkSpeed));

States.Add(PlayerState.Run, new MoveState(this, PlayerState.Run, runSpeed));
```

## Conclusion
I've done my best to enhance the State Machine concept as presented in the iHeartGameDev's [video](https://youtu.be/qsIiFsddGV4?si=-U1lZttXKdjt2RXs), striving to refine and improve upon the original design. I highly recommend reading the comments under the video as they offer a wealth of insights. The ideas and perspectives shared by other developers have been incredibly inspiring to me.

This project, in its current form, certainly has room for further development and customization. For instance, instead of using a dictionary, you might explore creating a more dynamic structure. However, I chose this approach as I believe it offers better performance for our needs. If you have any suggestions or ideas for improvement, please don't hesitate to share them with me. Your input is always valuable.

