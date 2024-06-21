# My Slidely Task - 2 :: VB.NET Windows Forms Application with TypeScript + Express Server

This project is a Windows Forms application written in VB.NET that interacts with a TypeScript + Express server. The application allows users to create, read, update, and delete entries stored in a JSON file on the server. The entries consist of name, email, phone number, GitHub link, and time spent (measured by a stopwatch).

## Table of Contents

- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
  - [Backend Setup](#backend-setup)
  - [Frontend Setup](#frontend-setup)
- [Application Features](#application-features)
- [Keyboard Shortcuts](#keyboard-shortcuts)
- [Error Handling](#error-handling)
- [Usage](#usage)
  - [Creating an Entry](#creating-an-entry)
  - [Viewing Entries](#viewing-entries)
  - [Deleting an Entry](#deleting-an-entry)
  - [Updating an Entry](#updating-an-entry)

## Prerequisites

- Visual Studio 2022
- Node.js and npm
- .NET Framework

## Setup Instructions

### Backend Setup

1. **Clone the repository:**

    ```sh
    git clone <repository-url>
    cd <repository-directory>/server
    ```

2. **Install dependencies:**

    ```sh
    npm install
    ```

3. **Setup TypeScript configuration:**

    Create `tsconfig.json`:

    ```json
    {
        "compilerOptions": {
            "target": "ES6",
            "module": "commonjs",
            "outDir": "./dist",
            "rootDir": "./src",
            "strict": true,
            "esModuleInterop": true
        }
    }
    ```

4. **Create server files:**

    Create `src/index.ts` and `db.json`:

    ```sh
    mkdir src
    cd src
    touch index.ts db.json
    ```

5. **Implement the server code in `index.ts`:**

    ```typescript
    import express, { Request, Response } from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';
import path from 'path';

const app = express();
const port = 3000;

app.use(bodyParser.json());

const dataFilePath = path.join(__dirname, 'db.json');

// Helper functions
const readDataFromFile = () => {
    return JSON.parse(fs.readFileSync(dataFilePath, 'utf-8'));
};

const writeDataToFile = (data: any) => {
    fs.writeFileSync(dataFilePath, JSON.stringify(data, null, 2));
};

// Route: Ping
app.get('/ping', (req: Request, res: Response) => {
    res.send(true);
});

// Route: Submit
app.post('/submit', (req: Request, res: Response) => {
    const { name, email, phone, githubLink, timeSpent } = req.body;
    const data = readDataFromFile();
    data.push({ name, email, phone, githubLink, timeSpent });
    writeDataToFile(data);
    res.sendStatus(200);
});

// Route: Read All
app.get('/read', (req: Request, res: Response) => {
    const data = readDataFromFile();
    res.json(data);
});

// Route: Read by Email
app.get('/read/:email', (req: Request, res: Response) => {
    const email = req.params.email;
    const data = readDataFromFile();
    const entry = data.find((entry: any) => entry.email === email);
    if (entry) {
        res.json(entry);
    } else {
        res.status(404).json({ error: "Not Found" });
    }
});

// Route: Delete by Email
app.delete('/delete/:email', (req: Request, res: Response) => {
    const email = req.params.email;
    let data = readDataFromFile();
    const initialLength = data.length;
    data = data.filter((entry: any) => entry.email !== email);
    writeDataToFile(data);
    if (data.length < initialLength) {
        res.sendStatus(200);
    } else {
        res.sendStatus(404);
    }
});

// Route: Update by Email
app.put('/update/:email', (req: Request, res: Response) => {
    const emailToUpdate = req.params.email;
    const { name, phone, githubLink } = req.body;

    let data = readDataFromFile();
    const index = data.findIndex((entry: any) => entry.email === emailToUpdate);

    if (index !== -1) {
        // Update fields
        data[index].name = name;
        data[index].phone = phone;
        data[index].githubLink = githubLink;
        
        writeDataToFile(data);
        res.sendStatus(200);
    } else {
        res.status(404).json({ error: "Not Found" });
    }
});


// Start the server
app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
    ```

6. **Build and start the server:**

    ```sh
    npm run build
    npm start
    ```

### Frontend Setup

1. **Open the solution in Visual Studio 2022:**

    ```sh
    cd ../client
    ```

2. **Open the `.sln` file in Visual Studio 2022.**

3. **Build the solution** to restore dependencies and ensure all references are correct.

4. **Run the application.**

## Application Features

- **Create Submission:** Allows users to enter and submit their details, including a stopwatch timer.
- **View Submissions:** Displays the details of submissions one by one with navigation buttons.
- **Update Submission:** Allows users to search for a submission by email and update the details.
- **Delete Submission:** Allows users to search for a submission by email and delete the entry.

## Keyboard Shortcuts

- **Create Submission:** `Ctrl + N`
- **View Submission:** `Ctrl + V`
- **Submit:** `Ctrl + S`
- **Previous Entry:** `Ctrl + P`
- **Next Entry:** `Ctrl + N`
- **Toggle Stopwatch:** `Ctrl + T`
- **Exit Application:** `Esc`

## Error Handling

The application includes robust error handling with `Try...Catch` blocks around critical operations to ensure graceful error handling and user notifications.

## Usage

### Creating an Entry

1. Open the application.
2. Click on "Create Submission" or press `Ctrl + N`.
3. Fill in the details.
4. Use the stopwatch as needed.
5. Click "Submit" or press `Ctrl + S` to save the entry.

### Viewing Entries

1. Click on "View Submission" or press `Ctrl + V`.
2. Navigate through the entries using the "Previous" (`Ctrl + P`) and "Next" (`Ctrl + N`) buttons.

### Deleting an Entry

1. Click on "Delete Submission" and enter the email address.
2. Click "Search" to find the entry.
3. Click "Delete" to remove the entry from the database.

### Updating an Entry

1. Click on "Update Submission" and enter the email address.
2. Click "Search" to find the entry.
3. Update the details as needed.
4. Click "Update" to save the changes.

---
