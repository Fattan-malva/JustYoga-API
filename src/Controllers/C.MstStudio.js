const ClassModel = require('../Models/M.MstStudio');

async function index(req, res) {
    const classes = await ClassModel.findAll();
    res.json(classes);
}

async function show(req, res) {
    const id = Number(req.params.id);
    const classData = await ClassModel.findById(id);
    if (!classData) return res.status(404).json({ message: 'Not found' });
    res.json(classData);
}

module.exports = { index };
