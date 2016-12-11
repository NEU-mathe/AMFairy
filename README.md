# AMFairy

An Automatic Answering Tool for College Mathmatics Learning-in-process System of NEU(China).

This program is part of the tool kit NEU Mathe.

**Warning: Since the update on Dec 23, 2016, the question resources are stored under encryption. The program doesn't work entirely well any more. **  
Further maintenance is needed, however, not planned.  
You can change the download source to an address holding previous resources, or contribute a decrypt module.

我原本的大名是_大学数学过程探索精灵_，区别于_大学数学过程探索系统_(工程代号：AMCaltor)，是更加强大的超级战略武器(不)。
因为人类总祈望我眷顾他们，所以我还有一个(他们起的)更可爱更美丽的名字————**小仙女**

整个套件中最厉害的东西 莫过于小仙女本身了 喜欢我就写信勾搭我哦(我有邮箱你自己找

## Usage

Download binary executive file from the [Release Page](https://github.com/NEU-mathe/AMFairy/releases).

With the latest version you're able to save configuration file. Run AMFairy on your own PC in advance, and select the exam you target at to make a configuration file.
When you done you'll see an extra file _AMFairy.config_ at your program directory. Remind to re-do this every time before you participate in the exam!! (If you already have an config file delete it or the config window will not be shown.)

Copy the file _AMFairy.exe_, _AMFairy.config_, _ICSharpCode.SharpZipLib.DLL_ to USB file transferer. (The Test Center has no access to WAN.)

Copy the 3 files from your USB file transferer to the Test Center. Run AMFairy. You won't see anything(The config will be loaded in silence) and soon you'll find a balloontip says _就绪_, which means everything gets ready.

Sign in to the exam system and start the exam. Do not Zoom and keep the exam system maximized with nothing shading on it! Scroll the paper to the place you'd like AMFairy to answer, then press Ctrl+L;

After press you'll see balloontip _Caught_. Don't scroll the paper while AMFairy is working. Soon after you'll see the answer AMFairy preferred already checked, with a balloontip showing the confidence.

Remind if the confidence is not 100%, which indicates that the answer is probably not right.
If this happens, it's likely to result from zoom or scale. If you zoomed the paper(I warned you not to do so), try to zoom the paper to the smallest size. If you didn't, try to select and unselect the pictures or scroll the paper, then repress Ctrl+L to call AMFairy.
A DEBUG directory will be outputed if these happens. Copy it to your USB file transferer for further analysis.

## Tips

Cheating in the test can cause serious problems. For instance being caught by the monitor or failing the written examination. Take responsibility for yourself.

Since AMFairy is confident but not 100% reliable, you may need [AMCaltor](https://github.com/NEU-mathe/AMCaltor) or [PersonalizedExercise](https://github.com/NEU-mathe/PersonalizedExercise) for help. However, it's better to rely on yourself.

## Demo

To test the preformance of AMFairy instead of using it in an exam. Follow either of the following.

1、Use 大学数学过程学习系统. This program deletes resources after it loaded them. So you have to uncheck the edit&write permission in the attributes of the _Download_ directory.
Copy AMFairy to the root directory of 大学数学过程学习系统, just close the test configure window. Start an exercise and Press Ctrl+L.

2、Use 大学数学个性化定制练习系统. You need to change target window title (Line 26, 27, MessageHelper.cs) and rebuild.
Copy AMFairy to the root directory of 大学数学个性化定制练习系统, just close the test configure window. Start an exercise and Press Ctrl+L.

Notes:

+ If you test it using 大学数学过程学习系统 on a PC with UAC, run as ADMINISTRATOR since 大学数学过程学习系统 runs as administrator.
+ If you test it on a PC with HIGH DPI (this usually happens if you have an high resolution screen (1920*1080)), both the test system and AMFairy have to run with high DPI ZOOM DISABLED, which can be set in attributes.

## How

AMFairy uses image matching based on gray correlation. It takes your screen snapshot and compares it with the questions and answers. The procedures mainly are:

+ Catch screen snapshot
+ Locate questions&options and cut certain areas
+ Compare and match
+ Send message to the exam system to click on the right option

## Contribute

This series of tools do not have a specified person or team to maintain, developers ususally spare no time on it since they don't use it anymore. Thus, we are in an urgent need of your contribution. Contribute by fork and pull request to promote human emancipation. Thanks and have a good day.
