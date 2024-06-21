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
