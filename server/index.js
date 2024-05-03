console.log('Hello World! Node is running.. in server/index.js');
const express = require('express');
const app = express();
const port = 4000;
app.get('/', (req, res) => res.json({hello: 'world'}));
app.listen(port, console.log(`Server is running on port ${port}.`));