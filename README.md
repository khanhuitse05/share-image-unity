# General Sharing Image in Android & iOS in Unity
# Introduction
Today we know many apps and games which provide general sharing of messages, scores, promotional images and much more. So here I’ll explain you how to accomplish this stuff using native code of java for android games and native code of objective-c for iOS games. Using my few lines of code you will be able to share messages and photos in your games on any android device as well as on iOS device.
# Android setup
If you already have a file ```Plugins/Android/AndroidManifest.xml``` following this step to clear you source.
- Copy ```<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />``` and past in your ```AndroidManifest.xml``` file.
- Remove ```ShareMobile/Plugins/Android``` Folder.
