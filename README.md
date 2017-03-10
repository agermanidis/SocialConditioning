<img src="socialconditioning.gif" width=700>

[Demo](https://vimeo.com/207685415)

## Social Conditioning 

Hololens app built with Unity that translates people's facial expressions into weather.

To install to your Hololens:

0. Make sure you have all the [Hololens requirements](https://developer.microsoft.com/en-us/windows/holographic/install_the_tools) installed.
1. Clone this repo to your local system.
2. Open the folder in Unity as a project.
3. Open the `SocialConditioning.unity` scene withint that project.
4. Sign up for [Microsoft Cognitive Services Face API](https://www.microsoft.com/cognitive-services/en-us/face-api) and get a key.
5. Sign up for [Microsoft Cognitive Services Emotion API](https://www.microsoft.com/cognitive-services/en-us/emotion-api) and get a key.
6. Click to the `Main Camera` object in the scene. Under the `Manager (Script)` in the Inspector, set your Face API and Emotion API keys.
7. Go to `File > Build Settings`, select `Windows Store`, and click Build. Create a new folder inside the project and select it.
8. Go to the new folder with Windows File Explorer and then open SocialConditioning.sln with Visual Studio.
9. Connect your Hololens with USB.
10. On the top toolbar of Visual Studio, change the target from Debug to Release and from ARM to X86.
11. Click on the arrow next to the Local Machine button, and change the deployment target to Device.
12. Press the Device button to run the app.
