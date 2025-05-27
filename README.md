# FAT-system-master

A virtual FAT-based file system simulation developed using C# and Windows Forms. This project simulates file system operations like file creation, deletion, renaming, directory handling, and disk management using a custom File Allocation Table (FAT) model.

---

## 🧠 How It Works

The system emulates a virtual disk (`miniFat.txt`) and builds a hierarchical file system structure on top of it using a FAT table. Users interact with the system via a shell-like interface that supports various commands.

The FAT table is used to manage disk space. Each file or directory is stored in clusters on the virtual disk. Directory and file metadata are handled through directory entries.

---

---

## 🧰 Technologies Used

- C# (Console Interaction for I/O operations)
- Object-Oriented Programming

---

## ⚙️ System Architecture

The architecture includes these major layers:

1. **Virtual Disk Layer:** Simulates persistent storage.
2. **FAT Layer:** Handles cluster allocation and linking.
3. **Directory & File Layer:** Handles file system structure and metadata.
4. **Parser & Command Layer:** Parses and executes user commands from the shell.

---

## 📦 Core Components

* `Directory` – Represents a folder containing entries (files or subdirectories).
* `DirectoryEntry` – Metadata for files/folders: name, size, attribute, and first cluster.
* `FILE` – Represents file objects and handles operations like reading, writing, copying, and deletion.
* `FAT` – Simulated File Allocation Table that manages cluster allocation and linking.
* `VirtualDisk` – Emulates low-level disk operations (reading/writing clusters from/to a text file).
* `Parser` – Processes and executes shell commands input by the user.
* `Program` – Main controller that initializes the system and runs the shell loop.
* `Help` – Provides command-line help and system usage instructions.
* `miniFat.txt` – A text-based virtual disk storage that holds the file system data persistently.

---

## 📚 Available Shell Commands

### ✅ `cls`

Clears the shell screen.

### ✅ `quit`

Terminates the application.

### ✅ `help`

Displays all available commands and usage instructions.

### ✅ `cd <directory>`

Changes the current directory to the specified directory.

### ✅ `md <directory>`

Creates a new directory under the current directory.

### ✅ `dir`

Lists the contents of the current directory including files and folders.

### ✅ `rd <directory>`

Deletes an empty directory from the current path.

### ✅ `import <source>`

Imports a real file from the host system into the virtual file system.

### ✅ `type <file>`

Displays the contents of the specified file.

### ✅ `export <filename> <destination>`

Exports a file from the virtual file system to a real location on the host machine.

### ✅ `rename <oldName> <newName>`

Renames a file or directory.

### ✅ `del <file>`

Deletes a file after confirmation.

### ✅ `copy <source> <destination>`

Copies a file from the current directory to a specified destination directory.

---

## 📁 Project Structure
```
Simple Shell
├── Directory.cs
├── DirectoryEntry.cs
├── FAT.cs
├── FILE.cs
├── Help.cs
├── miniFat.txt
├── Parser.cs
├── Program.cs
└── VirtualDisk.cs
|__README.md 
```
---

## 👨‍💻 Author
BY MY TEAM
Developed as a university project for demonstrating of file systems and disk management using C#.

---
---

## 🧪 Example Usage

Here's an example session demonstrating how to use the file system:

```shell
> help
Available commands:
- cls        : Clear screen
- quit       : Exit program
- help       : Show help
- cd         : Change directory
- md         : Make directory
- dir        : List contents
- rd         : Remove directory
- import     : Import file from host
- type       : Show file contents
- export     : Export file to host
- rename     : Rename file/directory
- del        : Delete file
- copy       : Copy file

> md Documents
Directory 'Documents' created successfully.

> cd Documents
Current directory changed to 'Documents'.

> import C:\Users\user\file.txt
File 'file.txt' imported successfully.

> dir
Contents of 'Documents':
file.txt       1024 bytes

> type file.txt
This is the content of the imported file.

> export file.txt C:\Users\user\backup.txt
File exported successfully to 'C:\Users\user\backup.txt'.

> rename file.txt document.txt
File renamed successfully to 'document.txt'.

> del document.txt
Are you sure you want to delete 'document.txt'? (Y/N)
Y
File deleted successfully.

> cd ..
Returned to parent directory.

> quit
Exiting FAT system...
