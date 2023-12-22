Pixisoft Corporations
pixisoft.tw@gmail.com

Inpsect Json package, installation and running notes
1. Package requieres Json.NET to parse the JSON content. The program is under MIT license hence you can download it from here
   https://www.newtonsoft.com/json
2. Make sure you have drag the target binary files to the project's Assets/ folder base on the .NET version you want. We have already
   drag a version (.NET 4.5) located at Assets/Inspect/JSON/DLL/Json.NET/. Please delete the whole folder if this isn't the desire
   .NET version you want.
3. [Optional] Package content from Inspect/JSON/Plugins folder must be placed to your project's root Plugins directory (Assets/Plugins)
   This is described in general Unity documentation, however, on our tests the step was not necessary

Samples:
Package contains one sample JSON file to show how the text is being render inside the inspector window.
1. "Playground" - Demo JSON file that has default supported JSON's generic type in it.

Feel free to contact us should you have any issues or questions regarding package, contact e-mail is stated on top of this document.
