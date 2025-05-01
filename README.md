## Ultrakill Fighting

A 2D fighting game inspired by classics like _Street Fighter_ and _Mortal Kombat_, set in the fast-paced world of _Ultrakill_. It uses original models, sounds, and textures from the base game to deliver an intense and stylized combat experience. This is a personal, non-commercial fan project.

## Project Status

This project is currently in development. Core fighting mechanics are implemented. The final build is done, but there is some bugs with the two player controllers and the game loop is incomplete. Upcoming features include advanced combo systems, additional characters, basic AI for single-player mode, and main menu of the game.

## Project Screen Shots 

![Ultrakill-Fighting-image](https://github.com/user-attachments/assets/b18ffc81-4077-4f96-9cb5-4d6e7fa110ff)

![Ultrakill-Fighting-gif](https://github.com/user-attachments/assets/ec808ee5-6e45-452c-895b-1490366fa29e)


## Installation and Setup Instructions

Clone this repository. The game was developed using Unity, so you'll need `https://unity.com/download` and the version `2022.3.44f1` of the Unity Editor installed.

### Installation and Running Instructions:

 1. Clone the repository with `https://github.com/eduardobr33/Ultrakill-Fighting.git`.
 2.  Open Unity Hub, click "Open Project from Disk", and select the cloned folder.
 3.  Press the Play button inside the Unity Editor to test the game.

_To build the game:_

 - Go to `File > Build Settings`, select your target platform, and click `Build`.

## Reflection

  - ### What was the context for this project?
    This project originally began as a universitary assignment, but over time it evolved into a personal passion project. Inspired by the game _Ultrakill_, it became a way to dive deeper into combat systems, animations, and game mechanics using Unity beyond the original academic scope.
  - ### What were you trying to build?
    A non-commercial fighting game that blends the style of retro fighters with the fast and brutal aesthetic of _Ultrakill_. The goal was to create a fun and playable tribute to the original game.
  - ### What made this challenging?
    It was my first time making a fighting game and using animations with root motion in Unity. 
  - ### What were some unexpected obstacles?
      - Creating a functional hitbox system that feels fair and responsive.
      - Developing a basic AI opponent for single-player mode (in progress).
  - ### Tools used:
      - Game Engine: Unity
      - Language: C#
      - Sprite/Texture Editing: Photoshop
      - Audio Tools: Audacity

#### Example:  

This was a 3 week long project built during my third module at Turing School of Software and Design. Project goals included using technologies learned up until this point and familiarizing myself with documentation for new features.  

Originally I wanted to build an application that allowed users to pull data from the Twitter API based on what they were interested in, such as 'most tagged users'. I started this process by using the `create-react-app` boilerplate, then adding `react-router-4.0` and `redux`.  

One of the main challenges I ran into was Authentication. This lead me to spend a few days on a research spike into OAuth, Auth0, and two-factor authentication using Firebase or other third parties. Due to project time constraints, I had to table authentication and focus more on data visualization from parts of the API that weren't restricted to authenticated users.

At the end of the day, the technologies implemented in this project are React, React-Router 4.0, Redux, LoDash, D3, and a significant amount of VanillaJS, JSX, and CSS. I chose to use the `create-react-app` boilerplate to minimize initial setup and invest more time in diving into weird technological rabbit holes. In the next iteration I plan on handrolling a `webpack.config.js` file to more fully understand the build process.

## Disclaimer

#### This is a fan-made, non-commercial, university project. All rights to characters, models, audio, and textures belong to the original creators of _Ultrakill_ (Arsi "Hakita" Patala and New Blood Interactive).
This project is not afflicted with or endorsed by creators of _Ultrakill_ in any way.
