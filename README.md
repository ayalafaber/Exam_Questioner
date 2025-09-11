# QuizVerse - Automated Quiz Management System
## Overview
QuizVerse is an automated quiz and exam management system designed for lecturers and students. The system allows lecturers to manage question banks, generate automatic exams, track student performance, and write student reviews. Students can practice exams, receive immediate feedback, and improve their skills based on personal progress.

## System Requirements
- **OS:** Windows 10/11
- **RAM:** 4GB minimum, 8GB recommended
- **Storage:** 500MB free space
- **Software:** Microsoft Excel, .NET Framework

## Quick Start
### For End Users
1. Download the QuizVerse.exe file
2. Place the Excel database file on your **Desktop**
3. **Important:** Save the Excel file before using the system
4. **Critical:** Ensure Excel file is completely closed before running the application
5. Run QuizVerse.exe
6. Incase you need to edit the Excel file, close the system first, make your changes, save, and then reopen the system.
    the excel password is `ayala123`

### Database Setup
- **Location:** Desktop (required)
- **Format:** Excel file (.xlsx)
- **Preparation:** Must be saved before launching the application
- **Excel Status:** Must be closed - if Excel file is open, the system cannot access it
- **Backup:** Regularly backup your database file

## Development
### Environment
- **IDE:** Visual Studio 2022
- **Language:** C#
- **Framework:** .NET
- **Database:** Excel files on DesktopD

### Required Packages
Before running or testing the project, install the MSTest packages:
```
Install-Package MSTest.TestFramework
Install-Package MSTest.TestAdapter
Install-Package SendGrid
```

### Running the Project
1. Open the solution file `Exam_Questioner.sln` in Visual Studio 2022
2. Set `Exam_Questioner` as the startup project
3. Ensure Excel database is on Desktop
4. **Important:** Close Excel file completely before running
5. Press the green "▶ Start" button or F5
6. System will launch in debug mode

### Testing
The project includes comprehensive unit tests:

**Available Test Suites:**
- `CreateQuestionTests.cs` - Question creation functionality
- `ExamGeneratorLogicTests.cs` - Exam generation logic
- `ExamLogicTests.cs` - Core exam functionality
- `ExamOrPracticeLogicTests.cs` - Practice mode logic
- `GradesTrackerTests.cs` - Grading system
- `LecturerReviewsFormTests.cs` - Lecturer review features
- `LoginFormTests.cs` - Authentication system
- `RegisterFormTests.cs` - User registration
- `RegisterLogic.cs` - Registration logic
- `StudentReviewsFormTests.cs` - Student review system

**Run Tests:**
- **Visual Studio:** Use Test Explorer → Run All Tests
- **Command Line:** `dotnet test`

**Testing Requirements:**
- MSTest.TestFramework must be installed
- MSTest.TestAdapter must be installed
- Excel file must be closed during testing

## Key Features
### For Lecturers
- Question bank management
- Automatic exam generation
- Student performance tracking
- Student review system
- Performance analytics

### For Students
- Practice exams
- Immediate feedback
- Progress tracking
- Personal improvement metrics

## Project Structure
```
Solution 'Exam_Questioner'/
├── Exam_Questioner/           # Main project
│   ├── Forms/                 # UI forms and design
│   ├── Logic/                 # Business logic
│   ├── Database/              # Excel integration
│   └── Resources/             # UI resources
└── Exam_Questioner_Tests/     # Test project
    ├── CreateQuestionTests.cs
    ├── ExamGeneratorLogicTests.cs
    ├── ExamLogicTests.cs
    ├── ExamOrPracticeLogicTests.cs
    ├── GradesTrackerTests.cs
    ├── LecturerReviewsFormTests.cs
    ├── LoginFormTests.cs
    ├── RegisterFormTests.cs
    └── StudentReviewsFormTests.cs
```

## Troubleshooting
**System can't find database:**
- Ensure Excel file is on Desktop
- Check file is saved and not open elsewhere

**Excel Access Errors:**
- **Most Common Issue:** Excel file is open - close it completely
- Verify Excel file isn't corrupted
- Check write permissions
- Try running as administrator

**Runtime errors:**
- Verify Excel file isn't corrupted
- Check write permissions
- Try running as administrator

**Performance issues:**
- Close unnecessary applications
- Ensure sufficient disk space

**Testing Issues:**
- Install MSTest packages if tests won't run
- Ensure Excel file is closed before running tests

## Important Notes
- **Critical:** Always close Excel file completely before launching the system
- Always save Excel database before launching
- Keep regular backups of your database
- Close system before manually editing Excel file
- Use practice mode before important exams
- Install MSTest packages for development and testing

---
**Version:** 1.0 | **Last Updated:** June 2025