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

## Configuration
Version 0.2.0 of the application adds a configuration interface that allows you to control
what the output looks like. The following sections explain how the application can be configured.
To open the configuration interface, click the cog icon in the bottom left of the application.

### Columns
On the `Columns` tab you can configure which columns get output, change the names of any 
pre-existing or new column, and add new meta data columns that you want output by default.
This saves you the step of manually performing this task each time you generate a new list.

#### Edit Column Name
To edit the name of a default or custom column, click the edit icon on the row that you
want to change. You will see the following interface that will allow you to change the name
or the column.

![image](/docs/EditColumn.png)

![image](/docs/EditColumnName.png)

#### Disable Column
To disable or prevent the column from being written to the output CSV file, click the 
toggle button on any column to disable it like shown here.

![image](/docs/DisableColumn.png)

#### Add Custom Column
To add a new column, click the plus action icon button on the top right. 
This will launch the an interface that allows you to specify the name of the column.

![image](/docs/AddColumn.png)

#### Delete Custom Column
To delete a custom column, click the trash icon on the column to remove it from the list.

![image](/docs/RemoveColumn.png)

### Filters
On the `Filters` tab you can configure which how the application will filter IO tags. 

First, you can specify whether or not to filter unused tags, which will remove tag
that are not used within the logic of the project from the output list.

Second, you can specify custom filters that allow you to further specify which 
tags to remove from the list. The following sections explain how to configure custom filters.

#### Add Custom Filter
To add a custom filter, click the plus action icon button on the top right. 
This will launch the tag filter popup which allows you to select a property,
condition, and value for which to filter the IO tags.


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