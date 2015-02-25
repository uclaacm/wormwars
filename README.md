Worm Wars
---

We are the AI@UCLA Gaming Group.

Worm Wars is an omnidirectional, competitive version of Snake. In a way it's a
combination of Snake and Tron, where the objective is to be the first to grow
to a certain size, while preventing your opponents from doing so before you.

Our first objective is to be able to write intelligent bots that can compete on
equal footing with human players. Our second, of course, is to have fun!

Gameplay
---

In the Executable folder, there is a pre-built 32-bit Windows binary along with
game assets. It should be able to run on an x86 Windows system. See the
Development section below if you want to build and run it yourself.

The gameplay is extremely basic for now. In Single Player mode, you control a
worm that grows by eating cheese. To launch AI players, press space. The player
count is capped at 8. In Observe mode, you don't control a worm but are allowed
to drag the camera around, while AI players can be launched the same way.

Development
---

The game is being built using the Unity game engine. [Download][0] and install
the free version if you don't have it already.

Included in this repository are the complete editor settings and assets of the
project. To set up:

1. Clone or download the repo as zip
2. Make sure the root folder is named "wormwars" if not already
3. Open Assets/scene.unity

To write your own AIs:

1. Create a script file in **Assets/Plugin Scripts**. C# or Javascript works
2. Edit the `scripts` array in **Assets/Standard Assets/WormWar Scripts/ComputerScripts.cs**
to point to your script name instead of ComputerController
3. To control your worm, use the `GetMoveDirection` and `SetMoveDirection`
functions from HeadLogic.cs

[0]: http://unity3d.com/unity/download