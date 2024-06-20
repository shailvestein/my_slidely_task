"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const body_parser_1 = __importDefault(require("body-parser"));
const fs_1 = __importDefault(require("fs"));
const path_1 = __importDefault(require("path"));
const app = (0, express_1.default)();
const PORT = 3000;
app.use(body_parser_1.default.json());
const dataFilePath = path_1.default.join(__dirname, 'data.json');
app.get('/ping', (req, res) => {
    res.json(true);
});
app.post('/submit', (req, res) => {
    const { name, email, phone, githubLink, timeSpent } = req.body;
    const newData = { name, email, phone, githubLink, timeSpent };
    let data = [];
    if (fs_1.default.existsSync(dataFilePath)) {
        data = JSON.parse(fs_1.default.readFileSync(dataFilePath, 'utf-8'));
    }
    data.push(newData);
    fs_1.default.writeFileSync(dataFilePath, JSON.stringify(data, null, 2));
    res.status(201).send('Submission saved');
});
app.get('/read', (req, res) => {
    if (fs_1.default.existsSync(dataFilePath)) {
        const data = JSON.parse(fs_1.default.readFileSync(dataFilePath, 'utf-8'));
        res.json(data);
    }
    else {
        res.json([]);
    }
});
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
