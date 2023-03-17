# ioList
A tool for generating IO Lists using RSLogix 5000 L5X export files.

## Summary
This is a simple WPF windows application that allows you to process an L5X project file
and generate an IO list. IO lists are helpful for tracking and documenting a given
PLC project's IO configuration. These are typically used in a controls FAT or commissioning
to track the IO that has been loop tested with field devices.

If you like or use this project, please leave a star! If you have feedback or issues, 
feel free to create an Issue [here](https://github.com/tnunnink/ioList/issues). Thanks and enjoy!

## Install
To install the application on a windows machine, download the Setup.exe or Setup.msi 
from the current latest releases [here](https://github.com/tnunnink/ioList/releases/latest).
Once installed, the app will check back here for new releases and give you the abaility to update
as desired.

## Usage

The application is very simple as of now. The high level steps are as follows:
1. Launch the application and click the **NewList** button. 
   1. You can also just drag a drop an L5X file which will automatically advance you to the next step with prefilled fields.
2. On the entry page, specify the following information
   1. L5X file to process.
   2. File name of the list to be generated.
   3. File location of the list to be generated.
3. Click **Generate** to start the process of extracting the IO list from the L5X. 
4. Once complete, click **Open In Explorer** to open the directory in which the csv was generated.

## Screenshots

#### Startup Page
![image](/docs/Startup.png)

#### Entry Page
![image](/docs/Entry.png)

#### Entry Filled Page
![image](/docs/EntryFilled.png)

#### Processing Page
![image](/docs/Processing.png)

#### Complete Page
![image](/docs/Complete.png)

## Future
At some point I plan on adding the ability to configure a few things related to how
the application generates the csv file. Right now, the application will automatically
try to find references to the IO tags within the project that have 
MOV or XIC/OTE XIO/OTE signatures. The idea is that this identifies the buffered IO
that is actually used within the project. The generated file will only output
IO that has references of these predefined patterns. This helps filter the list 
significantly to IO that is likely of interest.

At some point I will add the ability to configure whether to actually look for "Buffer"
tags, and if so which patters to look for (other than the configured defaults.)

I also plan on adding some simple configurable filters for automatically filtering the
generated output based on rules you define. For example you may wat to filter out certain
tag names that don't represent "IO" you care about. Or you may want to filter uncommented
IO tags. This would give better flexibility on what is generated. 

Stay tuned.