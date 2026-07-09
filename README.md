# Simple Webcam Overlay

A lightweight always-on-top webcam overlay for Windows.

Built with WPF (.NET Framework 4.8) and AForge.NET.

![Platform](https://img.shields.io/badge/platform-Windows-blue)
![Framework](https://img.shields.io/badge/.NET-Framework%204.8-purple) 
![License](https://img.shields.io/badge/license-MIT-green)

---

## Features

- 📷 Live webcam preview 
- 🖥️ Always-on-top window
- 🪟 Borderless overlay mode
- ↔️ Resizable and draggable window
- 🎥 Camera selection screen 
- 🔄 Multiple application instances supported 
- 🧹 Proper camera resource cleanup

---

## Download 

Download the latest release from the Releases page.

---

## Build from source

### Requirements

- Windows 10 or later 
- .NET SDK 6.0+ (used for building) 
- JetBrains Rider or Visual Studio

### Clone the repository

git clone https://github.com/MingulovTA/simple-webcam-overlay.git

cd simple-webcam-overlay

### Restore packages

dotnet restore src/WebcamOverlay/WebcamOverlay

### Build

dotnet build src/WebcamOverlay/WebcamOverlay

### Run

dotnet run --project src/WebcamOverlay/WebcamOverlay

---

## Usage 

1. Launch the application. 
2. Select a camera from the list. 
3. Drag the overlay anywhere on the screen. 
4. Resize the window as needed. 
5. The window will stay above other windows.

> Note: Most physical webcams can be opened by only one process at a time. 
> If you run multiple instances of the application, select different cameras in each instance.

## Technologies

- WPF
- .NET Framework 4.8 
- AForge 
- AForge.Video.DirectShow

## License 
This project is licensed under the MIT License.