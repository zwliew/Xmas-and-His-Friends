# Xmas and His Friends
A Unity mobile game built as part of the 2018 National Chinese Mobile App Development By Students For Students Competition organized by the Singapore Centre for Chinese Language.

The game aims to provide elementary school students in Singapore with an interesting and interactive mode of learning Chinese.

More details of the game and the competition can be found [here](https://appcompetition.tk.sg/).

## The team
Xmas and His Friends is built by a team of 4 students:
 - [Pan Taixi](https://github.com/annnnnnnnnnie)
 - [Han Jiale](https://github.com/josephaureliano)
 - Xu Yitian
 - [Liew Zhao Wei](https://github.com/zwliew)

## Introductory video
https://drive.google.com/file/d/1SGgtpVD__0JMjhDR0ExK3PdNnGKnIGKe

## Downloads
[Google Play Store](https://play.google.com/store/apps/details?id=sg.sccl.oc004)
[Apple App Store](https://itunes.apple.com/sg/app/xmas-and-his-friends-%E5%9C%A3%E8%AF%9E%E5%92%8C%E6%9C%8B%E5%8F%8B%E4%BB%AC/id1423507735)

## Technical information

This application contains five scenes: Intro, RoomSelection, HomeScreen, NewPinZiGame, and Maze.
They are all stored under Assets/Scenes/, except for Maze which is stored in Assets/Scenes/Archives/.

The scripts are stored in Assets/Script/.

By adding data to Assets/Resources/PinZiPianPang/PinZiData.json and ./Maze/CharSpawnerUTF.txt,
more in-game content can easily be added.
The sprites used for PinZiGame are also under Assets/Resources/PinZiPianPang/.

When optimizing performance, we:
1. implemented object pooling in maze game.
2. used fewer features of the post-processing stack for HomeScreen and Maze.

## Learning points
1. Game design and programming is hard
2. Good game design and programming is even harder

The game is not too well optimized performance-wise, but we hope to do better in the future :)
